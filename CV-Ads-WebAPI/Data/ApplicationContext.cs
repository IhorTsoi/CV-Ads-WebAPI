using CV_Ads_WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }

    }
}
