using backend.Dtos.Auth;
using backend.Models.Auth;

namespace backend.Repositories.AuthRepository
{
    public interface IAuthRepository
    {
        Task<User> Register(User newUser);
        Task<User> Login(LoginUserDto userCredentials);
        Task<string> ChangePassword(ChangePasswordDto changePasswordDto);
    }
}
