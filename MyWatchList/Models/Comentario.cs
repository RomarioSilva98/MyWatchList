using MyWatchList.Models;

public class Comentario
{
    public int Id { get; set; }
    public string Texto { get; set; }
    public int Nota { get; set; }
    public string Status { get; set; }
    public string Progresso { get; set; }
    public DateTime Data { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public int ObraId { get; set; }
    public Obra Obra { get; set; }
}
