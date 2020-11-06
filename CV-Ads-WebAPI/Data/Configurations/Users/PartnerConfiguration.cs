using CV_Ads_WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CV_Ads_WebAPI.Data.Configurations
{
    public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder.HasKey(partner => partner.Id);

            builder.Property(partner => partner.FirstName).IsRequired();
            builder.Property(partner => partner.LastName).IsRequired();

            builder.HasOne(partner => partner.UserIdentity)
                .WithOne()
                .HasForeignKey<Partner>(partner => partner.Id)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(partner => partner.SmartDevices)
                .WithOne(smartDevice => smartDevice.Partner)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
