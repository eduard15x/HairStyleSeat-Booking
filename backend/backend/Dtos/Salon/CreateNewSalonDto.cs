using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string WorkDays { get; set; } = string.Empty;
        [Required]
        public string StartTimeHour { get; set; } = string.Empty;
        [Required]
        public string EndTimeHour { get; set; } = string.Empty;
        [Column(TypeName = "decimal(2, 1)")]
        public decimal SalonReviews { get; set; } = 0;
    }
}
