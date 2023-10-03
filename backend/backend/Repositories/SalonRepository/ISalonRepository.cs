using backend.Dtos.Salon;

namespace backend.Repositories.SalonRepository
{
    public interface ISalonRepository
    {
        Task<GetSingleSalonDto> CreateNewSalon(CreateNewSalonDto newSalonDetails);
        Task<List<GetSingleSalonDto>> GetAllSalons(FilterSalonDto filterSalonDetails);
        Task<GetSingleSalonDto> GetSingleSalonDetails(int salonId);
    }
}
