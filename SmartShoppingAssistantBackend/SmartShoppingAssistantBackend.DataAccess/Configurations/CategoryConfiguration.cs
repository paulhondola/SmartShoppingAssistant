using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartShoppingAssistantBackend.DataAccess.Entities;

namespace SmartShoppingAssistantBackend.DataAccess.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    private const int NAME_MAX_LENGTH = 100;
    private const int DESCRIPTION_MAX_LENGTH = 500;

    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(NAME_MAX_LENGTH);

        builder.Property(c => c.Description).HasMaxLength(DESCRIPTION_MAX_LENGTH);
    }
}
