using Microsoft.EntityFrameworkCore;
using MyWatchList.Models;

namespace MyWatchList.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Obra> Obras { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Anime> Animes { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Temporada> Temporadas { get; set; }
        public DbSet<Episodio> Episodios { get; set; }
        public DbSet<ListaPersonalizada> Listas { get; set; }
        public DbSet<Ator> Atores { get; set; }
        public DbSet<ObraAtor> ObraAtores { get; set; }
        public DbSet<ListaObra> ListaObras { get; set; }
        public DbSet<UsuarioObraWatchlist> Watchlist { get; set; }
        public DbSet<UsuarioObraAssistida> Assistidas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ObraAtor>().HasKey(oa => new { oa.ObraId, oa.AtorId });
            modelBuilder.Entity<ListaObra>().HasKey(lo => new { lo.ListaId, lo.ObraId });
            modelBuilder.Entity<UsuarioObraWatchlist>().HasKey(uo => new { uo.UsuarioId, uo.ObraId });
            modelBuilder.Entity<UsuarioObraAssistida>().HasKey(uo => new { uo.UsuarioId, uo.ObraId });
        }
    }
}
