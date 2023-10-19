namespace backend.Dtos.Salon
{
    public class ReviewSalonDto
    {
        public int UserId { get; set; }
        public int SalonId { get; set; }
        public int ReviewRating { get; set; }
        public string ReviewMessage { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;
    }
}
