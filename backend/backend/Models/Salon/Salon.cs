using backend.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Salon
{
    public class Salon
    {
        public int Id { get; set; }
        public string SalonName { get; set; }
        public string SalonCity { get; set; }
        public string SalonAddress { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
