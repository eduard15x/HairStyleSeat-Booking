using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.SalonService
{
    public class DeleteSalonServiceDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int SalonId { get; set; }
        [Required]
        public string ServiceName { get; set; }
    }
}
