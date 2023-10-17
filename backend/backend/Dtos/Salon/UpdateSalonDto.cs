using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Salon
{
    public class UpdateSalonDto
    {
        [Required]
        public int Id { get; set;}
        [Required]
        public string SalonName { get; set; }
        [Required]
        public string SalonCity { get; set; }
        [Required]
        public string SalonAddress { get; set; }
        [Required]
        public int UserId { get; set; }
        public string WorkDays { get; set; } = string.Empty;
        public string StartTimeHour { get; set; } = string.Empty;
        public string EndTimeHour { get; set; } = string.Empty;
    }
}
