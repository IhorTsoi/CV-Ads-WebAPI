using CV_Ads_WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CV_Ads_WebAPI.Data.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(admin => admin.Id);

            builder.Property(admin => admin.FirstName).IsRequired();
            builder.Property(admin => admin.LastName).IsRequired();

            builder.HasOne(admin => admin.UserIdentity)
                .WithOne()
                .HasForeignKey<Admin>(admin => admin.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
