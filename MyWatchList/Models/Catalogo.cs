namespace MyWatchList.Models;

public class Catalogo
{
    public List<Obra> ListarObrasEmAlta(string periodo) => new(); //dia,semana, mes, ano
    public List<Obra> ListarMaisBemAvaliadas() => new(); //nota media
    public List<Obra> ListarMaisPopulares() => new(); //mais visualizadas
    public List<Obra> ListarPorGenero(string genero) => new(); //acao, aventura, comedia, drama, fantasia, ficcao-cientifica, terror, romance, suspense
    public List<Ator> ListarCelebridadesEmAlta() => new(); 
}