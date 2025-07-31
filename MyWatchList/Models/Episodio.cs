namespace MyWatchList.Models;

public class Episodio
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public int Numero { get; set; }
    public string Descricao { get; set; }

    public int TemporadaId { get; set; }
    public Temporada Temporada { get; set; }
}