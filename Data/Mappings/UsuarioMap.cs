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
        }
    }
}