using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDeGaleriaDeArteAPI.Models;

namespace SistemaDeGaleriaDeArteAPI.Data.Mappings
{
    public class WorkMap : IEntityTypeConfiguration<WorkModel>
    {
        public void Configure(EntityTypeBuilder<WorkModel> builder)
        {
            builder.ToTable("works");
            builder.HasKey(w=>w.WorkId);

            builder.Property(w=> w.WorkId)
                .HasColumnName("work_id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd()
                .UseMySqlIdentityColumn();

            builder.Property(w=>w.NameWork)
                .HasColumnName("work_name")
                .HasColumnType("varchar")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(w => w.Description)
                .HasColumnName("description")
                .HasColumnType("text")
                .IsRequired();


            builder.Property(w => w.ImageUrl)
                .HasColumnName("imageurl")
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(w=> w.CreatedAt)
                  .HasColumnName("createdAt")
                  .HasColumnType("datetime")
                  .IsRequired();

            builder.Property(u => u.LastUpdateDate)
                  .HasColumnName("lastUpdateDate")
                  .HasColumnType("datetime")
                  .IsRequired();

            builder.Property(w => w.IsAvailable)
                  .HasColumnName("isAvailable")
                  .HasColumnType("tinyint(1)")
                  .IsRequired();

            // relacionamento category
            builder.HasOne(c => c.Category)
                .WithMany(w=> w.Works)
                .OnDelete(DeleteBehavior.Cascade);

            // relacionamento artist
            builder.HasOne(a => a.Artist)
                .WithMany(w => w.Works)
                .OnDelete(DeleteBehavior.Cascade);

            

        }
    }
}
