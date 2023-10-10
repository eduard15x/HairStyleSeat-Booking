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
        [Required]
        public string WorkDays { get; set; }
        [Required]
        public string WorkHoursInterval { get; set; }
        [Required]
        [Column(TypeName = "decimal(2, 1)")]
        public decimal SalonReviews { get; set; } = 0;
    }
}
