using backend.Dtos.Reservation;

namespace backend.Services.ReservationService
{
    public interface IReservationService
    {
        Task<GetReservationDetailsCustomerDto> MakeReservation(CreateReservationDto createReservationDto);
        Task<List<GetReservationDetailsCustomerDto>> GetAllCustomerReservations(int customerId);
        Task<GetReservationDetailsCustomerDto> GetCustomerReservationDetails(int customerId, int reservationId);
    }
}
