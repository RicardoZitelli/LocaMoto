using LocaMotoDomain.Entities;

namespace LocaMotoDomain.Interfaces.Services
{
    public interface IMotorcycleService : IGenericService<Motorcycle>
    {
        public Task<Motorcycle?> FindByLicensePlateAsync(string description, CancellationToken cancellationToken);
    }
}
