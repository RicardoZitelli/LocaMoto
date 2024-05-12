using LocaMoto.Domain.Entities;
using LocaMoto.Domain.Interfaces.Repositories;
using LocaMoto.Domain.Interfaces.Services;

namespace LocaMoto.Domain.Services
{
    public class MotorcycleService(IMotorcycleRepository motorcycleRepository) : GenericService<Motorcycle>(motorcycleRepository), IMotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository = motorcycleRepository;

        public async Task<Motorcycle?> FindByLicensePlateAsync(string description, CancellationToken cancellationToken) =>
            await _motorcycleRepository.FindByLicensePlateAsync(description, cancellationToken);
    }
}
