namespace backend.Dtos.Reservation
{
    public class GetSalonReservationListDto
    {
        public List<GetReservationDetailsForSalonDto> Reservations { get; set; }
        public int TotalReservations { get; set; }
    }
}
