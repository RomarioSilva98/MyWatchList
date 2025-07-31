using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MyWatchList.Models;

public class Obra
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Sinopse { get; set; }
    public float NotaMedia { get; set; }

    public int PopularidadeDia { get; set; }
    public int PopularidadeSemana { get; set; }
    public int PopularidadeMes { get; set; }
    public int PopularidadeAno { get; set; }

    public ICollection<Genero> Generos { get; set; } = new List<Genero>();
    public ICollection<Foto> Fotos { get; set; } = new List<Foto>();
    public ICollection<Video> Videos { get; set; } = new List<Video>();

    public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    public ICollection<ObraAtor> Elenco { get; set; } = new List<ObraAtor>();
}
