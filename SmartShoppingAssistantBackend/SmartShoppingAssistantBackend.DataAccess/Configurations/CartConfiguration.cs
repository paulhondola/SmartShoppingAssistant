using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartShoppingAssistantBackend.DataAccess.Entities;

namespace SmartShoppingAssistantBackend.DataAccess.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("CartItems");
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Quantity).IsRequired();

        builder
            .HasOne(c => c.Product)
            .WithOne(p => p.Cart)
            .HasForeignKey<Cart>(c => c.ProductId);
    }
}
