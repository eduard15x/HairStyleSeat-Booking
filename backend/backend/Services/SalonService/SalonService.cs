using backend.Data;
using backend.Dtos.Salon;
using backend.Dtos.SalonService;
using backend.Helpers;
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
            // TODO validate salon status ID
        {
            var currentUser = GetUserId();
            if (newSalonDetails.UserId != currentUser || currentUser == 0)
            {
                throw new Exception("Not authorized!");
            }

            // repeated function
            var startTimeValid = HelperInputValidationRegex.CheckValidTimeFormat(newSalonDetails.StartTimeHour);
            var endTimeValid = HelperInputValidationRegex.CheckValidTimeFormat(newSalonDetails.EndTimeHour);

            if (!startTimeValid)
                throw new Exception("Start time hour format is not ok. Please respect the format as following: '00:30', '10:00', '07:30', '22:45'.");
            if (!endTimeValid)
                throw new Exception("End time hour format is not ok. Please respect the format as following: '00:30', '10:00', '07:30', '22:45'.");

            if (!HelperDateTimeValidation.CheckHourInterval(newSalonDetails.StartTimeHour, newSalonDetails.EndTimeHour))
                throw new Exception("Start time hour can not be higher than end time hour.");

            var workDaysLower = newSalonDetails.WorkDays.ToLower();
            var stringIsValid = HelperInputValidationRegex.CheckWorkDaysInput(workDaysLower);

            if (!stringIsValid)
            {
                throw new Exception("Work days not updated successfully. Pattern is incorrect. (\"monday,tuesday,saturday etc\")");
            }

            newSalonDetails.WorkDays = workDaysLower;
            return await _salonRepository.CreateNewSalon(newSalonDetails);
        }

        public async Task<GetSingleSalonDto> UpdateSalon(UpdateSalonDto updatedSalonDto)
        {
            var currentUser = GetUserId();

            if (updatedSalonDto.UserId != currentUser || currentUser == 0)
                throw new Exception("Not authorized!");

            // repeated function
            var startTimeValid = HelperInputValidationRegex.CheckValidTimeFormat(updatedSalonDto.StartTimeHour);
            var endTimeValid = HelperInputValidationRegex.CheckValidTimeFormat(updatedSalonDto.EndTimeHour);

            if (!startTimeValid)
                throw new Exception("Start time hour format is not ok. Please respect the format as following: '00:30', '10:00', '07:30', '22:45'.");
            if (!endTimeValid)
                throw new Exception("End time hour format is not ok. Please respect the format as following: '00:30', '10:00', '07:30', '22:45'.");

            if (!HelperDateTimeValidation.CheckHourInterval(updatedSalonDto.StartTimeHour, updatedSalonDto.EndTimeHour))
                throw new Exception("Start time hour can not be higher than end time hour.");

            var workDaysLower = updatedSalonDto.WorkDays.ToLower();
            var stringIsValid = HelperInputValidationRegex.CheckWorkDaysInput(workDaysLower);

            if (!stringIsValid)
            {
                throw new Exception("Work days not updated successfully. Pattern is incorrect. (\"monday,tuesday,saturday etc\")");
            }

            var updatedSalon = new UpdateSalonDto()
            {
                Id = updatedSalonDto.Id,
                SalonName = updatedSalonDto.SalonName,
                SalonAddress = updatedSalonDto.SalonAddress,
                SalonCity = updatedSalonDto.SalonCity,
                UserId = updatedSalonDto.UserId,
                WorkDays = workDaysLower,
                StartTimeHour = updatedSalonDto.StartTimeHour,
                EndTimeHour = updatedSalonDto.EndTimeHour,
            };

            return await _salonRepository.UpdateSalon(updatedSalon);
        }

        public async Task<GetSalonListDto> GetAllSalons(int page, int pageSize, string search, string selectedCities)
        {
            return await _salonRepository.GetAllSalons(page, pageSize, search, selectedCities);
        }

        public async Task<GetSingleSalonDto> GetSingleSalonDetails(int salonId)
        {
            if (salonId <= 0)
            {
                throw new Exception("Salon not found");
            }

            return await _salonRepository.GetSingleSalonDetails(salonId);
        }

        public async Task<string> SetWorkDays(SetWorkDaysDto workDaysDto)
        {
            var currentUserID = GetUserId();

            if (workDaysDto.UserId <= 0 || workDaysDto.UserId != currentUserID)
                throw new Exception("Not authorized.");

            if (workDaysDto.SalonId <= 0)
                throw new Exception("Salon doesn't exist.");

            if (string.IsNullOrEmpty(workDaysDto.WorkDays) || string.IsNullOrWhiteSpace(workDaysDto.WorkDays))
                return "You can not set an empty field for the work days of salon";

            var workDaysLower = workDaysDto.WorkDays.ToLower();
            var stringIsValid = HelperInputValidationRegex.CheckWorkDaysInput(workDaysLower);

            if (!stringIsValid)
            {
                return "Work days not updated successfully. Pattern is incorrect. (\"monday,tuesday,saturday etc\")";
            }

            workDaysDto.WorkDays = workDaysLower;
            return await _salonRepository.SetWorkDays(workDaysDto);
        }

        public async Task<string> ModifySalonStatus(ModifySalonStatusDto modifySalonStatusDto)
        {
            // TODO - validate salon status Id
            var currentUserID = GetUserId();

            if (modifySalonStatusDto.EmployeeId <= 0 || modifySalonStatusDto.EmployeeId != currentUserID)
                throw new Exception("Not authorized.");

            if (modifySalonStatusDto.SalonUserId <= 0)
                throw new Exception($"User with id {modifySalonStatusDto.SalonUserId} doesn't exist or doesn't have a salon.");

            if (modifySalonStatusDto.SalonId <= 0)
                throw new Exception("Salon doesn't exist.");

            // TODO - maybe we can use another approach to check this (enum?, db table?)
            if (modifySalonStatusDto.SalonStatusId <= 0 || modifySalonStatusDto.SalonStatusId > 4)
                throw new Exception("Salon status id is incorrect");

            var result = await _salonRepository.ModifySalonStatus(modifySalonStatusDto);

            if (!result)
                return "Salon status couldn't be modified.";

            return "Salon status was modified.";
        }

        public async Task<string> ReviewSalon(ReviewSalonDto reviewSalonDto)
        {
            var currentUserId = GetUserId();
            if (reviewSalonDto.UserId != currentUserId || reviewSalonDto.UserId <= 0)
                throw new Exception("Not authorized!");

            if (reviewSalonDto.SalonId <= 0)
                throw new Exception("Salon doesn't exist.");

            var checkIfReviewRatingIsCorrect = Enum.IsDefined(typeof(ReviewRating), reviewSalonDto.ReviewRating);

            if (!checkIfReviewRatingIsCorrect)
                throw new Exception("Not a valid rating to send a review. (1-10)");

            return await _salonRepository.ReviewSalon(reviewSalonDto);
        }

        public async Task<string> ReportUser(ReportUserDto reportUserDto)
        {
            if (reportUserDto.UserId <= 0)
                throw new Exception("User doesn't exist.");

            if (reportUserDto.SalonId <= 0)
                throw new Exception("Salon not found.");

            if (string.IsNullOrEmpty(reportUserDto.ReportMessage))
                throw new Exception("Report reason can not be empty.");

            return await _salonRepository.ReportUser(reportUserDto);
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

            var isHourValid = 
                createSalonServiceDto.HaircutDurationTime == "1800" || 
                createSalonServiceDto.HaircutDurationTime == "2700" ||
                createSalonServiceDto.HaircutDurationTime == "3600";
            if (!isHourValid)
                throw new Exception("Duration time format is not ok. Please select one of the following option: '1800' | '2700' | '3600'.");

            return await _salonRepository.CreateNewSalonService(createSalonServiceDto);
        }

        public async Task<GetSalonServiceDto> GetSingleSalonService(string salonServiceName, int salonId)
        {
            if (salonId <= 0)
                throw new Exception("Salon doesn't exists.");

            if (string.IsNullOrEmpty(salonServiceName) || string.IsNullOrWhiteSpace(salonServiceName))
                throw new Exception("Salon service name must have a value.");

            return await _salonRepository.GetSingleSalonService(salonServiceName, salonId);
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

            if (updateSalonServiceDto.Price <= 0)
                throw new Exception("Price must be greater than 0.");

            var isHourValid =
                updateSalonServiceDto.HaircutDurationTime == "1800" ||
                updateSalonServiceDto.HaircutDurationTime == "2700" ||
                updateSalonServiceDto.HaircutDurationTime == "3600";
            if (!isHourValid)
                throw new Exception("Duration time format is not ok. Please select one of the following option: '1800' | '2700' | '3600'.");

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
