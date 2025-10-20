using Biblioteca.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Data
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options) { }

        public DbSet<LibroEntity> Libros { get; set; }
        public DbSet<AutorEntity> Autores { get; set; }
        public DbSet<GeneroEntity> Generos { get; set; }
        public DbSet<EditorialEntity> Editoriales { get; set; }
        public DbSet<PersonaEntity> Personas { get; set; }
        public DbSet<UsuarioEntity> Usuarios { get; set; }
        public DbSet<PrestamoEntity> Prestamos { get; set; }
        public DbSet<PermisoEntity> Permisos { get; set; }
        public DbSet<GrupoPermisoEntity> Grupos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Autor
            modelBuilder.Entity<AutorEntity>(entity =>
            {
                entity.ToTable("Autores");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Id).ValueGeneratedOnAdd();
                entity.Property(a => a.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Apellido).IsRequired().HasMaxLength(100);
            });

            // Genero
            modelBuilder.Entity<GeneroEntity>(entity =>
            {
                entity.ToTable("Generos");
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Id).ValueGeneratedOnAdd();
                entity.Property(g => g.Nombre).IsRequired().HasMaxLength(100);
                entity.HasIndex(g => g.Nombre).IsUnique();
            });

            // Editorial
            modelBuilder.Entity<EditorialEntity>(entity =>
            {
                entity.ToTable("Editoriales");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Nombre).IsUnique();
            });

            // Persona
            modelBuilder.Entity<PersonaEntity>(entity =>
            {
                entity.ToTable("Personas");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Apellido).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Dni).IsRequired().HasMaxLength(20);
                entity.HasIndex(p => p.Dni).IsUnique();
                entity.Property(p => p.Email).IsRequired().HasMaxLength(200);
            });

            // Usuario
            modelBuilder.Entity<UsuarioEntity>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).ValueGeneratedOnAdd();
                entity.Property(u => u.NombreUsuario).IsRequired().HasMaxLength(50);
                entity.HasIndex(u => u.NombreUsuario).IsUnique();
                entity.Property(u => u.Rol).IsRequired().HasMaxLength(20);
                entity.Property(u => u.PersonaId).IsRequired();
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.Salt).IsRequired();
                entity.Property(u => u.FechaCreacion).IsRequired();
                entity.Property(u => u.Activo).IsRequired();
                entity.HasOne(u => u.Persona).WithMany().HasForeignKey(u => u.PersonaId).OnDelete(DeleteBehavior.Restrict);
            });

            // Permiso
            modelBuilder.Entity<PermisoEntity>(entity =>
            {
                entity.ToTable("Permisos");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Categoria).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Descripcion).HasMaxLength(500);
                entity.Property(p => p.Activo).IsRequired();
                entity.HasIndex(p => new { p.Categoria, p.Nombre }).IsUnique();
            });

            // Grupo
            modelBuilder.Entity<GrupoPermisoEntity>(entity =>
            {
                entity.ToTable("GruposPermiso");
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Id).ValueGeneratedOnAdd();
                entity.Property(g => g.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(g => g.Descripcion).HasMaxLength(500);
                entity.Property(g => g.Activo).IsRequired();
                entity.Property(g => g.FechaCreacion).IsRequired();
            });

            // Many-to-many Usuario-Grupo con nombres de columnas explícitos para seed
            modelBuilder.Entity<UsuarioEntity>()
                .HasMany(u => u.Grupos)
                .WithMany(g => g.Usuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuarioGrupos",
                    j => j.HasOne<GrupoPermisoEntity>().WithMany().HasForeignKey("GruposId").OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<UsuarioEntity>().WithMany().HasForeignKey("UsuariosId").OnDelete(DeleteBehavior.Cascade)
                );

            // Many-to-many Grupo-Permiso
            modelBuilder.Entity<GrupoPermisoEntity>()
                .HasMany(g => g.Permisos)
                .WithMany(p => p.Grupos)
                .UsingEntity<Dictionary<string, object>>(
                    "GrupoPermisos",
                    j => j.HasOne<PermisoEntity>().WithMany().HasForeignKey("PermisosId").OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<GrupoPermisoEntity>().WithMany().HasForeignKey("GruposId").OnDelete(DeleteBehavior.Cascade)
                );

            // Seed
            SeedData(modelBuilder);
        }

        private static (byte[] hash, byte[] salt) HashPasswordDeterministic(string password, string saltSeed)
        {
            var salt = Encoding.UTF8.GetBytes(saltSeed);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(32);
            return (hash, salt);
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

            // Permisos básicos por entidad/operación
            var permisoId = 1;
            var categorias = new[] { "autores", "libros", "generos", "editoriales", "personas", "usuarios", "prestamos" };
            var acciones = new[] { "leer", "agregar", "actualizar", "eliminar" };

            var permisos = new List<PermisoEntity>();
            foreach (var cat in categorias)
            {
                foreach (var acc in acciones)
                {
                    permisos.Add(new PermisoEntity { Id = permisoId++, Categoria = cat, Nombre = acc, Descripcion = $"{acc} {cat}", Activo = true });
                }
            }
            modelBuilder.Entity<PermisoEntity>().HasData(permisos);

            // Grupos base
            modelBuilder.Entity<GrupoPermisoEntity>().HasData(
                new GrupoPermisoEntity { Id = 1, Nombre = "bibliotecario", Descripcion = "Acceso completo", Activo = true, FechaCreacion = DateTime.UtcNow },
                new GrupoPermisoEntity { Id = 2, Nombre = "socio", Descripcion = "Acceso limitado", Activo = true, FechaCreacion = DateTime.UtcNow }
            );

            // Asignaciones Grupo-Permiso
            var grupoPermisos = new List<Dictionary<string, object>>();
            // bibliotecario: todos los permisos (Ids 1..(categorias*acciones))
            for (int pid = 1; pid < permisoId; pid++)
            {
                grupoPermisos.Add(new Dictionary<string, object> { ["GruposId"] = 1, ["PermisosId"] = pid });
            }
            // socio: permisos de lectura básicos (libros, préstamos, autores, géneros y editoriales)
            var lecturaCategorias = new[] { "libros", "prestamos", "autores", "generos", "editoriales" };
            foreach (var p in permisos.Where(p => p.Nombre == "leer" && lecturaCategorias.Contains(p.Categoria)))
            {
                grupoPermisos.Add(new Dictionary<string, object> { ["GruposId"] = 2, ["PermisosId"] = p.Id });
            }
            modelBuilder.Entity("GrupoPermisos").HasData(grupoPermisos.ToArray());

            // Usuarios iniciales con contraseñas hasheadas
            var (adminHash, adminSalt) = HashPasswordDeterministic("admin123", "seed-salt-admin-2025");
            var (socioHash, socioSalt) = HashPasswordDeterministic("socio123", "seed-salt-socio-2025");

            modelBuilder.Entity<UsuarioEntity>().HasData(
                new UsuarioEntity { Id = 1, NombreUsuario = "admin", Rol = "administrador", PersonaId = 1, PasswordHash = adminHash, Salt = adminSalt, Activo = true, FechaCreacion = DateTime.UtcNow },
                new UsuarioEntity { Id = 2, NombreUsuario = "juanp", Rol = "socio", PersonaId = 2, PasswordHash = socioHash, Salt = socioSalt, Activo = true, FechaCreacion = DateTime.UtcNow },
                new UsuarioEntity { Id = 3, NombreUsuario = "mariag", Rol = "socio", PersonaId = 3, PasswordHash = socioHash, Salt = socioSalt, Activo = true, FechaCreacion = DateTime.UtcNow }
            );

            // Asignaciones Usuario-Grupo
            modelBuilder.Entity("UsuarioGrupos").HasData(
                new Dictionary<string, object> { ["UsuariosId"] = 1, ["GruposId"] = 1 },
                new Dictionary<string, object> { ["UsuariosId"] = 2, ["GruposId"] = 2 },
                new Dictionary<string, object> { ["UsuariosId"] = 3, ["GruposId"] = 2 }
            );
        }
    }
}