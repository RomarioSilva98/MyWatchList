namespace MyWatchList.Models;

public class Obra
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Sinopse { get; set; }
    public float NotaMedia { get; set; }
    public string Genero { get; set; }
    public int PopularidadeDia { get; set; }
    public int PopularidadeSemana { get; set; }
    public int PopularidadeMes { get; set; }
    public int PopularidadeAno { get; set; }

    public ICollection<Comentario> Comentarios { get; set; }
    public ICollection<ObraAtor> Elenco { get; set; }
}
