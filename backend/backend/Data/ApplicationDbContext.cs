using backend.Models.Auth;
using backend.Models.Salon;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        public DbSet<User> Users { get; set; }
        public DbSet<Salon> Salons { get; set; }
        public DbSet<SalonService> SalonServices { get; set; }
        public DbSet<SalonStatus> SalonStatuses { get; set; }
    }
}
