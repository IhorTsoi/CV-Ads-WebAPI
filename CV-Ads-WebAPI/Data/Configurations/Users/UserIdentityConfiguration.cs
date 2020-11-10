using CV_Ads_WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CV_Ads_WebAPI.Data.Configurations
{
    public class UserIdentityConfiguration : IEntityTypeConfiguration<UserIdentity>
    {
        public void Configure(EntityTypeBuilder<UserIdentity> builder)
        {
            builder.HasKey(userIdentity => userIdentity.Id);

            builder.Property(userIdentity => userIdentity.Login).IsRequired();
            builder.Property(userIdentity => userIdentity.Password).IsRequired();
            builder.Property(userIdentity => userIdentity.Role).IsRequired();

            builder.HasIndex(userIdentity => userIdentity.Login).IsUnique();
        }
    }
}
