using LocaMotoDomain.Entities;

namespace LocaMotoDomain.Interfaces.Repositories
{
    public interface IMotorcycleRepository : IGenericRepository<Motorcycle>
    {
        public Task<Motorcycle?> FindByLicensePlateAsync(string description, CancellationToken cancellationToken);
    }
}
