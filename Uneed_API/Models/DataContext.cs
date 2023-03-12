using Microsoft.EntityFrameworkCore;

namespace Uneed_API.Models
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }
        static DataContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Calification> Calification { get; set; }
        public DbSet<ContratService> ContratService { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<AddressUser> AddressUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressUser>()
                .HasKey(au => new { au.AddressId, au.UserId });

            modelBuilder.Entity<AddressUser>()
                .HasOne(au => au.Address)
                .WithMany(a => a.AddressUser)
                .HasForeignKey(au => au.AddressId);

            modelBuilder.Entity<AddressUser>()
                .HasOne(au => au.User)
                .WithMany(u => u.AddressUser)
                .HasForeignKey(au => au.UserId);
        }

    }
}
