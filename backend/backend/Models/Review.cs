namespace backend.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SalonId { get; set; }
        public int ReviewRating  { get; set; }
        public string ReviewMessage { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;
    }
}
