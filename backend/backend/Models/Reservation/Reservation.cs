namespace backend.Models.Reservation
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SalonId { get; set; }
        public int SalonServiceId { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public string HaircutDurationTime { get; set; }
        public string ReservationDay { get; set; }
        public string ReservationHour { get; set; }
        public DateTime BookSubmitDate { get; set; } = DateTime.Now;
    }
}
