using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Reservation
{
    public class CreateReservationDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int SalonId { get; set; }
        [Required]
        public int SalonServiceId { get; set; }
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string HaircutDurationTime { get; set; }
        [Required]
        public string ReservationDay { get; set; }
        [Required]
        public string ReservationHour { get; set; }
        public DateTime BookSubmitDate { get; set; } = DateTime.Now;
    }
}
