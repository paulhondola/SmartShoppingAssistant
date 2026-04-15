using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartShoppingAssistantBackend.DataAccess.Entities;

namespace SmartShoppingAssistantBackend.DataAccess.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        
        
    }
}