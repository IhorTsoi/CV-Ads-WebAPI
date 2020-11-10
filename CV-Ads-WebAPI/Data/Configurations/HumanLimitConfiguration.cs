using CV_Ads_WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CV_Ads_WebAPI.Data.Configurations
{
    public class HumanLimitConfiguration : IEntityTypeConfiguration<HumanLimit>
    {
        public void Configure(EntityTypeBuilder<HumanLimit> builder)
        {
            builder.HasKey(humanLimit => humanLimit.Id);

            builder.HasOne(humanLimit => humanLimit.Advertisement)
                .WithMany(advertisement => advertisement.HumanLimits)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
