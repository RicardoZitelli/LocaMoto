using FluentValidation;
using LocaMoto.Application.DTOs.Requests;
using LocaMoto.Application.DTOs.Responses;
using LocaMoto.Application.Interfaces;
using LocaMoto.Application.Services;
using LocaMoto.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LocaMoto.API.Controllers
{ /// <summary>
  /// Controller de autenticação
  /// </summary>
    [Route("api/v1/[controller]")]    
    [ApiController]
    public class UserController(IConfiguration configuration, IUserApplicationService userApplicationService) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IUserApplicationService _userApplicationService = userApplicationService;
        /// <summary>
        /// Create JWT Token
        /// </summary>
        /// <returns>JWT Token</returns>

        [HttpPost("Autorizar")]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateJwtToken([FromBody] UserRequestDto usuarioRequestDto)
        {            
            var userResponseDto = await _userApplicationService.LoginAsync(usuarioRequestDto.UserEmail, usuarioRequestDto.Password, CancellationToken.None);

            if (userResponseDto == null)            
                return BadRequest("User not found or password is incorrect.");
                                    
            var secret = _configuration.GetSection("JwtSecret")?.Value;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, "Administrator"),
                    new(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(tokenHandler.WriteToken(token));
        }

        /// <summary>
        /// This endpoint is responsible for inserting a new user into the database
        /// </summary>
        /// <param name="userRequestDto"></param>
        /// <param name="validator"></param>
        /// <param name="cancellationToken"></param>        
        [HttpPost("Save")]
        [Authorize(Roles = "Admin,Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveAsync([FromBody] UserRequestDto userRequestDto,
            [FromServices] IValidator<UserRequestDto> validator,
            CancellationToken cancellationToken)
        {
            try
            {
                if (userRequestDto is null)
                    return BadRequest("The object User is null");

                var modelStateDictionary = await GenericValidatorHelpers.ValidateRequest(validator, userRequestDto);

                if (modelStateDictionary is not null)
                    return ValidationProblem(modelStateDictionary);

                await _userApplicationService.SaveAsync(userRequestDto, cancellationToken);
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// This endpoint is responsible for updating a motorcycle in the database
        /// </summary>
        /// <param name="userRequestDto"></param>
        /// <param name="cancellationToken"></param>        
        /// <param name="validator"></param>      
        [HttpPut("Update")]
        [Authorize(Roles = "Admin,Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(UserRequestDto userRequestDto,
          [FromServices] IValidator<UserRequestDto> validator,
          CancellationToken cancellationToken)
        {
            try
            {
                if (userRequestDto is null ||
                    userRequestDto.Id == Guid.Empty)
                    return BadRequest("No motorcycle was found");

                var modelStateDictionary = await GenericValidatorHelpers.ValidateRequest(validator, userRequestDto);

                if (modelStateDictionary is not null)
                    return ValidationProblem(modelStateDictionary);

                await _userApplicationService.UpdateAsync(userRequestDto!, cancellationToken);

                return Ok("Motorcycle updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Algo deu errado. {ex.Message}");
            }
        }

        /// <summary>
        /// This endpoint is responsible for deleting a motorcycle from the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>        
        [HttpDelete("Excluir/{id}")]
        [Authorize(Roles = "Admin,Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("Ops, nenhum cliente foi identificado");

                await _userApplicationService.DeleteAsync(id, cancellationToken);

                return Ok("Cliente excluído com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Algo deu errado. {ex.Message}");
            }
        }

        /// <summary>
        /// This endpoint is responsible for listing all motorcycle from the database
        /// </summary>        
        /// <param name="cancellationToken"></param>        
        [HttpGet("Listar")]
        [Authorize(Roles = "Admin,Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<UserResponseDto>> ListarAsync(CancellationToken cancellationToken) =>
            await _userApplicationService.GetAllAsync(cancellationToken);

        /// <summary>
        /// This endpoint is responsible for retrieving a motorcycle from the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>        
        [HttpGet("Obter/{id}")]
        [Authorize(Roles = "Admin,Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<UserResponseDto> ObterAsync([FromRoute] Guid id,
            CancellationToken cancellationToken) =>
            await _userApplicationService.GetByIdAsync(id, cancellationToken);

    }
}
