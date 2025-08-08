using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MyWatchList.Models;

public class ListaPersonalizada
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }

    public int UsuarioId { get; set; }

    [ValidateNever]
    public Usuario Usuario { get; set; }

    [ValidateNever]
    public ICollection<ListaObra> Obras { get; set; }
}
