﻿using backend.Data;
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
            existingSalon.SalonCity = updateSalonDto.SalonCity;
            existingSalon.WorkDays = updateSalonDto.WorkDays;
            existingSalon.StartTimeHour = updateSalonDto.StartTimeHour;
            existingSalon.EndTimeHour = updateSalonDto.EndTimeHour;

            await _context.SaveChangesAsync();

            return new GetSingleSalonDto
            {
                SalonId = existingSalon.Id,
                SalonName = existingSalon.SalonName,
                SalonCity = existingSalon.SalonCity,
                SalonAddress = existingSalon.SalonAddress,
                WorkDays = existingSalon.WorkDays,
                SalonStatus = existingSalon.StatusId,
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

        public async Task<GetSalonListDto> GetAllSalons(int page, int pageSize, string search, string selectedCities)
        {
            var query = _context.Salons
                .Where(s => s.StatusId == 1 || s.StatusId == 4);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.SalonName.Contains(search));
            }

            if (!string.IsNullOrEmpty(selectedCities))
            {
                var cities = selectedCities.Split(',').Select(city => city.Trim());
                query = query.Where(s => cities.Contains(s.SalonCity));
            }

            var totalItemCount = await query.CountAsync();

            var salonsListDb = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new GetSalonListDto {
                Salons = salonsListDb,
                TotalSalons = totalItemCount
            };
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
                SalonId = salonDetails.Id,
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
                EndTimeHour = salonDetails.EndTimeHour,
                SalonStatus = salonDetails.StatusId
            };
        }
        public async Task<GetSingleSalonDto> GetSingleSalonDetailsForUser(int currentUserId)
        {
            var salonDetails = await _context.Salons
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.UserId == currentUserId);

            if (salonDetails == null)
            {
                return null;
            }

            return new GetSingleSalonDto
            {
                SalonId = salonDetails.Id,
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
                EndTimeHour = salonDetails.EndTimeHour,
                SalonStatus = salonDetails.StatusId
            };
        }

        public async Task<string> SetWorkDays(SetWorkDaysDto workDaysDto)
        {
            var salonFromDb = await _context.Salons.FirstOrDefaultAsync(s =>  s.Id == workDaysDto.SalonId && s.UserId == workDaysDto.UserId);

            if (salonFromDb is null)
                throw new Exception("Salon doesn't exist or you are not authorized to make this change.");

            salonFromDb.WorkDays = workDaysDto.WorkDays;
            _context.Salons.Update(salonFromDb);
            await _context.SaveChangesAsync();

            return "Work days of salon updated successfully.";
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

        public async Task<string> ReviewSalon(ReviewSalonDto reviewSalonDto)
        {
            var salonFromDb = await _context.Salons
                .FirstOrDefaultAsync(s => s.Id == reviewSalonDto.SalonId);
            if (salonFromDb is null)
                throw new Exception("Salon doesn't exist.");

            var userHasAnyReservationCompleted = await _context.Reservations
                .AnyAsync(r => r.UserId == reviewSalonDto.UserId && r.SalonId == reviewSalonDto.SalonId && r.CompletedReservation == 1);
            if (!userHasAnyReservationCompleted)
                throw new Exception("You can't review a salon until complete a reservation.");

            var userAlreadyReviewSalon = await _context.Reviews
                .AnyAsync(r => r.UserId == reviewSalonDto.UserId && r.SalonId == reviewSalonDto.SalonId);
            if (userAlreadyReviewSalon)
                throw new Exception("User already review this salon.");

            var newReview = new Review
            {
                UserId = reviewSalonDto.UserId,
                SalonId = reviewSalonDto.SalonId,
                ReviewRating = reviewSalonDto.ReviewRating,
                ReviewMessage = reviewSalonDto.ReviewMessage
            };

            _context.Reviews.Add(newReview);
            await _context.SaveChangesAsync();

            var salonReviews = await _context.Reviews
                .Where(s => s.SalonId == reviewSalonDto.SalonId)
                .ToListAsync();

            var ratingReviewsSum = 0;
            decimal averageRatingFromReviews = 0;

            foreach (var review in salonReviews)
            {
                ratingReviewsSum += review.ReviewRating;
            }

            averageRatingFromReviews = (decimal)ratingReviewsSum / salonReviews.Count;

            salonFromDb.SalonReviews = averageRatingFromReviews;
            _context.Salons.Update(salonFromDb);
            await _context.SaveChangesAsync();

            return "Your review was submitted.";
        }

        public async Task<string> ReportUser(ReportUserDto reportUserDto)
        {
            var userFromDb = await _context.Users.FirstOrDefaultAsync(ru => ru.Id ==  reportUserDto.UserId);
            if (userFromDb is null)
                throw new Exception("User not found");

            var salonFromDb = await _context.Salons.FirstOrDefaultAsync(s => s.Id == reportUserDto.SalonId);
            if (salonFromDb is null)
                throw new Exception("Salon not found");

            var userHadAnyReservation = await _context.Reservations
                .AnyAsync(r => r.UserId == reportUserDto.UserId && r.SalonId == reportUserDto.SalonId);

            if (!userHadAnyReservation)
                throw new Exception("You can not report an user if he hasn't had no reservation for your salon.");

            var userIsAlreadyReportedBySalon = await _context.Reports
                .AnyAsync(r => r.UserId == reportUserDto.UserId && r.SalonId == reportUserDto.SalonId);

            if (userIsAlreadyReportedBySalon)
                throw new Exception("User is already reported by your salon.");

            var newReport = new Report
            {
                UserId = reportUserDto.UserId,
                SalonId = reportUserDto.SalonId,
                ReportMessage = reportUserDto.ReportMessage,
            };

            _context.Reports.Add(newReport);
            var usersTotalReports = userFromDb.Reports + 1;
            userFromDb.Reports = usersTotalReports;

            if (usersTotalReports >= 5)
            {
                userFromDb.Suspended = 1;
            }

            _context.Users.Update(userFromDb);
            await _context.SaveChangesAsync();

            return "User was reported.";
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
                ServiceId = ss.Id,
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
