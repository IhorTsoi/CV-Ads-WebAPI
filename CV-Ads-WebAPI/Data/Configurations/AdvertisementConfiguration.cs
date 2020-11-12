using CV_Ads_WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CV_Ads_WebAPI.Data.Configurations
{
    public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.HasKey(advertisement => advertisement.Id);

            builder.Property(advertisement => advertisement.Name).IsRequired();
            builder.Property(advertisement => advertisement.PictureExtension).IsRequired();

            builder.HasOne(advertisement => advertisement.Customer)
                .WithMany(customer => customer.Advertisements)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(advertisement => advertisement.AdvertisementViews)
                .WithOne(advertisementView => advertisementView.Advertisement)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(advertisement => advertisement.TimePeriodLimits)
                .WithOne(timePeriodLimit => timePeriodLimit.Advertisement)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(advertisement => advertisement.HumanLimits)
                .WithOne(timePeriodLimit => timePeriodLimit.Advertisement)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
