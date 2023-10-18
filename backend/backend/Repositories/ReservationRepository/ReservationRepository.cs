using backend.Data;
using backend.Dtos.Reservation;
using backend.Models;
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

            if (salonFromDb.StatusId == 2 || salonFromDb.StatusId == 3)
                throw new Exception("Salon status is not OK.");

            var salonServiceFromDb = await _applicationDbContext.SalonServices.FirstOrDefaultAsync(ss => ss.Id == createReservationDto.SalonServiceId);

            if (salonServiceFromDb is null)
                throw new Exception("Salon's service doesn't exist.");

            var newReservation = new Reservation
            {
                UserId = createReservationDto.UserId,
                SalonId = createReservationDto.SalonId,
                ServiceId = createReservationDto.SalonServiceId,
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
            //var customerReservationsFromDb = await _applicationDbContext.Reservations
            //    .Where(r => r.UserId == customerId)
            //    .Include(r => r.SalonServiceId) // Include SalonService for the join
            //    .ThenInclude(s => s.Salon)  // Include Salon for the join
            //    .ToListAsync();

            //if (customerReservationsFromDb.Count == 0 || customerReservationsFromDb is null)
            //    throw new Exception("No reservations available.");

            //var customerReservationsList = customerReservationsFromDb
            //    .Select(reservation => new GetReservationDetailsCustomerDto
            //    {
            //        ReservationId = reservation.Id,
            //        SalonServiceId = reservation.SalonServiceId,
            //        SalonName = ,
            //        SalonCity = ,
            //        ServiceName = reservation.ServiceName,
            //        Price = reservation.Price, 
            //        HaircutDurationTime = reservation.HaircutDurationTime,
            //        ReservationDay = reservation.ReservationDay,
            //        ReservationHour = reservation.ReservationHour
            //    })
            //    .ToList();


            //return customerReservationsList;
            return null;
        }

        public Task<GetReservationDetailsCustomerDto> GetCustomerReservationDetails(int customerId, int reservationId)
        {
            throw new NotImplementedException();
        }
    }
}
