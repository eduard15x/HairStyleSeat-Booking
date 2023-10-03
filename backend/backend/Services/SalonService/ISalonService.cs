using backend.Dtos.Salon;

namespace backend.Services.SalonService
{
    public interface ISalonService
    {
        Task<GetSingleSalonDto> CreateNewSalon(CreateNewSalonDto newSalonDetails);
        Task<List<GetSingleSalonDto>> GetAllSalons(FilterSalonDto filterSalonDetails);
        Task<GetSingleSalonDto> GetSingleSalonDetails(int salonId);
    }
}
