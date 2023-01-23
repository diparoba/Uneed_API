﻿using Microsoft.EntityFrameworkCore;

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
        public DbSet<ServiceProvider> ServiceProvider { get; set; }
        public DbSet<ServiceCategory> ServiceCategory { get; set; }
    }
}
