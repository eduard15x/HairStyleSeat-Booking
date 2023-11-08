namespace backend.Dtos.Reservation
{
    public class GetReservationDetailsForSalonDto
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int SalonId { get; set; }
        public int SalonServiceId { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public string HaircutDurationTime { get; set; }
        public string ReservationDay { get; set; }
        public string ReservationHour { get; set; }
        public int IsReservationCompleted { get; set; }
        public DateTime ReservationSubmittedDate { get; set; }
    }
}