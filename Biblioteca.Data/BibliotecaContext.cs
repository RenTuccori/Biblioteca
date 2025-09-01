using Biblioteca.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options)
        {
        }

        public DbSet<LibroEntity> Libros { get; set; }
        public DbSet<AutorEntity> Autores { get; set; }
        public DbSet<GeneroEntity> Generos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la entidad Autor
            modelBuilder.Entity<AutorEntity>(entity =>
            {
                entity.ToTable("Autores");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Id).ValueGeneratedOnAdd();
                
                entity.Property(a => a.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(a => a.Apellido)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            // Configuración de la entidad Genero
            modelBuilder.Entity<GeneroEntity>(entity =>
            {
                entity.ToTable("Generos");
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Id).ValueGeneratedOnAdd();
                
                entity.Property(g => g.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(g => g.Nombre).IsUnique();
            });

            // Configuración de la entidad Libro
            modelBuilder.Entity<LibroEntity>(entity =>
            {
                entity.ToTable("Libros");
                entity.HasKey(l => l.Id);
                entity.Property(l => l.Id).ValueGeneratedOnAdd();
                
                entity.Property(l => l.Titulo)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(l => l.ISBN)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasIndex(l => l.ISBN).IsUnique();

                // Configuración de claves foráneas
                entity.Property(l => l.AutorId)
                    .IsRequired();

                entity.Property(l => l.GeneroId)
                    .IsRequired();

                // Configuración de relaciones
                entity.HasOne(l => l.Autor)
                    .WithMany()
                    .HasForeignKey(l => l.AutorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(l => l.Genero)
                    .WithMany()
                    .HasForeignKey(l => l.GeneroId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Datos iniciales
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Géneros iniciales
            modelBuilder.Entity<GeneroEntity>().HasData(
                new GeneroEntity { Id = 1, Nombre = "Ficción" },
                new GeneroEntity { Id = 2, Nombre = "Ciencia Ficción" },
                new GeneroEntity { Id = 3, Nombre = "Romance" },
                new GeneroEntity { Id = 4, Nombre = "Misterio" },
                new GeneroEntity { Id = 5, Nombre = "Biografía" }
            );

            // Autores iniciales
            modelBuilder.Entity<AutorEntity>().HasData(
                new AutorEntity { Id = 1, Nombre = "Gabriel", Apellido = "García Márquez" },
                new AutorEntity { Id = 2, Nombre = "Isaac", Apellido = "Asimov" },
                new AutorEntity { Id = 3, Nombre = "Jane", Apellido = "Austen" },
                new AutorEntity { Id = 4, Nombre = "Agatha", Apellido = "Christie" },
                new AutorEntity { Id = 5, Nombre = "Jorge Luis", Apellido = "Borges" }
            );
        }
    }
}