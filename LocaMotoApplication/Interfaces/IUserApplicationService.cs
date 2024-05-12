
using LocaMoto.Application.DTOs.Requests;
using LocaMoto.Application.DTOs.Responses;
using LocaMoto.Domain.Entities;

namespace LocaMoto.Application.Interfaces
{
    public interface IUserApplicationService
    {
        Task SaveAsync(UserRequestDto userRequestDto, CancellationToken cancellationToken);
        Task UpdateAsync(UserRequestDto userRequestDto, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<UserResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);        
        Task<IEnumerable<UserResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        public Task<UserResponseDto?> LoginAsync(string userEmail, string password, CancellationToken cancellationToken);
    }
}
