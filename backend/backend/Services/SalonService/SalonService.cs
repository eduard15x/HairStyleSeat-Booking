using backend.Dtos.Salon;
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

        public async Task<List<GetSingleSalonDto>> GetAllSalons(FilterSalonDto filterSalonDetails)
        {
            throw new NotImplementedException();
        }

        public async Task<GetSingleSalonDto> GetSingleSalonDetails(int salonId)
        {
            throw new NotImplementedException();
        }
    }
}
