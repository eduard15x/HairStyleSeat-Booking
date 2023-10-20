namespace backend.Dtos.Salon
{
    public class ReportUserDto
    {
        public int UserId { get; set; }
        public int SalonId { get; set; }
        public string ReportMessage { get; set; }
    }
}
