using FluentValidation;
using LocaMoto.Validator;
using LocaMoto.Application.DTOs.Requests;
using LocaMoto.Application.DTOs.Responses;
using LocaMoto.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocaMoto.API.Controllers
{
    /// <summary>
    /// Controller used for inserting, updating, deleting, retrieving, and listing motorcycle data
    /// </summary>    
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Admin,Administrator")]
    [ApiController]
    public class MotorcycleController(IMotorcycleApplicationService motorcycleApplicationService) : ControllerBase
    {
        private readonly IMotorcycleApplicationService _motorcycleApplicationService = motorcycleApplicationService;

        /// <summary>
        /// This endpoint is responsible for inserting a new customer into the database
        /// </summary>
        /// <param name="motorcycleRequestDto"></param>
        /// <param name="validator"></param>
        /// <param name="cancellationToken"></param>        
        [HttpPost("Save")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveAsync([FromBody] MotorcycleRequestDto motorcycleRequestDto,
            [FromServices]IValidator<MotorcycleRequestDto> validator,
            CancellationToken cancellationToken)
        {
            try
            {
                if (motorcycleRequestDto is null)
                    return BadRequest("The object Motorcycle is null");

                var modelStateDictionary = await GenericValidatorHelpers.ValidateRequest(validator, motorcycleRequestDto);

                if (modelStateDictionary is not null)
                    return ValidationProblem(modelStateDictionary);
                
                await _motorcycleApplicationService.SaveAsync(motorcycleRequestDto, cancellationToken);
                return Ok("Motorcycle registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// This endpoint is responsible for updating a customer in the database
        /// </summary>
        /// <param name="motorcycleRequestDto"></param>
        /// <param name="cancellationToken"></param>        
        /// <param name="validator"></param>      
        [HttpPut("Update")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(MotorcycleRequestDto motorcycleRequestDto,
          [FromServices] IValidator<MotorcycleRequestDto> validator,
          CancellationToken cancellationToken)
        {
            try
            {
                if (motorcycleRequestDto is null ||
                    motorcycleRequestDto.Id == Guid.Empty)
                    return BadRequest("No motorcycle was found");

                var modelStateDictionary = await GenericValidatorHelpers.ValidateRequest(validator, motorcycleRequestDto);

                if (modelStateDictionary is not null)
                    return ValidationProblem(modelStateDictionary);

                await _motorcycleApplicationService.UpdateAsync(motorcycleRequestDto!, cancellationToken);

                return Ok("Motorcycle updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Algo deu errado. {ex.Message}");
            }
        }

        /// <summary>
        /// This endpoint is responsible for deleting a customer from the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>        
        [HttpDelete("Excluir/{id}")]
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

                await _motorcycleApplicationService.DeleteAsync(id, cancellationToken);

                return Ok("Cliente excluído com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Algo deu errado. {ex.Message}");
            }
        }

        /// <summary>
        /// This endpoint is responsible for listing all customers from the database
        /// </summary>        
        /// <param name="cancellationToken"></param>        
        [HttpGet("Listar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<MotorcycleResponseDto>> ListarAsync(CancellationToken cancellationToken) =>
            await _motorcycleApplicationService.GetAllAsync(cancellationToken);

        /// <summary>
        /// This endpoint is responsible for retrieving a motorcycle from the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>        
        [HttpGet("Obter/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<MotorcycleResponseDto> ObterAsync([FromRoute] Guid id,
            CancellationToken cancellationToken) =>
            await _motorcycleApplicationService.GetByIdAsync(id, cancellationToken);
    }   
}
