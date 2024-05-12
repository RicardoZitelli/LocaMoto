using LocaMoto.Domain.Entities;
using LocaMoto.Domain.Interfaces.Repositories;
using LocaMoto.Domain.Interfaces.Services;

namespace LocaMoto.Domain.Services
{
    public class UserService(IUserRepository userRepository) : GenericService<User>(userRepository), IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<User?> FindByUserEmailAsync(string description, CancellationToken cancellationToken) =>
            await _userRepository.FindByUserEmailAsync(description, cancellationToken);
        
        public async Task<User?> LoginAsync(string userEmail, string password, CancellationToken cancellationToken) =>
            await _userRepository.LoginAsync(userEmail, password, cancellationToken);
    }
}