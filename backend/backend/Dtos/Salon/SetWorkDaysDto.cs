namespace backend.Dtos.Salon
{
    public class SetWorkDaysDto
    {
        public int UserId { get; set; }
        public int SalonId { get; set; }
        public string WorkDays { get; set; }
    }
}
