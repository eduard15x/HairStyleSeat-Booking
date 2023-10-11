using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Auth
{
    public class ChangePasswordDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
