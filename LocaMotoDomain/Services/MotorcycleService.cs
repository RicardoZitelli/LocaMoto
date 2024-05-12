using LocaMotoDomain.Entities;
using LocaMotoDomain.Interfaces.Repositories;
using LocaMotoDomain.Interfaces.Services;

namespace LocaMotoDomain.Services
{
    public class MotorcycleService(IMotorcycleRepository motorcycleRepository) : GenericService<Motorcycle>(motorcycleRepository), IMotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository = motorcycleRepository;

        public async Task<Motorcycle?> FindByLicensePlateAsync(string description, CancellationToken cancellationToken) =>
            await _motorcycleRepository.FindByLicensePlateAsync(description, cancellationToken);
    }
}
