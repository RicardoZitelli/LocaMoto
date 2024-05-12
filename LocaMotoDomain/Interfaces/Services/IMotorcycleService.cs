using LocaMoto.Domain.Entities;

namespace LocaMoto.Domain.Interfaces.Services
{
    public interface IMotorcycleService : IGenericService<Motorcycle>
    {
        public Task<Motorcycle?> FindByLicensePlateAsync(string description, CancellationToken cancellationToken);
    }
}
