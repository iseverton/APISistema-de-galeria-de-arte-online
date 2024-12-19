using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDeGaleriaDeArteAPI.Models;

namespace SistemaDeGaleriaDeArteAPI.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {

            builder.ToTable("users");
            builder.HasKey(u => u.UserID);

            builder.Property(u => u.UserID)
                .HasColumnName("user_id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd()
                .UseMySqlIdentityColumn();

            builder.Property(u => u.UserName)
                .HasColumnName("user_name")
                .HasColumnType("varchar")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(u => u.UserEmail)
                .HasColumnName("user_email")
                .HasColumnType("varchar")
                .HasMaxLength(180)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnName("password")
                .HasColumnType("varchar")
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(u => u.Bio)
                .HasColumnName("bio")
                .HasColumnType("text")
                .HasMaxLength(120)
                .IsRequired(false);

            builder.Property(u=> u.PhoneNumber)
                   .HasColumnName("phoneNumber")
                   .HasColumnType("varchar")
                   .HasMaxLength(11)
                   .IsRequired(false);

            builder.Property(u => u.CreatedAt)
                   .HasColumnName("createdAt")
                   .HasColumnType("datetime")
                   .IsRequired();

           
            
        }
    }
}
