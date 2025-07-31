namespace MyWatchList.Models;

public class Anime : Obra
{
    public ICollection<Temporada> Temporadas { get; set; } = new List<Temporada>();
}