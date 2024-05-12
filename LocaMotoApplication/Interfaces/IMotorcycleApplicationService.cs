using LocaMoto.Application.DTOs.Requests;
using LocaMoto.Application.DTOs.Responses;

namespace LocaMoto.Application.Interfaces
{
    public interface IMotorcycleApplicationService
    {        
        Task SaveAsync(MotorcycleRequestDto motorcycleRequestDto, CancellationToken cancellationToken);
        Task UpdateAsync(MotorcycleRequestDto motorcycleRequestDto, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<MotorcycleResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<MotorcycleResponseDto>> FindByLicensePlateAsync(string description, CancellationToken cancellationToken);
        Task<IEnumerable<MotorcycleResponseDto>> GetAllAsync(CancellationToken cancellationToken);
    }
}
