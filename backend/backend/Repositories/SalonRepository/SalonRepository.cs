using backend.Data;
using backend.Dtos.Auth;
using backend.Dtos.Salon;
using backend.Dtos.SalonService;
using backend.Helpers;
using backend.Models.Salon;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.SalonRepository
{
    public class SalonRepository : HelperInputValidationRegex, ISalonRepository
    {
        #region Private Fields
        private readonly ApplicationDbContext _context;
        #endregion

        #region Public Constructor
        public SalonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Salon Methods
        public async Task<GetSingleSalonDto> CreateNewSalon(CreateNewSalonDto newSalonDetails)
        {
            var userFromDb = await _context.Users.FirstOrDefaultAsync(u => u.Id == newSalonDetails.UserId);

            if (userFromDb is null)
            {
                throw new Exception("You must be authenticate to become an affiliate.");
            }

            var userHasSalon = await _context.Salons.AnyAsync(s => s.UserId == newSalonDetails.UserId);
            if (userHasSalon)
            {
                throw new Exception("User already has a salon.");
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

        public async Task<GetSingleSalonDto> UpdateSalon(UpdateSalonDto updateSalonDto)
        {
            var userFromDb = await _context.Users.FirstOrDefaultAsync(u => u.Id == updateSalonDto.UserId);
            if (userFromDb is null)
            {
                throw new Exception("You must be authenticated to be able to update salon's information.");
            }

            var existingSalon = await _context.Salons.FirstOrDefaultAsync(s => s.Id == updateSalonDto.Id);
            if (existingSalon is null)
            {
                throw new Exception("Salon was not found.");
            }

            var salonAlreadyExists = await _context.Salons.AnyAsync(s => s.SalonName == updateSalonDto.SalonName);
            if (!salonAlreadyExists)
            {
                existingSalon.SalonName = updateSalonDto.SalonName;
            }

            existingSalon.SalonAddress = updateSalonDto.SalonAddress;
            existingSalon.SalonCity = updateSalonDto.SalonAddress;
            existingSalon.WorkDays = updateSalonDto.SalonAddress;
            existingSalon.WorkHoursInterval = updateSalonDto.SalonAddress;

            await _context.SaveChangesAsync();

            return new GetSingleSalonDto
            {
                SalonName = existingSalon.SalonName,
                SalonCity = existingSalon.SalonCity,
                SalonAddress = existingSalon.SalonAddress,
                WorkDays = existingSalon.WorkDays,
                WorkHoursInterval = existingSalon.WorkHoursInterval,
                SalonReviews = existingSalon.SalonReviews,
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

        public async Task<string> SetWorkDays(SetWorkDaysDto workDaysDto)
        {
            var salonFromDb = await _context.Salons.FirstOrDefaultAsync(s =>  s.Id == workDaysDto.SalonId && s.UserId == workDaysDto.UserId);

            if (salonFromDb is null)
                throw new Exception("Salon doesn't exist or you are not authorized to make this change.");

            string[] workDaysArray = { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday", };
            var workDaysLower = workDaysDto.WorkDays.ToLower();
            var stringIsValid = CheckWorkDaysInput(workDaysLower);

            if (stringIsValid)
            {
                salonFromDb.WorkDays = workDaysLower;
                _context.Salons.Update(salonFromDb);
                await _context.SaveChangesAsync();

                return "Work days updated successfully";
            }

            return "Work days of salon not updated because the pattern is not respected.";
        }
        #endregion

        #region SalonService Methods
        public async Task<GetSalonServiceDto> CreateNewSalonService(CreateSalonServiceDto createSalonServiceDto)
        {
            var salonFromDb = await _context.Salons.FirstOrDefaultAsync(s => s.Id == createSalonServiceDto.SalonId);
            if (salonFromDb is null)
                throw new Exception("You can't create a service for a non-existing salon");

            if (salonFromDb.UserId != createSalonServiceDto.UserId)
                throw new Exception("You are not authorized to create this service.");

            var salonService = await _context.SalonServices.AnyAsync(ss => ss.SalonId == createSalonServiceDto.SalonId && ss.ServiceName == createSalonServiceDto.ServiceName);
            if (salonService)
                throw new Exception("A simillar service already exists.");

            var newSalonService = new Models.Salon.SalonService()
            {
                SalonId = createSalonServiceDto.SalonId,
                ServiceName = createSalonServiceDto.ServiceName,
                Price = createSalonServiceDto.Price,
            };

            _context.SalonServices.Add(newSalonService);
            await _context.SaveChangesAsync();

            return new GetSalonServiceDto
            {
                ServiceName = createSalonServiceDto.ServiceName,
                Price = createSalonServiceDto.Price,
            };
        }

        public async Task<GetSalonServiceDto> GetSingleSalonService(string salonServiceName, int salonId)
        {
            var salonServiceFromDbo = await _context.SalonServices.FirstOrDefaultAsync(ss => ss.SalonId == salonId && ss.ServiceName == salonServiceName);
            if (salonServiceFromDbo is null)
                throw new Exception($"Service with name '{salonServiceName}' doesn't exist");

            return new GetSalonServiceDto
            {
                ServiceName = salonServiceFromDbo.ServiceName,
                Price = salonServiceFromDbo.Price
            };
        }

        public async Task<List<GetSalonServiceDto>> GetAllSalonServices(int salonId)
        {
            var salonServicesFromDb = await _context.SalonServices
                .Where(ss => ss.SalonId == salonId)
                .ToListAsync();

            var salonServicesList = salonServicesFromDb.Select(ss => new GetSalonServiceDto
            {
                ServiceName = ss.ServiceName,
                Price = ss.Price
            }).ToList();

            return salonServicesList;
        }

        public async Task<GetSalonServiceDto> UpdateSalonService(UpdateSalonServiceDto updateSalonServiceDto)
        {
            var salonServiceFromDb = await _context.SalonServices.FirstOrDefaultAsync(ss => ss.Id == updateSalonServiceDto.Id);
            if (salonServiceFromDb is null)
                throw new Exception("Salon service doesn't exist.");

            var serviceNameExists = await _context.SalonServices
                .AnyAsync(ss => ss.SalonId == updateSalonServiceDto.SalonId && ss.ServiceName == updateSalonServiceDto.ServiceName);
            
            if (!serviceNameExists)
            {
                salonServiceFromDb.ServiceName = updateSalonServiceDto.ServiceName;
            }

            salonServiceFromDb.Price = updateSalonServiceDto.Price;

            _context.SalonServices.Update(salonServiceFromDb);
            await _context.SaveChangesAsync();

            return new GetSalonServiceDto
            {
                ServiceName = updateSalonServiceDto.ServiceName,
                Price = updateSalonServiceDto.Price
            };
        }

        public async Task<bool> DeleteSalonService(DeleteSalonServiceDto deleteSalonServiceDto)
        {
            var salonServiceFromDb = await _context.SalonServices.
                FirstOrDefaultAsync(ss => ss.ServiceName == deleteSalonServiceDto.ServiceName && ss.SalonId == deleteSalonServiceDto.SalonId);

            if (salonServiceFromDb is null)
                return false;

            _context.SalonServices.Remove(salonServiceFromDb);
            await _context.SaveChangesAsync();

            return true;
        }


        #endregion
    }
}
