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
        public DbSet<EditorialEntity> Editoriales { get; set; }
        public DbSet<PersonaEntity> Personas { get; set; }
        public DbSet<UsuarioEntity> Usuarios { get; set; }
        public DbSet<PrestamoEntity> Prestamos { get; set; }

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

            // Configuración de la entidad Editorial
            modelBuilder.Entity<EditorialEntity>(entity =>
            {
                entity.ToTable("Editoriales");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(e => e.Nombre).IsUnique();
            });

            // Configuración de la entidad Persona
            modelBuilder.Entity<PersonaEntity>(entity =>
            {
                entity.ToTable("Personas");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                
                entity.Property(p => p.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(p => p.Apellido)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(p => p.Dni)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasIndex(p => p.Dni).IsUnique();
                
                entity.Property(p => p.Email)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            // Configuración de la entidad Usuario
            modelBuilder.Entity<UsuarioEntity>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).ValueGeneratedOnAdd();
                
                entity.Property(u => u.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(u => u.NombreUsuario).IsUnique();
                
                entity.Property(u => u.Clave)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(u => u.Rol)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(u => u.PersonaId)
                    .IsRequired();

                // Relación con Persona
                entity.HasOne(u => u.Persona)
                    .WithMany()
                    .HasForeignKey(u => u.PersonaId)
                    .OnDelete(DeleteBehavior.Restrict);
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

                entity.Property(l => l.Estado)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValue("disponible");

                // Configuración de claves foráneas
                entity.Property(l => l.AutorId)
                    .IsRequired();

                entity.Property(l => l.GeneroId)
                    .IsRequired();

                entity.Property(l => l.EditorialId)
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

                entity.HasOne(l => l.Editorial)
                    .WithMany()
                    .HasForeignKey(l => l.EditorialId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de la entidad Prestamo
            modelBuilder.Entity<PrestamoEntity>(entity =>
            {
                entity.ToTable("Prestamos");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                
                entity.Property(p => p.LibroId)
                    .IsRequired();

                entity.Property(p => p.SocioId)
                    .IsRequired();

                entity.Property(p => p.FechaPrestamo)
                    .IsRequired();

                entity.Property(p => p.FechaDevolucionPrevista)
                    .IsRequired();

                entity.Property(p => p.FechaDevolucionReal)
                    .IsRequired(false);

                // Configuración de relaciones
                entity.HasOne(p => p.Libro)
                    .WithMany()
                    .HasForeignKey(p => p.LibroId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Socio)
                    .WithMany()
                    .HasForeignKey(p => p.SocioId)
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

            // Editoriales iniciales
            modelBuilder.Entity<EditorialEntity>().HasData(
                new EditorialEntity { Id = 1, Nombre = "Sudamericana" },
                new EditorialEntity { Id = 2, Nombre = "Penguin Random House" },
                new EditorialEntity { Id = 3, Nombre = "Planeta" },
                new EditorialEntity { Id = 4, Nombre = "Alfaguara" },
                new EditorialEntity { Id = 5, Nombre = "Anagrama" }
            );

            // Personas iniciales
            modelBuilder.Entity<PersonaEntity>().HasData(
                new PersonaEntity { Id = 1, Nombre = "Admin", Apellido = "Sistema", Dni = "00000000", Email = "admin@biblioteca.com" },
                new PersonaEntity { Id = 2, Nombre = "Juan", Apellido = "Pérez", Dni = "12345678", Email = "juan.perez@email.com" },
                new PersonaEntity { Id = 3, Nombre = "María", Apellido = "González", Dni = "87654321", Email = "maria.gonzalez@email.com" }
            );

            // Usuarios iniciales
            modelBuilder.Entity<UsuarioEntity>().HasData(
                new UsuarioEntity { Id = 1, NombreUsuario = "admin", Clave = "admin123", Rol = "bibliotecario", PersonaId = 1 },
                new UsuarioEntity { Id = 2, NombreUsuario = "juanp", Clave = "socio123", Rol = "socio", PersonaId = 2 },
                new UsuarioEntity { Id = 3, NombreUsuario = "mariag", Clave = "socio123", Rol = "socio", PersonaId = 3 }
            );
        }
    }
}