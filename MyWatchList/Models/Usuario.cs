namespace MyWatchList.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string FotoPerfil { get; set; }
        public string Biografia { get; set; }

        public ICollection<Comentario> Comentarios { get; set; }
        public ICollection<ListaPersonalizada> Listas { get; set; }
        public ICollection<UsuarioObraWatchlist> Watchlist { get; set; }
        public ICollection<UsuarioObraAssistida> Assistidas { get; set; }
    }

}