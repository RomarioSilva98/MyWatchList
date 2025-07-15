using MyWatchList.Models;

public class UsuarioObraAssistida
{
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public int ObraId { get; set; }
    public Obra Obra { get; set; }
}