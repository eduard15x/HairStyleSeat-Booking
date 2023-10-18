using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Salon
    {
        public int Id { get; set; }
        public string SalonName { get; set; }
        public string SalonCity { get; set; }
        public string SalonAddress { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string WorkDays { get; set; }
        public string StartTimeHour { get; set; }
        public string EndTimeHour { get; set; }
        [Column(TypeName = "decimal(2, 1)")]
        public decimal SalonReviews { get; set; } = 0;
        public int StatusId { get; set; } = 2;
        public Status Status { get; set; }
        public List<Service> Services { get; set; }
        public List<Reservation> Reservations { get; set; }

    }
}


// if i call the prop above "SalonStatus" + "Id" with and ID at the end or make a new foreign key, cant migrate