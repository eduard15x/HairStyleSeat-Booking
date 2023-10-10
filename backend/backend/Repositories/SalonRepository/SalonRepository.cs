using backend.Data;
using backend.Dtos.Auth;
using backend.Dtos.Salon;
using backend.Models.Salon;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.SalonRepository
{
    public class SalonRepository : ISalonRepository
    {
        private readonly ApplicationDbContext _context;

        public SalonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetSingleSalonDto> CreateNewSalon(CreateNewSalonDto newSalonDetails)
        {
            var userFromDb = await _context.Users.FirstOrDefaultAsync(u => u.Id == newSalonDetails.UserId);

            if (userFromDb is null)
            {
                throw new Exception("You must be authenticate to become an affiliate.");
            }

            var salonAlreadyExists = await _context.Salons.AnyAsync(s => s.SalonName == newSalonDetails.SalonName);

            if (salonAlreadyExists)
            {
                throw new Exception("Salon with same name already exists.");
            }

            var newSalon =  new Salon
            {
                SalonName = newSalonDetails.SalonName,
                SalonCity = newSalonDetails.SalonCity,
                SalonAddress = newSalonDetails.SalonAddress,
                UserId = newSalonDetails.UserId,
                WorkDays = newSalonDetails.WorkDays,
                WorkHoursInterval = newSalonDetails.WorkHoursInterval,
                SalonReviews = newSalonDetails.SalonReviews
            };

            _context.Salons.Add(newSalon);
            await _context.SaveChangesAsync();

            return new GetSingleSalonDto
            {
                SalonName = newSalonDetails.SalonName,
                SalonCity = newSalonDetails.SalonCity,
                SalonAddress = newSalonDetails.SalonAddress,
                WorkDays = newSalonDetails.WorkDays,
                WorkHoursInterval = newSalonDetails.WorkHoursInterval,
                SalonReviews = newSalonDetails.SalonReviews,
                UserDetails = new UsersSalonsDetailsDto
                {
                    UserName = userFromDb.UserName,
                    Email = userFromDb.Email,
                    PhoneNumber = userFromDb.PhoneNumber
                }
            };
        }

        public async Task<List<Salon>> GetAllSalons()
        {
            var salonsListDb = await _context.Salons.ToListAsync();

            if (salonsListDb.Count == 0 || salonsListDb is null)
            {
                throw new Exception("No salons in the list.");
            }

            return salonsListDb;
        }

        public async Task<GetSingleSalonDto> GetSingleSalonDetails(int salonId)
        {
            var salonDetails = await _context.Salons
                .Where(s => s.Id == salonId)
                .Select(u => new GetSingleSalonDto
                {
                    SalonName = u.SalonName,
                    SalonCity = u.SalonCity,
                    SalonAddress = u.SalonAddress,
                    WorkDays = u.WorkDays,
                    WorkHoursInterval = u.WorkHoursInterval,
                    SalonReviews = u.SalonReviews,
                    UserDetails = new UsersSalonsDetailsDto
                    {
                        UserName = u.User.UserName,
                        Email = u.User.Email,
                        PhoneNumber = u.User.PhoneNumber
                    }
                })
                .FirstOrDefaultAsync();

            if (salonDetails is null)
            {
                throw new Exception("Salon doesn't exist");
            }

            return salonDetails;
        }
    }
}
