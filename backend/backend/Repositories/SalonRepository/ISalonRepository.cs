using backend.Dtos.Salon;
using backend.Dtos.SalonService;

namespace backend.Repositories.SalonRepository
{
    public interface ISalonRepository
    {
        #region Salon
        Task<GetSingleSalonDto> CreateNewSalon(CreateNewSalonDto newSalonDetails);
        Task<GetSingleSalonDto> UpdateSalon(UpdateSalonDto updateSalonDto);
        Task<GetSalonListDto> GetAllSalons(int page, int pageSize, string search, string salonCities);
        Task<GetSingleSalonDto> GetSingleSalonDetails(int salonId);
        Task<int> GetSingleSalonDetailsForUser(int currentUserId);
        Task<string> SetWorkDays(SetWorkDaysDto workDaysDto);
        Task<bool> ModifySalonStatus(ModifySalonStatusDto modifySalonStatusDto);
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
