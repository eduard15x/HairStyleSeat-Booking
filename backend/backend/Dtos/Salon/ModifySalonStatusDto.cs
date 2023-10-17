using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Salon
{
    public class ModifySalonStatusDto
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int SalonUserId { get; set; }
        [Required]
        public int SalonId { get; set; }
        [Required]
        public int SalonStatusId { get; set; }
    }
}
