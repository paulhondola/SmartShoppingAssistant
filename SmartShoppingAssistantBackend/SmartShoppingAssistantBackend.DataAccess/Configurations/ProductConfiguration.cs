using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartShoppingAssistantBackend.DataAccess.Entities;

namespace SmartShoppingAssistantBackend.DataAccess.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    private const int NAME_MAX_LENGTH = 200;
    private const int DESCRIPTION_MAX_LENGTH = 1000;
    private const int IMAGE_URL_MAX_LENGTH = 500;

    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(NAME_MAX_LENGTH);

        builder.Property(p => p.Description).HasMaxLength(DESCRIPTION_MAX_LENGTH);

        builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(10,2)");

        builder.Property(p => p.ImageUrl).HasMaxLength(IMAGE_URL_MAX_LENGTH);
    }
}
