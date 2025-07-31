using Microsoft.EntityFrameworkCore;
using MyWatchList.Models;


namespace MyWatchList.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Obra> Obras { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Anime> Animes { get; set; }

        public DbSet<Temporada> Temporadas { get; set; }
        public DbSet<Episodio> Episodios { get; set; }
        public DbSet<Usuario> Usuario { get; set; } = default!;
        public DbSet<ObraAtor> ObraAtores { get; set; }
        public DbSet<ListaObra> ListaObras { get; set; }
        public DbSet<UsuarioObraAssistida> UsuarioObrasAssistidas { get; set; }
        public DbSet<UsuarioObraWatchlist> UsuarioObrasWatchlist { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Ator> Atores { get; set; }

        public DbSet<Foto> Fotos { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Genero> Generos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Obra>()
                .HasDiscriminator<string>("TipoObra")
                .HasValue<Obra>("Obra")
                .HasValue<Filme>("Filme")
                .HasValue<Serie>("Serie")
                .HasValue<Anime>("Anime");


            modelBuilder.Entity<Obra>()
                 .HasMany(o => o.Fotos)
                 .WithOne(f => f.Obra)
                 .HasForeignKey(f => f.ObraId)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Obra>()
                .HasMany(o => o.Videos)
                .WithOne(v => v.Obra)
                .HasForeignKey(v => v.ObraId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Obra>()
                .HasMany(o => o.Generos)
                .WithOne(g => g.Obra)
                .HasForeignKey(g => g.ObraId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Temporada>()
                .HasOne(t => t.Obra)
                .WithMany()
                .HasForeignKey(t => t.ObraId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Episodio>()
                .HasOne(e => e.Temporada)
                .WithMany(t => t.Episodios)
                .HasForeignKey(e => e.TemporadaId);

            modelBuilder.Entity<ObraAtor>()
                .HasKey(oa => new { oa.ObraId, oa.AtorId });

            modelBuilder.Entity<ListaObra>()
                .HasKey(lo => new { lo.ListaId, lo.ObraId });

            modelBuilder.Entity<UsuarioObraAssistida>()
                .HasKey(uoa => new { uoa.UsuarioId, uoa.ObraId });

            modelBuilder.Entity<UsuarioObraWatchlist>()
                .HasKey(uow => new { uow.UsuarioId, uow.ObraId });


        }
    }
}
