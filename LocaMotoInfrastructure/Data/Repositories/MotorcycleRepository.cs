using LocaMotoDomain.Entities;
using LocaMotoDomain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LocaMotoInfrastructure.Data.Repositories
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
