using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DataAccess.Entities;

namespace Backend.DataAccess.Configurations;

public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
{
    private const int NAME_MAX_LENGTH = 200;

    public void Configure(EntityTypeBuilder<Promotion> builder)
    {
        builder.ToTable("Promotions");
        
        builder.HasKey(pr => pr.Id);

        builder.Property(pr => pr.Name).IsRequired().HasMaxLength(NAME_MAX_LENGTH);

        builder.Property(pr => pr.Type).IsRequired().HasConversion<int>();

        builder.Property(pr => pr.Threshold).IsRequired().HasColumnType("decimal(10,2)");

        builder.Property(pr => pr.Reward).IsRequired().HasConversion<int>();

        builder.Property(pr => pr.RewardValue).IsRequired();

        builder.Property(pr => pr.IsActive);

        builder
            .HasOne(pr => pr.Product)
            .WithMany(p => p.Promotions)
            .HasForeignKey(pr => pr.ProductId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(pr => pr.Category)
            .WithMany(c => c.Promotions)
            .HasForeignKey(pr => pr.CategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
