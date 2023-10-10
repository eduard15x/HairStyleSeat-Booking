using backend.Dtos.Salon;
using backend.Models.Salon;
using backend.Repositories.SalonRepository;
using System.Security.Claims;

namespace backend.Services.SalonService
{
    public class SalonService : ISalonService
    {
        private readonly ISalonRepository _salonRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SalonService(ISalonRepository salonRepository, IHttpContextAccessor httpContextAccessor)
        {
            _salonRepository = salonRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<GetSingleSalonDto> CreateNewSalon(CreateNewSalonDto newSalonDetails)
        {
            var currentUser = GetUserId();
            if (newSalonDetails.UserId != currentUser || currentUser == 0)
            {
                throw new Exception("Not authorized!");
            }

            return await _salonRepository.CreateNewSalon(newSalonDetails);
        }

        public async Task<GetSingleSalonDto> UpdateSalon(UpdateSalonDto updatedSalonDto)
        {
            var currentUser = GetUserId();

            if (updatedSalonDto.UserId != currentUser || currentUser == 0)
            {
                throw new Exception("Not authorized!");
            }

            var updatedSalon = new UpdateSalonDto()
            {
                Id = updatedSalonDto.Id,
                SalonName = updatedSalonDto.SalonName,
                SalonAddress = updatedSalonDto.SalonAddress,
                SalonCity = updatedSalonDto.SalonCity,
                UserId = updatedSalonDto.UserId,
                WorkDays = updatedSalonDto.WorkDays,
                WorkHoursInterval = updatedSalonDto.WorkHoursInterval,
            };

            return await _salonRepository.UpdateSalon(updatedSalon);
        }

        public async Task<dynamic> GetAllSalons()
        {
            return await _salonRepository.GetAllSalons();
        }

        public async Task<GetSingleSalonDto> GetSingleSalonDetails(int salonId)
        {
            if (salonId <= 0)
            {
                throw new Exception("Salon not found");
            }

            return await _salonRepository.GetSingleSalonDetails(salonId);
        }
    }
}
