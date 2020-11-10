using CV_Ads_WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CV_Ads_WebAPI.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(customer => customer.Id);

            builder.Property(customer => customer.FirstName).IsRequired();
            builder.Property(customer => customer.LastName).IsRequired();

            builder.HasOne(customer => customer.UserIdentity)
                .WithOne()
                .HasForeignKey<Customer>(customer => customer.Id)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(customer => customer.Advertisements)
                .WithOne(advertisement => advertisement.Customer)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
