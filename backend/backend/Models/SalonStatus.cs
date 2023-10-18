namespace backend.Models
{
    public class SalonStatus
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
        public ICollection<Salon> Salons { get; set; }
    }
}
