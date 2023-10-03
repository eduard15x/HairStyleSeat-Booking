using backend.Dtos.Auth;
using backend.Models.Auth;
using backend.Repositories.AuthRepository;
using backend.Services.TokenService;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.IdentityModel.Tokens.Jwt;

namespace backend.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
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

        public async Task<UserTokenDto> Login(LoginUserDto userCredentials)
        {
            if (userCredentials == null || string.IsNullOrEmpty(userCredentials.Email) || string.IsNullOrEmpty(userCredentials.Password))
            {
                throw new Exception("User credentials are missing");
            }

            var userInfo = await _authRepository.Login(userCredentials);

            return _tokenService.GenerateToken(userInfo);
        }
    }
}
