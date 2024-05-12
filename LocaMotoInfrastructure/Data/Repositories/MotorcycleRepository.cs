using LocaMoto.Domain.Entities;
using LocaMoto.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LocaMoto.Infrastructure.Data.Repositories
{
    public class MotorcycleRepository(SqlContext sqlContext) : GenericRepository<Motorcycle>(sqlContext), IMotorcycleRepository
    {
        private readonly SqlContext _sqlContext = sqlContext;

        public async Task<Motorcycle?> FindByLicensePlateAsync(string description, CancellationToken cancellationToken)
        {
            var motorcycle = await _sqlContext
                .Set<Motorcycle>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.LicensePlate == description, cancellationToken);

            return motorcycle;         
        }
    }

   
}
