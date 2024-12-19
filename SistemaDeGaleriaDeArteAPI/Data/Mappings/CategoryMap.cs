using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDeGaleriaDeArteAPI.Models;

namespace SistemaDeGaleriaDeArteAPI.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<CategoryModel>
    {
        public void Configure(EntityTypeBuilder<CategoryModel> builder)
        {
            builder.ToTable("category");
            builder.HasKey(c=> c.CategoryID);

            builder.Property(c=> c.CategoryID)
                .HasColumnName("category_id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd()
                .UseMySqlIdentityColumn();

            builder.Property(c=> c.CategoryName)
                .HasColumnName("category_name")
                .HasColumnType("varchar")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(c=> c.Description)
                .HasColumnName("description")
                .HasColumnType("text")
                .IsRequired();
        }
    }
}
