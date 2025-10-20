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

            // Libro
            modelBuilder.Entity<LibroEntity>(entity =>
            {
                entity.ToTable("Libros");
                entity.HasKey(l => l.Id);
                entity.Property(l => l.Id).ValueGeneratedOnAdd();
                entity.Property(l => l.Titulo).IsRequired().HasMaxLength(200);
                entity.Property(l => l.ISBN).IsRequired().HasMaxLength(20);
                entity.Property(l => l.Estado).IsRequired().HasMaxLength(20);
                entity.HasOne(l => l.Autor).WithMany().HasForeignKey(l => l.AutorId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(l => l.Genero).WithMany().HasForeignKey(l => l.GeneroId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(l => l.Editorial).WithMany().HasForeignKey(l => l.EditorialId).OnDelete(DeleteBehavior.Restrict);
            });

            // Prestamo
            modelBuilder.Entity<PrestamoEntity>(entity =>
            {
                entity.ToTable("Prestamos");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.FechaPrestamo).IsRequired();
                entity.Property(p => p.FechaDevolucionPrevista).IsRequired();
                entity.Property(p => p.FechaDevolucionReal);
                entity.HasOne(p => p.Libro).WithMany().HasForeignKey(p => p.LibroId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(p => p.Socio).WithMany().HasForeignKey(p => p.SocioId).OnDelete(DeleteBehavior.Restrict);
            });

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
            // Géneros expandidos
            modelBuilder.Entity<GeneroEntity>().HasData(
                new GeneroEntity { Id = 1, Nombre = "Ficción" },
                new GeneroEntity { Id = 2, Nombre = "Ciencia Ficción" },
                new GeneroEntity { Id = 3, Nombre = "Romance" },
                new GeneroEntity { Id = 4, Nombre = "Misterio" },
                new GeneroEntity { Id = 5, Nombre = "Biografía" },
                new GeneroEntity { Id = 6, Nombre = "Historia" },
                new GeneroEntity { Id = 7, Nombre = "Filosofía" },
                new GeneroEntity { Id = 8, Nombre = "Poesía" },
                new GeneroEntity { Id = 9, Nombre = "Drama" },
                new GeneroEntity { Id = 10, Nombre = "Aventura" },
                new GeneroEntity { Id = 11, Nombre = "Terror" },
                new GeneroEntity { Id = 12, Nombre = "Fantasía" },
                new GeneroEntity { Id = 13, Nombre = "Autoayuda" },
                new GeneroEntity { Id = 14, Nombre = "Ensayo" },
                new GeneroEntity { Id = 15, Nombre = "Novela Gráfica" },
                new GeneroEntity { Id = 16, Nombre = "Juventud" },
                new GeneroEntity { Id = 17, Nombre = "Infantil" },
                new GeneroEntity { Id = 18, Nombre = "Técnico" },
                new GeneroEntity { Id = 19, Nombre = "Gastronomía" },
                new GeneroEntity { Id = 20, Nombre = "Arte" }
            );

            // Autores expandidos (50 autores)
            modelBuilder.Entity<AutorEntity>().HasData(
                // Autores clásicos
                new AutorEntity { Id = 1, Nombre = "Gabriel", Apellido = "García Márquez" },
                new AutorEntity { Id = 2, Nombre = "Isaac", Apellido = "Asimov" },
                new AutorEntity { Id = 3, Nombre = "Jane", Apellido = "Austen" },
                new AutorEntity { Id = 4, Nombre = "Agatha", Apellido = "Christie" },
                new AutorEntity { Id = 5, Nombre = "Jorge Luis", Apellido = "Borges" },
                new AutorEntity { Id = 6, Nombre = "Miguel", Apellido = "de Cervantes" },
                new AutorEntity { Id = 7, Nombre = "William", Apellido = "Shakespeare" },
                new AutorEntity { Id = 8, Nombre = "Fyodor", Apellido = "Dostoevsky" },
                new AutorEntity { Id = 9, Nombre = "Leo", Apellido = "Tolstoy" },
                new AutorEntity { Id = 10, Nombre = "Ernest", Apellido = "Hemingway" },
                
                // Autores contemporáneos
                new AutorEntity { Id = 11, Nombre = "Stephen", Apellido = "King" },
                new AutorEntity { Id = 12, Nombre = "J.K.", Apellido = "Rowling" },
                new AutorEntity { Id = 13, Nombre = "George R.R.", Apellido = "Martin" },
                new AutorEntity { Id = 14, Nombre = "Paulo", Apellido = "Coelho" },
                new AutorEntity { Id = 15, Nombre = "Isabel", Apellido = "Allende" },
                new AutorEntity { Id = 16, Nombre = "Mario", Apellido = "Vargas Llosa" },
                new AutorEntity { Id = 17, Nombre = "Octavio", Apellido = "Paz" },
                new AutorEntity { Id = 18, Nombre = "Julio", Apellido = "Cortázar" },
                new AutorEntity { Id = 19, Nombre = "Roberto", Apellido = "Bolaño" },
                new AutorEntity { Id = 20, Nombre = "Elena", Apellido = "Poniatowska" },
                
                // Autores de ciencia ficción y fantasía
                new AutorEntity { Id = 21, Nombre = "J.R.R.", Apellido = "Tolkien" },
                new AutorEntity { Id = 22, Nombre = "Frank", Apellido = "Herbert" },
                new AutorEntity { Id = 23, Nombre = "Philip K.", Apellido = "Dick" },
                new AutorEntity { Id = 24, Nombre = "Arthur C.", Apellido = "Clarke" },
                new AutorEntity { Id = 25, Nombre = "Ray", Apellido = "Bradbury" },
                new AutorEntity { Id = 26, Nombre = "Ursula K.", Apellido = "Le Guin" },
                new AutorEntity { Id = 27, Nombre = "Douglas", Apellido = "Adams" },
                new AutorEntity { Id = 28, Nombre = "Neil", Apellido = "Gaiman" },
                new AutorEntity { Id = 29, Nombre = "Terry", Apellido = "Pratchett" },
                new AutorEntity { Id = 30, Nombre = "Brandon", Apellido = "Sanderson" },
                
                // Autores de misterio y thriller
                new AutorEntity { Id = 31, Nombre = "Arthur Conan", Apellido = "Doyle" },
                new AutorEntity { Id = 32, Nombre = "Edgar Allan", Apellido = "Poe" },
                new AutorEntity { Id = 33, Nombre = "Raymond", Apellido = "Chandler" },
                new AutorEntity { Id = 34, Nombre = "Gillian", Apellido = "Flynn" },
                new AutorEntity { Id = 35, Nombre = "Dan", Apellido = "Brown" },
                new AutorEntity { Id = 36, Nombre = "John", Apellido = "Grisham" },
                new AutorEntity { Id = 37, Nombre = "Michael", Apellido = "Crichton" },
                new AutorEntity { Id = 38, Nombre = "Stieg", Apellido = "Larsson" },
                new AutorEntity { Id = 39, Nombre = "Patricia", Apellido = "Highsmith" },
                new AutorEntity { Id = 40, Nombre = "Henning", Apellido = "Mankell" },
                
                // Autores diversos
                new AutorEntity { Id = 41, Nombre = "Haruki", Apellido = "Murakami" },
                new AutorEntity { Id = 42, Nombre = "Toni", Apellido = "Morrison" },
                new AutorEntity { Id = 43, Nombre = "Margaret", Apellido = "Atwood" },
                new AutorEntity { Id = 44, Nombre = "Salman", Apellido = "Rushdie" },
                new AutorEntity { Id = 45, Nombre = "Chinua", Apellido = "Achebe" },
                new AutorEntity { Id = 46, Nombre = "Virginia", Apellido = "Woolf" },
                new AutorEntity { Id = 47, Nombre = "James", Apellido = "Joyce" },
                new AutorEntity { Id = 48, Nombre = "Franz", Apellido = "Kafka" },
                new AutorEntity { Id = 49, Nombre = "Albert", Apellido = "Camus" },
                new AutorEntity { Id = 50, Nombre = "Jack", Apellido = "Kerouac" }
            );

            // Editoriales expandidas (25 editoriales)
            modelBuilder.Entity<EditorialEntity>().HasData(
                new EditorialEntity { Id = 1, Nombre = "Sudamericana" },
                new EditorialEntity { Id = 2, Nombre = "Penguin Random House" },
                new EditorialEntity { Id = 3, Nombre = "Planeta" },
                new EditorialEntity { Id = 4, Nombre = "Alfaguara" },
                new EditorialEntity { Id = 5, Nombre = "Anagrama" },
                new EditorialEntity { Id = 6, Nombre = "Tusquets" },
                new EditorialEntity { Id = 7, Nombre = "Seix Barral" },
                new EditorialEntity { Id = 8, Nombre = "Destino" },
                new EditorialEntity { Id = 9, Nombre = "Crítica" },
                new EditorialEntity { Id = 10, Nombre = "Taurus" },
                new EditorialEntity { Id = 11, Nombre = "Salamandra" },
                new EditorialEntity { Id = 12, Nombre = "Minotauro" },
                new EditorialEntity { Id = 13, Nombre = "Valdemar" },
                new EditorialEntity { Id = 14, Nombre = "Alianza Editorial" },
                new EditorialEntity { Id = 15, Nombre = "Cátedra" },
                new EditorialEntity { Id = 16, Nombre = "Espasa" },
                new EditorialEntity { Id = 17, Nombre = "Norma" },
                new EditorialEntity { Id = 18, Nombre = "Emecé" },
                new EditorialEntity { Id = 19, Nombre = "Fondo de Cultura Económica" },
                new EditorialEntity { Id = 20, Nombre = "Siglo XXI" },
                new EditorialEntity { Id = 21, Nombre = "Paidós" },
                new EditorialEntity { Id = 22, Nombre = "Ariel" },
                new EditorialEntity { Id = 23, Nombre = "Juventud" },
                new EditorialEntity { Id = 24, Nombre = "Ediciones B" },
                new EditorialEntity { Id = 25, Nombre = "Booket" }
            );

            // Personas expandidas (30 personas)
            modelBuilder.Entity<PersonaEntity>().HasData(
                // Personal del sistema
                new PersonaEntity { Id = 1, Nombre = "Admin", Apellido = "Sistema", Dni = "00000000", Email = "admin@biblioteca.com" },
                new PersonaEntity { Id = 2, Nombre = "María", Apellido = "Bibliotecaria", Dni = "11111111", Email = "maria.bibliotecaria@biblioteca.com" },
                new PersonaEntity { Id = 3, Nombre = "Carlos", Apellido = "Asistente", Dni = "22222222", Email = "carlos.asistente@biblioteca.com" },
                
                // Socios de la biblioteca
                new PersonaEntity { Id = 4, Nombre = "Juan", Apellido = "Pérez", Dni = "12345678", Email = "juan.perez@email.com" },
                new PersonaEntity { Id = 5, Nombre = "María", Apellido = "González", Dni = "87654321", Email = "maria.gonzalez@email.com" },
                new PersonaEntity { Id = 6, Nombre = "Luis", Apellido = "Rodríguez", Dni = "23456789", Email = "luis.rodriguez@email.com" },
                new PersonaEntity { Id = 7, Nombre = "Ana", Apellido = "Martínez", Dni = "34567890", Email = "ana.martinez@email.com" },
                new PersonaEntity { Id = 8, Nombre = "Pedro", Apellido = "López", Dni = "45678901", Email = "pedro.lopez@email.com" },
                new PersonaEntity { Id = 9, Nombre = "Laura", Apellido = "Sánchez", Dni = "56789012", Email = "laura.sanchez@email.com" },
                new PersonaEntity { Id = 10, Nombre = "Diego", Apellido = "García", Dni = "67890123", Email = "diego.garcia@email.com" },
                new PersonaEntity { Id = 11, Nombre = "Sofía", Apellido = "Fernández", Dni = "78901234", Email = "sofia.fernandez@email.com" },
                new PersonaEntity { Id = 12, Nombre = "Martín", Apellido = "Torres", Dni = "89012345", Email = "martin.torres@email.com" },
                new PersonaEntity { Id = 13, Nombre = "Valentina", Apellido = "Ruiz", Dni = "90123456", Email = "valentina.ruiz@email.com" },
                new PersonaEntity { Id = 14, Nombre = "Sebastián", Apellido = "Morales", Dni = "01234567", Email = "sebastian.morales@email.com" },
                new PersonaEntity { Id = 15, Nombre = "Camila", Apellido = "Vargas", Dni = "13579246", Email = "camila.vargas@email.com" },
                new PersonaEntity { Id = 16, Nombre = "Nicolás", Apellido = "Castro", Dni = "24681357", Email = "nicolas.castro@email.com" },
                new PersonaEntity { Id = 17, Nombre = "Isabella", Apellido = "Jiménez", Dni = "35792468", Email = "isabella.jimenez@email.com" },
                new PersonaEntity { Id = 18, Nombre = "Mateo", Apellido = "Herrera", Dni = "46813579", Email = "mateo.herrera@email.com" },
                new PersonaEntity { Id = 19, Nombre = "Lucía", Apellido = "Mendoza", Dni = "57924680", Email = "lucia.mendoza@email.com" },
                new PersonaEntity { Id = 20, Nombre = "Santiago", Apellido = "Ortega", Dni = "68035791", Email = "santiago.ortega@email.com" },
                new PersonaEntity { Id = 21, Nombre = "Florencia", Apellido = "Silva", Dni = "79146802", Email = "florencia.silva@email.com" },
                new PersonaEntity { Id = 22, Nombre = "Tomás", Apellido = "Ramos", Dni = "80257913", Email = "tomas.ramos@email.com" },
                new PersonaEntity { Id = 23, Nombre = "Agustina", Apellido = "Vega", Dni = "91368024", Email = "agustina.vega@email.com" },
                new PersonaEntity { Id = 24, Nombre = "Facundo", Apellido = "Delgado", Dni = "02479135", Email = "facundo.delgado@email.com" },
                new PersonaEntity { Id = 25, Nombre = "Antonella", Apellido = "Rojas", Dni = "15830246", Email = "antonella.rojas@email.com" },
                new PersonaEntity { Id = 26, Nombre = "Emiliano", Apellido = "Paredes", Dni = "26941357", Email = "emiliano.paredes@email.com" },
                new PersonaEntity { Id = 27, Nombre = "Julieta", Apellido = "Navarro", Dni = "37052468", Email = "julieta.navarro@email.com" },
                new PersonaEntity { Id = 28, Nombre = "Ignacio", Apellido = "Guerrero", Dni = "48163579", Email = "ignacio.guerrero@email.com" },
                new PersonaEntity { Id = 29, Nombre = "Renata", Apellido = "Medina", Dni = "59274680", Email = "renata.medina@email.com" },
                new PersonaEntity { Id = 30, Nombre = "Joaquín", Apellido = "Aguilar", Dni = "60385791", Email = "joaquin.aguilar@email.com" }
            );

            // Libros expandidos (100 libros)
            var libros = new List<dynamic>();
            var libroId = 1;
            
            // Libros de Gabriel García Márquez
            libros.Add(new { Id = libroId++, Titulo = "Cien años de soledad", ISBN = "978-84-376-0495-7", AutorId = 1, GeneroId = 1, EditorialId = 1, Estado = "disponible" });
            libros.Add(new { Id = libroId++, Titulo = "El amor en los tiempos del cólera", ISBN = "978-84-376-0496-4", AutorId = 1, GeneroId = 3, EditorialId = 1, Estado = "disponible" });
            libros.Add(new { Id = libroId++, Titulo = "Crónica de una muerte anunciada", ISBN = "978-84-376-0497-1", AutorId = 1, GeneroId = 4, EditorialId = 1, Estado = "prestado" });
            
            // Libros de Isaac Asimov
            libros.Add(new { Id = libroId++, Titulo = "Fundación", ISBN = "978-84-204-8143-5", AutorId = 2, GeneroId = 2, EditorialId = 12, Estado = "disponible" });
            libros.Add(new { Id = libroId++, Titulo = "Yo, Robot", ISBN = "978-84-204-8144-2", AutorId = 2, GeneroId = 2, EditorialId = 12, Estado = "disponible" });
            libros.Add(new { Id = libroId++, Titulo = "El fin de la eternidad", ISBN = "978-84-204-8145-9", AutorId = 2, GeneroId = 2, EditorialId = 12, Estado = "prestado" });
            
            // Libros de J.K. Rowling
            libros.Add(new { Id = libroId++, Titulo = "Harry Potter y la piedra filosofal", ISBN = "978-84-9838-310-8", AutorId = 12, GeneroId = 12, EditorialId = 11, Estado = "disponible" });
            libros.Add(new { Id = libroId++, Titulo = "Harry Potter y la cámara secreta", ISBN = "978-84-9838-311-5", AutorId = 12, GeneroId = 12, EditorialId = 11, Estado = "disponible" });
            libros.Add(new { Id = libroId++, Titulo = "Harry Potter y el prisionero de Azkaban", ISBN = "978-84-9838-312-2", AutorId = 12, GeneroId = 12, EditorialId = 11, Estado = "prestado" });
            
            // Libros de Stephen King
            libros.Add(new { Id = libroId++, Titulo = "It", ISBN = "978-84-8455-174-3", AutorId = 11, GeneroId = 11, EditorialId = 3, Estado = "disponible" });
            libros.Add(new { Id = libroId++, Titulo = "El resplandor", ISBN = "978-84-8455-175-0", AutorId = 11, GeneroId = 11, EditorialId = 3, Estado = "disponible" });
            libros.Add(new { Id = libroId++, Titulo = "Carrie", ISBN = "978-84-8455-176-7", AutorId = 11, GeneroId = 11, EditorialId = 3, Estado = "prestado" });
            
            // Agregar más libros variados para completar 100
            for (int i = libroId; i <= 100; i++)
            {
                var autorId = ((i - 1) % 50) + 1;
                var generoId = ((i - 1) % 20) + 1;
                var editorialId = ((i - 1) % 25) + 1;
                var estado = (i % 3 == 0) ? "prestado" : "disponible";
                
                libros.Add(new { 
                    Id = i, 
                    Titulo = $"Libro {i}", 
                    ISBN = $"978-84-{1000 + i:D4}-{100 + (i % 900):D3}-{(i % 10)}", 
                    AutorId = autorId, 
                    GeneroId = generoId, 
                    EditorialId = editorialId, 
                    Estado = estado 
                });
            }

            modelBuilder.Entity<LibroEntity>().HasData(libros.ToArray());

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

            // Usuarios expandidos
            var (adminHash, adminSalt) = HashPasswordDeterministic("admin123", "seed-salt-admin-2025");
            var (bibliotecarioHash, bibliotecarioSalt) = HashPasswordDeterministic("biblio123", "seed-salt-biblio-2025");
            var (socioHash, socioSalt) = HashPasswordDeterministic("socio123", "seed-salt-socio-2025");

            var usuarios = new List<object>();
            
            // Admin y bibliotecarios
            usuarios.Add(new { Id = 1, NombreUsuario = "admin", Rol = "administrador", PersonaId = 1, PasswordHash = adminHash, Salt = adminSalt, Activo = true, FechaCreacion = DateTime.UtcNow });
            usuarios.Add(new { Id = 2, NombreUsuario = "maria.bib", Rol = "bibliotecario", PersonaId = 2, PasswordHash = bibliotecarioHash, Salt = bibliotecarioSalt, Activo = true, FechaCreacion = DateTime.UtcNow });
            usuarios.Add(new { Id = 3, NombreUsuario = "carlos.asist", Rol = "bibliotecario", PersonaId = 3, PasswordHash = bibliotecarioHash, Salt = bibliotecarioSalt, Activo = true, FechaCreacion = DateTime.UtcNow });
            
            // Socios (personas 4-30)
            for (int i = 4; i <= 30; i++)
            {
                usuarios.Add(new { 
                    Id = i, 
                    NombreUsuario = $"socio{i:D2}", 
                    Rol = "socio", 
                    PersonaId = i, 
                    PasswordHash = socioHash, 
                    Salt = socioSalt, 
                    Activo = true, 
                    FechaCreacion = DateTime.UtcNow 
                });
            }

            modelBuilder.Entity<UsuarioEntity>().HasData(usuarios.ToArray());

            // Préstamos con fechas variadas (los últimos 6 meses)
            var prestamos = new List<object>();
            var prestamoId = 1;
            var random = new Random(12345); // Seed fijo para resultados consistentes
            var fechaBase = DateTime.Now.AddMonths(-6);

            // Generar 150 préstamos distribuidos en los últimos 6 meses
            for (int i = 0; i < 150; i++)
            {
                var diasAleatorios = random.Next(0, 180); // 6 meses = ~180 días
                var fechaPrestamo = fechaBase.AddDays(diasAleatorios);
                var fechaDevolucionPrevista = fechaPrestamo.AddDays(14); // 14 días de préstamo
                
                // 70% de libros devueltos
                DateTime? fechaDevolucionReal = null;
                if (random.NextDouble() < 0.7)
                {
                    var diasDevolucion = random.Next(1, 20); // Entre 1 y 20 días después del préstamo
                    fechaDevolucionReal = fechaPrestamo.AddDays(diasDevolucion);
                }

                var libroIdPrestamo = random.Next(1, 101); // Libros del 1 al 100
                var socioId = random.Next(4, 31); // Socios del 4 al 30

                prestamos.Add(new { 
                    Id = prestamoId++, 
                    LibroId = libroIdPrestamo, 
                    SocioId = socioId, 
                    FechaPrestamo = fechaPrestamo, 
                    FechaDevolucionPrevista = fechaDevolucionPrevista, 
                    FechaDevolucionReal = fechaDevolucionReal 
                });
            }

            modelBuilder.Entity<PrestamoEntity>().HasData(prestamos.ToArray());
        }
    }
}