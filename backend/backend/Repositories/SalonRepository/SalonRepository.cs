using backend.Data;
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
            };

            _context.Salons.Add(newSalon);
            await _context.SaveChangesAsync();

            return new GetSingleSalonDto
            {
                SalonName = newSalonDetails.SalonName,
                SalonCity = newSalonDetails.SalonCity,
                SalonAddress = newSalonDetails.SalonAddress,
                HairstylistName = userFromDb.UserName
            };
        }

        public Task<List<GetSingleSalonDto>> GetAllSalons(FilterSalonDto filterSalonDetails)
        {
            throw new NotImplementedException();
        }

        public Task<GetSingleSalonDto> GetSingleSalonDetails(int salonId)
        {
            throw new NotImplementedException();
        }
    }
}
