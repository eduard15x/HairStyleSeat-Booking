using backend.Dtos.Auth;

namespace backend.Dtos.Salon
{
    public class GetSingleSalonDto
    {
        public string SalonName { get; set; }
        public string SalonCity { get; set; }
        public string SalonAddress { get; set; }
        public UsersSalonsDetailsDto UserDetails { get; set; }
        public string WorkDays { get; set; }
        public string StartTimeHour { get; set; } = string.Empty;
        public string EndTimeHour { get; set; } = string.Empty;
        public decimal SalonReviews { get; set; }
        public int SalonStatus { get; set; }
    }
}