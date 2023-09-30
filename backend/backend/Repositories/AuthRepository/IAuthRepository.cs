using backend.Models.Auth;

namespace backend.Repositories.AuthRepository
{
    public interface IAuthRepository
    {
        Task<User> Register(User newUser);
    }
}
