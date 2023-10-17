using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.SalonService
{
    public class UpdateSalonServiceDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int SalonId { get; set; }
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public double Price { get; set; }
        public string HaircutDurationTime { get; set; }
    }
}
