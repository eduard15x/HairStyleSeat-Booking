using backend.Dtos.Auth;
using backend.Models.Auth;
using backend.Repositories.AuthRepository;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace backend.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<User> Register(RegisterUserDto registerUserDto)
        {
            if (registerUserDto.Password != registerUserDto.ConfirmPassword)
            {
                throw new Exception("Passwords don't match");
            }

            var newUser = new User()
            {
                UserName = registerUserDto.UserName,
                Email = registerUserDto.Email,
                Password = registerUserDto.Password,
                City = registerUserDto.City,
                PhoneNumber = registerUserDto.PhoneNumber,
            };

            return await _authRepository.Register(newUser);
        }
    }
}
