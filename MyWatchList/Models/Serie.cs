namespace MyWatchList.Models;

public class Serie : Obra
{
    public ICollection<Temporada> Temporadas { get; set; } = new List<Temporada>();
}