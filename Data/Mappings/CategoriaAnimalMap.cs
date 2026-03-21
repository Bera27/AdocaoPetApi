using AdocaoPetApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdocaoPetApi.Data.Mappings
{
    public class CategoriaAnimalMap : IEntityTypeConfiguration<CategoriaAnimal>
    {
        public void Configure(EntityTypeBuilder<CategoriaAnimal> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.NomeCategoria)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(x => x.AnimaisCategorias)
                .WithOne(x => x.CategoriaAnimal)
                .HasForeignKey(x => x.IdCategoriaAnimal)
                .HasConstraintName("FK_Animal_CategoriaAnimal")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}