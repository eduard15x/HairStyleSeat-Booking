using backend.Data;
using backend.Dtos.Auth;
using backend.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.AuthRepository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Register(User newUser)
        {
            if (await _dbContext.Users.AnyAsync(u => u.Email == newUser.Email))
            {
                throw new Exception("User already exists.");
            }
            // Hash and Salt password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            var newUserRegistered = new User();
            // adding properties to the new user
            newUserRegistered.UserName = newUser.UserName;
            newUserRegistered.Email= newUser.Email;
            newUserRegistered.Password = hashedPassword;
            newUserRegistered.City = newUser.City;
            newUserRegistered.PhoneNumber = newUser.PhoneNumber;

            _dbContext.Users.Add(newUserRegistered);
            await _dbContext.SaveChangesAsync();

            return newUserRegistered;
        }

        public async Task<User> Login(LoginUserDto userCredentials)
        {
            var userFromDb = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userCredentials.Email);
            
            if (userFromDb == null)
            {
                throw new Exception("Email doesn't exist.");
            }

            var verifyPassword = BCrypt.Net.BCrypt.Verify(userCredentials.Password, userFromDb.Password);

            if (!verifyPassword)
            {
                throw new Exception("Password is wrong.");
            }

            return userFromDb;
        }
    }
}
