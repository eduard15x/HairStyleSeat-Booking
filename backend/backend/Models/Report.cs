namespace backend.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SalonId { get; set; }
        public string ReportMessage { get; set; }
        public DateTime ReportDate { get; set; } = DateTime.Now;
    }
}
