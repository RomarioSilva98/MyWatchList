namespace MyWatchList.Models;
public class Ator
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Biografia { get; set; }
    public string Foto { get; set; }

    public ICollection<ObraAtor> Obras { get; set; }
}