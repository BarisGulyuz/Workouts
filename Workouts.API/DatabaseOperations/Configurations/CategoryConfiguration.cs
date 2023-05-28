using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workouts.API.Models;

namespace Workouts.API.DatabaseOperations.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //SELECT * FROM WRK.Categories
            builder.ToTable("Categories", "WRK");

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);

            builder.HasMany(x => x.Products)
                   .WithOne(p => p.Category)
                   .HasForeignKey(x => x.CategoryId);

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

        }
    }
}
