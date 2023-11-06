using backend.Dtos.Salon;
using backend.Dtos.SalonService;

namespace backend.Services.SalonService
{
    public interface ISalonService
    {
        #region Salon
        Task<GetSingleSalonDto> CreateNewSalon(CreateNewSalonDto newSalonDetails);
        Task<GetSingleSalonDto> UpdateSalon(UpdateSalonDto updatedSalonDto);
        Task<GetSalonListDto> GetAllSalons(int page, int pageSize, string search, string selectedCities);
        Task<GetSingleSalonDto> GetSingleSalonDetails(int salonId);
        Task<GetSingleSalonDto> GetSingleSalonDetailsForUser();
        Task<string> SetWorkDays(SetWorkDaysDto workDaysDto);
        Task<string> ModifySalonStatus(ModifySalonStatusDto modifySalonStatusDto);
        Task<string> ReviewSalon(ReviewSalonDto reviewSalonDto);
        Task<string> ReportUser(ReportUserDto reportUserDto);
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
