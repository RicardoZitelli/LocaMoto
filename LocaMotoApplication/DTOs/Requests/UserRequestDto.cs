namespace LocaMoto.Application.DTOs.Requests
{
    public record UserRequestDto(Guid Id, string UserEmail, string Password, string Role);    
}
