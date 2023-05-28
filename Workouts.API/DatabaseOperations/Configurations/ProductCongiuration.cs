using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workouts.API.Models;

namespace Workouts.API.DatabaseOperations.Configurations
{
    public class ProductCongiuration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //SELECT * FROM WRK.Products
            builder.ToTable("Products", "WRK");

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
            builder.HasIndex(x => x.CategoryId);

            builder.Property(x => x.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.ProductionYear)
                   .HasDefaultValue(DateTime.Now.Year);

            builder.Property(x => x.Price).HasColumnType("decimal(18,2)").HasPrecision(18,4);

        }
    }
}
