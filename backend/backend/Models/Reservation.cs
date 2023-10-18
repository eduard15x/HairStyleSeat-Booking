using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int SalonId { get; set; }
        public Salon Salon { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public string HaircutDurationTime { get; set; }
        public string ReservationDay { get; set; }
        public string ReservationHour { get; set; }
        public DateTime BookSubmitDate { get; set; } = DateTime.Now;
    }
}
