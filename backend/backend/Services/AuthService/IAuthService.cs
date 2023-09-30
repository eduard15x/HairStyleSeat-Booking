using backend.Dtos.Auth;
using backend.Models.Auth;

namespace backend.Services.AuthService
{
    public interface IAuthService
    {
        Task<User> Register(RegisterUserDto registerUserDto);
    }
}
