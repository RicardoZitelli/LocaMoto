using LocaMoto.Domain.Entities;

namespace LocaMoto.Domain.Interfaces.Services
{
    public interface IUserService : IGenericService<User>
    {
        public Task<User?> FindByUserEmailAsync(string description, CancellationToken cancellationToken);

        public Task<User?> LoginAsync(string userEmail, string password, CancellationToken cancellationToken);
    }
}
