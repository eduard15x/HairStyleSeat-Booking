using backend.Dtos.Auth;
using backend.Models;

namespace backend.Services.AuthService
{
    public interface IAuthService
    {
        Task<User> Register(RegisterUserDto registerUserDto);
        Task<UserTokenDto> Login(LoginUserDto userCredentials);
        Task<UserTokenDto> ChangePassword(ChangePasswordDto changePassword);
        Task<UpdateUserDto> UpdateUser(UpdateUserDto updateUser);
    }
}
