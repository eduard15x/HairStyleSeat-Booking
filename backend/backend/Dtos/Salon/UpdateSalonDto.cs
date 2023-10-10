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
        [Required]
        public string WorkDays { get; set; }
        [Required]
        public string WorkHoursInterval { get; set; }
    }
}
