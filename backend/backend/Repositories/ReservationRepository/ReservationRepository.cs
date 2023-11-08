using backend.Data;
using backend.Dtos.Reservation;
using backend.Dtos.Salon;
using backend.Helpers;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

            if (!HelperDateTimeValidation.CheckHourWithinInterval(createReservationDto.ReservationHour, salonFromDb.StartTimeHour, salonFromDb.EndTimeHour))
                throw new Exception($"Reservation hour is not in the salon's interval program. ({salonFromDb.StartTimeHour}-{salonFromDb.EndTimeHour})");

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
            var userFromDb = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == customerId);
            if (userFromDb is null)
                throw new Exception("User doesn't exists");

            var customerReservationsFromDb = await _applicationDbContext.Reservations
                .Where(r => r.UserId == customerId)
                .Include(r => r.Salon) // Include SalonService for the join
                //.ThenInclude(s => s.Services)  // Include Salon for the join
                .ToListAsync();

            if (customerReservationsFromDb.Count == 0 || customerReservationsFromDb is null)
                return new List<GetReservationDetailsCustomerDto>();

            var customerReservationsList = customerReservationsFromDb
                .Select(reservation => new GetReservationDetailsCustomerDto
                {
                    ReservationId = reservation.Id,
                    SalonServiceId = reservation.ServiceId,
                    SalonName = reservation.Salon.SalonName,
                    SalonCity = reservation.Salon.SalonCity,
                    ServiceName = reservation.ServiceName,
                    Price = reservation.Price,
                    HaircutDurationTime = reservation.HaircutDurationTime,
                    ReservationDay = reservation.ReservationDay,
                    ReservationHour = reservation.ReservationHour
                })
                .ToList();

            return customerReservationsList;
        }

        public async Task<GetSalonReservationListDto> GetAllSalonReservations(int salonAffiliateId, int page, int pageSize)
        {
            var userFromDb = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == salonAffiliateId);
            if (userFromDb is null)
                throw new Exception("User doesn't exists");
            
            if (userFromDb.Role == "customer")
                throw new Exception("Not authorized");

            var salonFromDb = await _applicationDbContext.Salons.FirstOrDefaultAsync(s => s.UserId == salonAffiliateId);
            if (salonFromDb is null)
                throw new Exception("Salon not available for current user.");

            var query = _applicationDbContext.Reservations
                .Where(r => r.SalonId == salonFromDb.Id)
                .Include(r => r.Salon);

            var totalItemCount = await query.CountAsync();

            if (totalItemCount == 0)
                return new GetSalonReservationListDto
                {
                    Reservations = new List<GetReservationDetailsForSalonDto>(),
                    TotalReservations = totalItemCount
                };

            var salonReservationsFromDb = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var customerReservationsList = salonReservationsFromDb
                .Select(reservation => new GetReservationDetailsForSalonDto
                {
                    ReservationId = reservation.Id,
                    UserId = reservation.UserId,
                    SalonId = reservation.SalonId,
                    SalonServiceId = reservation.ServiceId,
                    ServiceName = reservation.ServiceName,
                    Price = reservation.Price,
                    HaircutDurationTime = reservation.HaircutDurationTime,
                    ReservationDay = reservation.ReservationDay,
                    ReservationHour = reservation.ReservationHour,
                    IsReservationCompleted = reservation.CompletedReservation,
                    ReservationSubmittedDate = reservation.BookSubmitDate
                })
                .ToList();

            return new GetSalonReservationListDto
            {
                Reservations = customerReservationsList,
                TotalReservations = totalItemCount
            };
        }

        public async Task<GetReservationDetailsCustomerDto> GetCustomerReservationDetails(int customerId, int reservationId)
        {
            var userFromDb = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == customerId);
            if (userFromDb is null)
                throw new Exception("User doesn't exists");

            var reservationFromDb = await _applicationDbContext.Reservations
                .Include(r => r.Salon)
                .FirstOrDefaultAsync(r => r.Id == reservationId);
            if (reservationFromDb is null)
                throw new Exception("Reservation doesn't exists");

            var rxe = reservationFromDb.UserId;

            if (reservationFromDb.UserId != customerId)
                throw new Exception("No reservation associated for current user.");

            return new GetReservationDetailsCustomerDto
            {
                ReservationId = reservationFromDb.Id,
                SalonName = reservationFromDb.Salon.SalonName,
                SalonCity = reservationFromDb.Salon.SalonCity,
                SalonServiceId = reservationFromDb.ServiceId,
                ServiceName = reservationFromDb.ServiceName,
                Price = reservationFromDb.Price,
                HaircutDurationTime = reservationFromDb.HaircutDurationTime,
                ReservationDay = reservationFromDb.ReservationDay,
                ReservationHour = reservationFromDb.ReservationHour,
            };
        }

        public async Task<string> ConfirmReservation(int reservationId, int salonOwnerId)
        {
            var reservationFromDb = await _applicationDbContext.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId);
            if (reservationFromDb is null)
                throw new Exception("Reservation not found.");

            if (reservationFromDb.CompletedReservation == 1)
                throw new Exception("Reservation is already completed.");

            reservationFromDb.CompletedReservation = 1;
            _applicationDbContext.Update(reservationFromDb);
            await _applicationDbContext.SaveChangesAsync();

            return "Reservation completed.";
        }

        public async Task<string> CancelReservation(int reservationId, int customerId)
        {
            var reservationFromDb = await _applicationDbContext.Reservations
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservationFromDb is null)
                throw new Exception("Reservation not found.");

            if (reservationFromDb.CompletedReservation == 1)
                throw new Exception("You can't cancel a completed reservation.");

            _applicationDbContext.Reservations.Remove(reservationFromDb);
            await _applicationDbContext.SaveChangesAsync();

            return "Reservation canceled.";
        }
    }
}
