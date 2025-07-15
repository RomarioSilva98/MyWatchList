namespace MyWatchList.Models;

public class ListaObra
{
    public int ListaId { get; set; }
    public ListaPersonalizada Lista { get; set; }
    public int ObraId { get; set; }
    public Obra Obra { get; set; }
}
