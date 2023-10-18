using backend.Dtos.Salon;
using backend.Dtos.SalonService;
using backend.Models;

namespace backend.Repositories.SalonRepository
{
    public interface ISalonRepository
    {
        #region Salon
        Task<GetSingleSalonDto> CreateNewSalon(CreateNewSalonDto newSalonDetails);
        Task<GetSingleSalonDto> UpdateSalon(UpdateSalonDto updateSalonDto);
        Task<List<Salon>> GetAllSalons();
        Task<GetSingleSalonDto> GetSingleSalonDetails(int salonId);
        Task<string> SetWorkDays(SetWorkDaysDto workDaysDto);
        Task<bool> ModifySalonStatus(ModifySalonStatusDto modifySalonStatusDto);
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
