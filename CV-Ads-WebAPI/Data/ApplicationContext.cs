using CV_Ads_WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CV_Ads_WebAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        public DbSet<UserIdentity> UserIdentities { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SmartDevice> SmartDevices { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
            
            AddDefaultAppAdmin(builder);
        }

        private void AddDefaultAppAdmin(ModelBuilder builder)
        {
            Guid commonGuid = Guid.Parse("1EC7309F-C97D-412C-B8B8-31C1459CBD41");
            Admin defaultAppAdmin = new Admin(
                "qweqwe", "YVFjQHffGy3JitvNiD7sfE+NwgUesCXVH3zzpJ1HqVNUi2soi7DFh5T8PRu1dtXJ", "Ihor", "Tsoi");
            
            UserIdentity userIdentity = defaultAppAdmin.UserIdentity;
            userIdentity.Id = commonGuid;

            defaultAppAdmin.UserIdentity = null;
            defaultAppAdmin.Id = commonGuid;

            builder.Entity<UserIdentity>().HasData(userIdentity);
            builder.Entity<Admin>().HasData(defaultAppAdmin);
        }
    }
}
