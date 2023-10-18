using backend.Dtos.Reservation;
using backend.Helpers;
using backend.Repositories.ReservationRepository;
using System.Security.Claims;

namespace backend.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);


        public ReservationService(IReservationRepository reservationRepository, IHttpContextAccessor httpContextAccessor)
        {
            _reservationRepository = reservationRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetReservationDetailsCustomerDto> MakeReservation(CreateReservationDto createReservationDto)
        {
            var currentUserId = GetUserId();

            if (currentUserId != createReservationDto.UserId)
                throw new Exception("Not authorized");

            if (createReservationDto.SalonId <= 0)
                throw new Exception("Salon doesn't exist.");

            if (createReservationDto.SalonServiceId <= 0)
                throw new Exception("Salon service doesn't exist.");

            if (!HelperDateTimeValidation.CheckReservationDateAndTime(createReservationDto.ReservationDay, createReservationDto.ReservationHour))
                throw new Exception("Reservation day/hour is not correct.");

            return await _reservationRepository.MakeReservation(createReservationDto);
        }

        public Task<List<GetReservationDetailsCustomerDto>> GetAllCustomerReservations(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<GetReservationDetailsCustomerDto> GetCustomerReservationDetails(int customerId, int reservationId)
        {
            throw new NotImplementedException();
        }

    }
}
