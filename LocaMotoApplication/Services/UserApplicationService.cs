using AutoMapper;
using LocaMoto.Application.DTOs.Requests;
using LocaMoto.Application.DTOs.Responses;
using LocaMoto.Application.Interfaces;
using LocaMoto.Domain.Entities;
using LocaMoto.Domain.Interfaces.Services;
using LocaMoto.Domain.Services;
using System.Threading;
using System.Threading.Tasks;

namespace LocaMoto.Application.Services
{
    public class UserApplicationService(IUserService userService,
        IMapper mapper) : IUserApplicationService
    {
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id, cancellationToken);

                if (user is not null)
                    await _userService.DeleteAsync(user, cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllAsync(cancellationToken);

            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        public async Task<UserResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id, cancellationToken);

                return _mapper.Map<UserResponseDto>(user);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public async Task SaveAsync(UserRequestDto userRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                var userExists = await UserEmailExists(userRequestDto.UserEmail, cancellationToken);

                if (userExists)
                    throw new Exception("Username already registered");

                var user = _mapper.Map<User>(userRequestDto);
                user.Id = Guid.NewGuid();
                user.CreationDate = DateTime.Now;
                user.UserEmailConfirmed = true;

                await _userService.AddAsync(user, cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(UserRequestDto userRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                var userWithSameEmail = await _userService
                    .FindByUserEmailAsync(userRequestDto.UserEmail, cancellationToken);

                if (userWithSameEmail != null &&
                    userRequestDto.Id != userWithSameEmail.Id)
                    throw new Exception("User email exists already");

                var user = _mapper.Map<User>(userRequestDto);                

                await _userService.UpdateAsync(user, cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<bool> UserEmailExists(string description, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByUserEmailAsync(description, cancellationToken);

            if (user != null)
                return true;

            return false;
        }

        public async Task<UserResponseDto?> LoginAsync(string userEmail, string password, CancellationToken cancellationToken)
        {
            var user = await _userService.LoginAsync(userEmail, password, cancellationToken);

            var userResponseDto = _mapper.Map<UserResponseDto?>(user);

            return userResponseDto;
        }
    }
}