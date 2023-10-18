using backend.Data;
using backend.Dtos.Reservation;
using backend.Models.Reservation;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.ReservationRepository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ReservationRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<GetReservationDetailsCustomerDto> MakeReservation(CreateReservationDto createReservationDto)
        {
            var salonFromDb = await _applicationDbContext.Salons.FirstOrDefaultAsync(s => s.Id == createReservationDto.SalonId);

            if (salonFromDb is null)
                throw new Exception("Salon doesn't exist.");

            if (salonFromDb.SalonStatus == 2 || salonFromDb.SalonStatus == 3)
                throw new Exception("Salon status is not OK.");

            var salonServiceFromDb = await _applicationDbContext.SalonServices.FirstOrDefaultAsync(ss => ss.Id == createReservationDto.SalonServiceId);

            if (salonServiceFromDb is null)
                throw new Exception("Salon's service doesn't exist.");

            var newReservation = new Reservation
            {
                UserId = createReservationDto.UserId,
                SalonId = createReservationDto.SalonId,
                SalonServiceId = createReservationDto.SalonServiceId,
                ServiceName = salonServiceFromDb.ServiceName,
                Price = salonServiceFromDb.Price,
                HaircutDurationTime = salonServiceFromDb.HaircutDurationTime,
                ReservationDay = createReservationDto.ReservationDay,
                ReservationHour = createReservationDto.ReservationHour,
                BookSubmitDate = DateTime.Now,
            };

            _applicationDbContext.Reservations.Add(newReservation);
            await _applicationDbContext.SaveChangesAsync();

            return new GetReservationDetailsCustomerDto
            {
                ReservationId = newReservation.Id,
                SalonName = salonFromDb.SalonName,
                SalonCity = salonFromDb.SalonCity,
                SalonServiceId = salonServiceFromDb.Id,
                ServiceName = salonServiceFromDb.ServiceName,
                Price = salonServiceFromDb.Price,
                HaircutDurationTime = salonServiceFromDb.HaircutDurationTime,
                ReservationDay = createReservationDto.ReservationDay,
                ReservationHour = createReservationDto.ReservationHour,
            };
        }

        public async Task<List<GetReservationDetailsCustomerDto>> GetAllCustomerReservations(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<GetReservationDetailsCustomerDto> GetCustomerReservationDetails(int customerId, int reservationId)
        {
            throw new NotImplementedException();
        }
    }
}
