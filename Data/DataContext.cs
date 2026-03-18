using AdocaoPetApi.Data.Mappings;
using AdocaoPetApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AdocaoPetApi.Data
{
    public class DataContext : DbContext
    {

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Animal> Animais { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new AnimalMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
        }
    }
}