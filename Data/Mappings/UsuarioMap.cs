using AdocaoPetApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdocaoPetApi.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                    .ValueGeneratedOnAdd();
            
            builder.Property(u => u.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(u => u.Telefone)
                    .IsRequired()
                    .HasMaxLength(13);

            builder.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(255);

            builder.Property(u => u.Senha)
                    .IsRequired()
                    .HasMaxLength(255);
           
           builder.HasIndex(x => x.Email, "IX_Usuario_Email")
                  .IsUnique();

                // Relacionameto de Role com usuario

            builder.HasMany(u => u.Roles)
                   .WithMany(x => x.Usuarios)
                   .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        role => role
                                .HasOne<Role>()
                                .WithMany()
                                .HasForeignKey("RoleId")
                                .HasConstraintName("FK_UsuarioRole_RoleId")
                                .OnDelete(DeleteBehavior.Cascade),
                        user => user
                                .HasOne<Usuario>()
                                .WithMany()
                                .HasForeignKey("UserId")
                                .HasConstraintName("FK_UserRole_UserId")
                                .OnDelete(DeleteBehavior.Cascade));
        }
    }
}