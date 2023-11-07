using backend.Dtos.Reservation;

namespace backend.Services.ReservationService
{
    public interface IReservationService
    {
        Task<GetReservationDetailsCustomerDto> MakeReservation(CreateReservationDto createReservationDto);
        Task<List<GetReservationDetailsCustomerDto>> GetAllCustomerReservations(int customerId);
        Task<List<GetReservationDetailsForSalonDto>> GetAllSalonReservations(int salonAffiliateId);
        Task<GetReservationDetailsCustomerDto> GetCustomerReservationDetails(int customerId, int reservationId);
        Task<string> ConfirmReservation(int reservationId, int salonOwnerId);
        Task<string> CancelReservation(int reservationId, int customerId);
    }
    
}
