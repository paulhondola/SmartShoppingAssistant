using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DataAccess.Entities;

namespace Backend.DataAccess.Configurations;

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
