using CV_Ads_WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Data.Configurations
{
    public class TimePeriodLimitConfiguration : IEntityTypeConfiguration<TimePeriodLimit>
    {
        public void Configure(EntityTypeBuilder<TimePeriodLimit> builder)
        {
            builder.HasKey(timePeriodLimit => timePeriodLimit.Id);

            builder.HasOne(timePeriodLimit => timePeriodLimit.Advertisement)
                .WithMany(advertisement => advertisement.TimePeriodLimits)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
