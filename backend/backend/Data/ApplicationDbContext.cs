using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        public DbSet<User> Users { get; set; }
        public DbSet<Salon> Salons { get; set; }
        public DbSet<Service> SalonServices { get; set; }
        public DbSet<Status> SalonStatuses { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
