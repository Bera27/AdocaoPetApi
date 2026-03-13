using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdocaoPetApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdocaoPetApi.Data.Mappings
{
    public class AnimalMap : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                    .ValueGeneratedOnAdd();

            builder.Property(a => a.Nome)
                    .IsRequired()
                    .HasMaxLength(100);
            
            builder.Property(a => a.Especie)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(a => a.Idade)
                    .IsRequired();

            builder.Property(a => a.Raca)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(a => a.Porte)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(a => a.Sexo)
                    .IsRequired()
                    .HasMaxLength(10);

            builder.Property(a => a.Descricao)
                    .HasMaxLength(500);

            builder.Property(a => a.Saude)
                    .HasMaxLength(100);
            
            builder.Property(a => a.Historia)
                    .HasMaxLength(500);

            builder.Property(a => a.Status)
                    .IsRequired()
                    .HasMaxLength(20);

            builder.Property(a => a.FotoUrl)
                    .HasMaxLength(200);

            builder.HasOne(a => a.Usuario)
                    .WithMany(u => u.Animais)
                    .HasForeignKey(a => a.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}