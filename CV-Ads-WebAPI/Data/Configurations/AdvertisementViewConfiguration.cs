using CV_Ads_WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CV_Ads_WebAPI.Data.Configurations
{
    public class AdvertisementViewConfiguration : IEntityTypeConfiguration<AdvertisementView>
    {
        public void Configure(EntityTypeBuilder<AdvertisementView> builder)
        {
            builder.HasKey(advertisementView => advertisementView.Id);

            builder.Property(advertisementView => advertisementView.Country).IsRequired();
            builder.Property(advertisementView => advertisementView.City).IsRequired();

            builder.HasOne(advertisementView => advertisementView.Advertisement)
                .WithMany(advertisement => advertisement.AdvertisementViews)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(advertisementView => advertisementView.SmartDevice)
                .WithMany(smartDevice => smartDevice.AdvertisementViews)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
