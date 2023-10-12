﻿using backend.Data;
using backend.Dtos.Auth;
using backend.Dtos.Salon;
using backend.Dtos.SalonService;
using backend.Models.Salon;
using backend.Services.SalonService;
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

            var salonAlreadyExists = await _context.Salons.AnyAsync(s => s.SalonName == updateSalonDto.SalonName);
            if (salonAlreadyExists)
            {
                throw new Exception("Salon with same name already exists.");
            }

            var existingSalon = await _context.Salons.FirstOrDefaultAsync(s => s.Id == updateSalonDto.Id);
            if (existingSalon is null)
            {
                throw new Exception("Salon was not found.");
            }

            existingSalon.SalonName = updateSalonDto.SalonName;
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

        #region SalonService
        public async Task<GetSalonServiceDto> CreateNewSalonService(CreateSalonServiceDto createSalonServiceDto)
        {
            var salonFromDb = await _context.Salons.FirstOrDefaultAsync(s => s.Id == createSalonServiceDto.SalonId);
            if (salonFromDb is null)
                throw new Exception("You can't create a service for a non-existing salon");

            if (salonFromDb.UserId != createSalonServiceDto.UserId)
                throw new Exception("You are not authorized to create this service.");

            var salonService = await _context.SalonServices.FirstOrDefaultAsync(ss => ss.SalonId == createSalonServiceDto.SalonId);
            if (salonService != null && salonService.ServiceName == createSalonServiceDto.ServiceName)
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
        #endregion
    }
}
