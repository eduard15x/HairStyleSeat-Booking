namespace backend.Dtos.Reservation
{
    public class GetReservationDetailsCustomerDto
    {
        public int ReservationId { get; set; }
        public string SalonName { get; set; }
        public string SalonCity { get; set; }
        public int SalonServiceId { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public string HaircutDurationTime { get; set; }
        public string ReservationDay { get; set; }
        public string ReservationHour { get; set; }
    }
}
