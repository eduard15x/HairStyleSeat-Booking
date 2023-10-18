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
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string WorkDays { get; set; }
        public string StartTimeHour { get; set; }
        public string EndTimeHour { get; set; }
        [Column(TypeName = "decimal(2, 1)")]
        public decimal SalonReviews { get; set; }
        public int SalonStatusId { get; set; }
        [ForeignKey("SalonStatusId")]
        public SalonStatus Status { get; set; }
    }
}


// if i call the prop above "SalonStatus" + "Id" with and ID at the end or make a new foreign key, cant migrate