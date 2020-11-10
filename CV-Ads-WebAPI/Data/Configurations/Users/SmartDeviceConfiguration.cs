using CV_Ads_WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CV_Ads_WebAPI.Data.Configurations
{
    public class SmartDeviceConfiguration : IEntityTypeConfiguration<SmartDevice>
    {
        public void Configure(EntityTypeBuilder<SmartDevice> builder)
        {
            builder.HasKey(smartDevice => smartDevice.Id);

            builder.HasOne(smartDevice => smartDevice.UserIdentity)
                .WithOne()
                .HasForeignKey<SmartDevice>(smartDevice => smartDevice.Id)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(smartDevice => smartDevice.Partner)
                .WithMany(partner => partner.SmartDevices)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(smartDevice => smartDevice.AdvertisementViews)
                .WithOne(advertisementView => advertisementView.SmartDevice)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
