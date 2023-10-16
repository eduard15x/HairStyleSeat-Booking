using backend.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Salon
{
    public class Salon
    {
        public int Id { get; set; }
        public string SalonName { get; set; }
        public string SalonCity { get; set; }
        public string SalonAddress { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string WorkDays { get; set; }
        public string WorkHoursInterval { get; set; }
        [Column(TypeName = "decimal(2, 1)")]
        public decimal SalonReviews { get; set; }
        public int SalonStatus { get; set; } = 2;
        // if i call this prop above with and ID at the end or make a new foreign key, cant migrate
    }
}
