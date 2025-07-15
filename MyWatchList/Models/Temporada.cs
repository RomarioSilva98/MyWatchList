namespace MyWatchList.Models;

public class Temporada
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public int ObraId { get; set; }
    public Obra Obra { get; set; }
    public ICollection<Episodio> Episodios { get; set; }
}