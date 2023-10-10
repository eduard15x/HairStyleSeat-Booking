using backend.Dtos.Salon;

namespace backend.Services.SalonService
{
    public interface ISalonService
    {
        Task<GetSingleSalonDto> CreateNewSalon(CreateNewSalonDto newSalonDetails);
        Task<GetSingleSalonDto> UpdateSalon(UpdateSalonDto updatedSalonDto);
        Task<dynamic> GetAllSalons();
        Task<GetSingleSalonDto> GetSingleSalonDetails(int salonId);
    }
}
