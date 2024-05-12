namespace LocaMoto.Application.DTOs.Responses
{
    public record UserResponseDto(Guid Id, string UserEmail,string Role, DateTime CreationDate, DateTime LastLoginDate, bool UserEmailConfirmed);
}
