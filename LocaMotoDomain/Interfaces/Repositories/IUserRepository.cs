using LocaMoto.Domain.Entities;

namespace LocaMoto.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User?> FindByUserEmailAsync(string description, CancellationToken cancellationToken);

        public Task<User?> LoginAsync(string userEmail, string password, CancellationToken cancellationToken);
    }
}
