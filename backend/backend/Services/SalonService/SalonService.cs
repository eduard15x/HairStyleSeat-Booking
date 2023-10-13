using backend.Dtos.Salon;
using backend.Dtos.SalonService;
using backend.Repositories.SalonRepository;
using System.Security.Claims;

namespace backend.Services.SalonService
{
    public class SalonService : ISalonService
    {
        #region Private Fields
        private readonly ISalonRepository _salonRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        #endregion

        #region Public Constructor
        public SalonService(ISalonRepository salonRepository, IHttpContextAccessor httpContextAccessor)
        {
            _salonRepository = salonRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Salon
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
        #endregion

        #region SalonService
        public async Task<GetSalonServiceDto> CreateNewSalonService(CreateSalonServiceDto createSalonServiceDto)
        {
            var currentUserId = GetUserId();

            if (createSalonServiceDto.UserId <= 0 || createSalonServiceDto.UserId != currentUserId)
                throw new Exception("User not authorized.");

            if (createSalonServiceDto.SalonId <= 0)
                throw new Exception("Salon doesn't exist.");

            if (createSalonServiceDto.Price <= 0)
                throw new Exception("Price must be greater than 0.");

            return await _salonRepository.CreateNewSalonService(createSalonServiceDto);
        }

        public async Task<GetSalonServiceDto> GetSingleSalonService(string salonServiceName, int salonId)
        {
            if (salonId <= 0)
                throw new Exception("Salon doesn't exists.");

            if (string.IsNullOrEmpty(salonServiceName) || string.IsNullOrWhiteSpace(salonServiceName))
                throw new Exception("Salon service name must have a value.");

            return await _salonRepository.GetSingleSalonService(salonServiceName);
        }

        public async Task<List<GetSalonServiceDto>> GetAllSalonServices(int salonId)
        {
            if (salonId <= 0)
                throw new Exception("Salon doesn't exist.");

            return await _salonRepository.GetAllSalonServices(salonId);
        }

        public async Task<GetSalonServiceDto> UpdateSalonService(UpdateSalonServiceDto updateSalonServiceDto)
        {
            var currentUserId = GetUserId();

            if (currentUserId != updateSalonServiceDto.UserId || updateSalonServiceDto.UserId <= 0)
                throw new Exception("Not authorized");

            if (updateSalonServiceDto.SalonId <= 0)
                throw new Exception("Salon doesn't exist.");

            if (string.IsNullOrEmpty(updateSalonServiceDto.ServiceName))
                throw new Exception("Service can not be null.");

            return await _salonRepository.UpdateSalonService(updateSalonServiceDto);
        }

        public async Task<bool> DeleteSalonService(DeleteSalonServiceDto deleteSalonServiceDto)
        {
            var currentUserId = GetUserId();

            if (currentUserId != deleteSalonServiceDto.UserId || deleteSalonServiceDto.UserId <= 0)
                throw new Exception("Not authorized");

            if (deleteSalonServiceDto.SalonId <= 0)
                throw new Exception("Salon doesn't exist.");

            if (String.IsNullOrEmpty(deleteSalonServiceDto.ServiceName))
                throw new Exception("Service must have an name.");

            var response = await _salonRepository.DeleteSalonService(deleteSalonServiceDto);

            if (!response)
                throw new Exception("Salon doesn't exist or could not be deleted.");

            return response;
        }
        #endregion
    }
}
