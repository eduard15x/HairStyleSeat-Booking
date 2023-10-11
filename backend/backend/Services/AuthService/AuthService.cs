using backend.Dtos.Auth;
using backend.Models.Auth;
using backend.Repositories.AuthRepository;
using backend.Services.TokenService;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace backend.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IAuthRepository authRepository, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);


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

        public async Task<string> ChangePassword(ChangePasswordDto changePassword)
        {
            if (changePassword.UserId <= 0)
                throw new Exception("User not found");

            if (changePassword.UserId != GetUserId())
                throw new Exception("User not authorized");

            if (changePassword.NewPassword != changePassword.ConfirmNewPassword)
                throw new Exception("Passwords don't match");

            return await _authRepository.ChangePassword(changePassword);
        }

        public async Task<UpdateUserDto> UpdateUser(UpdateUserDto updateUser)
        {
            var currentUser = GetUserId();

            if (updateUser.Id != currentUser)
                throw new Exception("User not authorized");

            var updatedUser = await _authRepository.UpdateUser(updateUser);

            return new UpdateUserDto() 
            { 
                Id = updatedUser.Id, 
                UserName = updateUser.UserName,
                City = updateUser.City,
                PhoneNumber = updateUser.PhoneNumber,
            };
        }
    }
}
