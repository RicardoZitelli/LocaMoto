using LocaMoto.Domain.Entities;
using LocaMoto.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LocaMoto.Infrastructure.Data.Repositories
{
    public class UserRepository(SqlContext sqlContext) : GenericRepository<User>(sqlContext), IUserRepository
    {
        private readonly SqlContext _sqlContext = sqlContext;

        public async Task<User?> FindByUserEmailAsync(string description, CancellationToken cancellationToken)
        {
            var user = await _sqlContext
                .Set<User>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserEmail == description, cancellationToken);

            return user;
        }

        public async Task<User?> LoginAsync(string userEmail,string password, CancellationToken cancellationToken)
        {
            var user = await _sqlContext
                .Set<User>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserEmail == userEmail && x.Password == password, cancellationToken);

            return user;
        }
    }
}
