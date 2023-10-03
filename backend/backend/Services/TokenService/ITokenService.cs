using backend.Dtos.Auth;
using backend.Models.Auth;

namespace backend.Services.TokenService
{
    public interface ITokenService
    {
        UserTokenDto GenerateToken(User user);
    }
}
