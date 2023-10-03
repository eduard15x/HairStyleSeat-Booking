using backend.Dtos.Auth;

namespace backend.Dtos.Salon
{
    public class GetSingleSalonDto
    {
        public string SalonName { get; set; }
        public string SalonCity { get; set; }
        public string SalonAddress { get; set; }
        public UsersSalonsDetailsDto UserDetails { get; set; }
    }
}