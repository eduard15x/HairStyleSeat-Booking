using backend.Data;
using backend.Dtos.Auth;
using backend.Dtos.Salon;
using backend.Dtos.SalonService;
using backend.Helpers;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.SalonRepository
{
    public class SalonRepository : ISalonRepository
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
                SalonReviews = newSalonDetails.SalonReviews,
                StartTimeHour = newSalonDetails.StartTimeHour,
                EndTimeHour = newSalonDetails.EndTimeHour
            };

            _context.Salons.Add(newSalon);
            await _context.SaveChangesAsync();

            return new GetSingleSalonDto
            {
                SalonName = newSalonDetails.SalonName,
                SalonCity = newSalonDetails.SalonCity,
                SalonAddress = newSalonDetails.SalonAddress,
                WorkDays = newSalonDetails.WorkDays,
                SalonReviews = newSalonDetails.SalonReviews,
                UserDetails = new UsersSalonsDetailsDto
                {
                    UserName = userFromDb.UserName,
                    Email = userFromDb.Email,
                    PhoneNumber = userFromDb.PhoneNumber
                },
                StartTimeHour = newSalonDetails.StartTimeHour,
                EndTimeHour = newSalonDetails.EndTimeHour
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
            existingSalon.StartTimeHour = updateSalonDto.StartTimeHour;
            existingSalon.EndTimeHour = updateSalonDto.EndTimeHour;

            await _context.SaveChangesAsync();

            return new GetSingleSalonDto
            {
                SalonName = existingSalon.SalonName,
                SalonCity = existingSalon.SalonCity,
                SalonAddress = existingSalon.SalonAddress,
                WorkDays = existingSalon.WorkDays,
                SalonReviews = existingSalon.SalonReviews,
                UserDetails = new UsersSalonsDetailsDto
                {
                    UserName = userFromDb.UserName,
                    Email = userFromDb.Email,
                    PhoneNumber = userFromDb.PhoneNumber
                },
                StartTimeHour = existingSalon.StartTimeHour,
                EndTimeHour = existingSalon.EndTimeHour,
            };
        }

        public async Task<List<Salon>> GetAllSalons()
        {
            var salonsListDb = await _context.Salons
                .Where(s => s.StatusId == 1 || s.StatusId == 4)
                .ToListAsync();

            if (salonsListDb.Count == 0 || salonsListDb is null)
            {
                throw new Exception("No salons in the list.");
            }

            return salonsListDb;
        }

        public async Task<GetSingleSalonDto> GetSingleSalonDetails(int salonId)
        {
            var salonDetails = await _context.Salons
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == salonId && (s.StatusId == 1 || s.StatusId == 4));

            if (salonDetails is null)
            {
                throw new Exception("Salon doesn't exist");
            }

            return new GetSingleSalonDto
            {
                SalonName = salonDetails.SalonName,
                SalonCity = salonDetails.SalonCity,
                SalonAddress = salonDetails.SalonAddress,
                WorkDays = salonDetails.WorkDays,
                SalonReviews = salonDetails.SalonReviews,
                UserDetails = new UsersSalonsDetailsDto
                {
                    UserName = salonDetails.User.UserName,
                    Email = salonDetails.User.Email,
                    PhoneNumber = salonDetails.User.PhoneNumber,
                },
                StartTimeHour = salonDetails.StartTimeHour,
                EndTimeHour = salonDetails.EndTimeHour
            };
        }

        public async Task<string> SetWorkDays(SetWorkDaysDto workDaysDto)
        {
            var salonFromDb = await _context.Salons.FirstOrDefaultAsync(s =>  s.Id == workDaysDto.SalonId && s.UserId == workDaysDto.UserId);

            if (salonFromDb is null)
                throw new Exception("Salon doesn't exist or you are not authorized to make this change.");

            string[] workDaysArray = { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday", };
            var workDaysLower = workDaysDto.WorkDays.ToLower();
            var stringIsValid = HelperInputValidationRegex.CheckWorkDaysInput(workDaysLower);

            if (stringIsValid)
            {
                salonFromDb.WorkDays = workDaysLower;
                _context.Salons.Update(salonFromDb);
                await _context.SaveChangesAsync();

                return "Work days updated successfully";
            }

            return "Work days of salon not updated because the pattern is not respected.";
        }

        public async Task<bool> ModifySalonStatus(ModifySalonStatusDto modifySalonStatusDto)
        {
            var salonStatusCodeExists = await _context.SalonStatuses
                .AnyAsync(ss => ss.Id == modifySalonStatusDto.SalonStatusId);
            if (!salonStatusCodeExists)
                throw new Exception("Salon status code doesn't exist.");

            var salonFromDb = await _context.Salons
                .FirstOrDefaultAsync(s => s.Id == modifySalonStatusDto.SalonId && s.UserId == modifySalonStatusDto.SalonUserId);
            if (salonFromDb is null)
                throw new Exception("The salon doesn't exist or it doesn't belong to the current user.");

            if (salonFromDb.StatusId == modifySalonStatusDto.SalonStatusId)
                return false;

            salonFromDb.StatusId = modifySalonStatusDto.SalonStatusId;
            _context.Salons.Update(salonFromDb);

            var userWithCurrentSalon = await _context.Users.FirstOrDefaultAsync(u => u.Id == salonFromDb.UserId);

            switch(modifySalonStatusDto.SalonStatusId)
            {
                case 1:
                    userWithCurrentSalon!.Role = "affiliate";
                    break;
                case 2:
                    userWithCurrentSalon!.Role = "customer";
                    break;
                case 3:
                    userWithCurrentSalon!.Role = "customer";
                    break;
                case 4:
                    userWithCurrentSalon!.Role = "affiliate";
                    break;
            }

            _context.Users.Update(userWithCurrentSalon!);
            
            await _context.SaveChangesAsync();
            return true;
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

            var newSalonService = new Service()
            {
                SalonId = createSalonServiceDto.SalonId,
                ServiceName = createSalonServiceDto.ServiceName,
                Price = createSalonServiceDto.Price,
                HaircutDurationTime = createSalonServiceDto.HaircutDurationTime
            };

            _context.SalonServices.Add(newSalonService);
            await _context.SaveChangesAsync();

            return new GetSalonServiceDto
            {
                ServiceName = createSalonServiceDto.ServiceName,
                Price = createSalonServiceDto.Price,
                HaircutDurationTime = createSalonServiceDto.HaircutDurationTime
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
                Price = salonServiceFromDbo.Price,
                HaircutDurationTime = Convert.ToString((Convert.ToInt32(salonServiceFromDbo.HaircutDurationTime)) / 60) + " minutes"
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
                Price = ss.Price,
                HaircutDurationTime = Convert.ToString((Convert.ToInt32(ss.HaircutDurationTime)) / 60) + " minutes"
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
                Price = updateSalonServiceDto.Price,
                HaircutDurationTime = Convert.ToString((Convert.ToInt32(updateSalonServiceDto.HaircutDurationTime)) / 60) + " minutes"
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
