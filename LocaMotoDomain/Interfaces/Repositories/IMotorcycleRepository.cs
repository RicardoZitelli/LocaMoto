using LocaMoto.Domain.Entities;

namespace LocaMoto.Domain.Interfaces.Repositories
{
    public interface IMotorcycleRepository : IGenericRepository<Motorcycle>
    {
        public Task<Motorcycle?> FindByLicensePlateAsync(string description, CancellationToken cancellationToken);
    }
}
