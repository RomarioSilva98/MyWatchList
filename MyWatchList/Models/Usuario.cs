using System.ComponentModel.DataAnnotations;

namespace MyWatchList.Models
{

    public enum TipoUsuario
    {
        Comum,
        Admin
    }
    public class Usuario
    {
        public int Id { get; set; }
        
        [Required]
        public string Nome { get; set; }
       
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Senha { get; set; }
        public string? FotoPerfil { get; set; }
        public string? Biografia { get; set; }

        public TipoUsuario Tipo { get; set; } = TipoUsuario.Comum;

        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public ICollection<ListaPersonalizada> Listas { get; set; } = new List<ListaPersonalizada>();
        public ICollection<UsuarioObraWatchlist> Watchlist { get; set; } = new List<UsuarioObraWatchlist>();
        public ICollection<UsuarioObraAssistida> Assistidas { get; set; } = new List<UsuarioObraAssistida>();
    }

}