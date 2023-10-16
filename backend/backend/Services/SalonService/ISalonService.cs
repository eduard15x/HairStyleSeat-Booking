using backend.Dtos.Salon;
using backend.Dtos.SalonService;

namespace backend.Services.SalonService
{
    public interface ISalonService
    {
        #region Salon
        Task<GetSingleSalonDto> CreateNewSalon(CreateNewSalonDto newSalonDetails);
        Task<GetSingleSalonDto> UpdateSalon(UpdateSalonDto updatedSalonDto);
        Task<dynamic> GetAllSalons();
        Task<GetSingleSalonDto> GetSingleSalonDetails(int salonId);
        Task<string> SetWorkDays(SetWorkDaysDto workDaysDto);
        #endregion

        #region SalonService
        Task<GetSalonServiceDto> CreateNewSalonService(CreateSalonServiceDto createSalonServiceDto);
        Task<GetSalonServiceDto> GetSingleSalonService(string salonServiceName, int salonId);
        Task<List<GetSalonServiceDto>> GetAllSalonServices(int salonId);
        Task<GetSalonServiceDto> UpdateSalonService(UpdateSalonServiceDto updateSalonServiceDto);
        Task<bool> DeleteSalonService(DeleteSalonServiceDto deleteSalonServiceDto);
        #endregion
    }
}
