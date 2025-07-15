using MyWatchList.Models;

public class ListaPersonalizada
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public ICollection<ListaObra> Obras { get; set; }
}
