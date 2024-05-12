using LocaMoto.Application.DTOs.Requests;
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
    public class UserController(IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        /// <summary>
        /// Create JWT Token
        /// </summary>
        /// <returns>JWT Token</returns>

        [HttpPost("Autorizar")]
        [AllowAnonymous]
        public ActionResult GenerateJwtToken([FromBody] UserRequestDto usuarioRequestDto)
        {
            if ((string.IsNullOrWhiteSpace(usuarioRequestDto.User) || usuarioRequestDto.User != "Admin") &&
                (string.IsNullOrWhiteSpace(usuarioRequestDto.Password) || usuarioRequestDto.Password != "Admin123"))
            {
                return BadRequest("User not found or password is incorrect.");
            }

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
    }
}
