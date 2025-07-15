namespace MyWatchList.Models;

public class Catalogo
{
    public List<Obra> ListarObrasEmAlta(string periodo) => new();
    public List<Obra> ListarMaisBemAvaliadas() => new();
    public List<Obra> ListarMaisPopulares() => new();
    public List<Obra> ListarPorGenero(string genero) => new();
    public List<Ator> ListarCelebridadesEmAlta() => new();
}