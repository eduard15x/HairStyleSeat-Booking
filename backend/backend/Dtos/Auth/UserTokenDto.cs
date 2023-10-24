using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Auth
{
    public class UserTokenDto
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
