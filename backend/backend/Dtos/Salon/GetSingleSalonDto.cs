using backend.Dtos.Auth;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Salon
{
    public class GetSingleSalonDto
    {
        public string SalonName { get; set; }
        public string SalonCity { get; set; }
        public string SalonAddress { get; set; }
        public UsersSalonsDetailsDto UserDetails { get; set; }
        public string WorkDays { get; set; }
        public string WorkHoursInterval { get; set; }
        public decimal SalonReviews { get; set; }
    }
}