using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Auth
{
    public class UpdateUserDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
