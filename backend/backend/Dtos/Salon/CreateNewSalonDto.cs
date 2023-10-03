using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Salon
{
    public class CreateNewSalonDto
    {
        [Required]
        public string SalonName { get; set; }
        [Required]
        public string SalonCity { get; set; }
        [Required]
        public string SalonAddress { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
