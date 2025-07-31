using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWatchList.Data;
using MyWatchList.Models;

namespace MyWatchList.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context) => _context = context;

    public List<Obra> EmAltaDia { get; set; } = new();
    public List<Obra> EmAltaSemana { get; set; } = new();
    public List<Obra> EmAltaMes { get; set; } = new();
    public List<Obra> EmAltaAno { get; set; } = new();

    public Dictionary<string, List<Obra>> ObrasPorGenero { get; set; } = new();
    public List<Ator> AtoresEmAlta { get; set; } = new();

    public void OnGet()
    {
        EmAltaDia = GetObras(o => o.PopularidadeDia);
        EmAltaSemana = GetObras(o => o.PopularidadeSemana);
        EmAltaMes = GetObras(o => o.PopularidadeMes);
        EmAltaAno = GetObras(o => o.PopularidadeAno);

        var generos = new[] { "Ação", "Comédia", "Drama", "Romance", "Ficção Científica" };
        ObrasPorGenero = generos.ToDictionary(
            genero => genero,
            genero => _context.Obras
                .Include(o => o.Fotos)
                .Include(o => o.Generos)
                .Where(o => o.Generos.Any(g => g.Nome == genero))
                .OrderByDescending(o => o.NotaMedia)
                .Take(10)
                .ToList()
        );

        AtoresEmAlta = _context.Atores
            .Include(a => a.Obras)
                .ThenInclude(oa => oa.Obra)
            .Select(a => new
            {
                Ator = a,
                Pontos = a.Obras
                    .Sum(oa => oa.Obra.PopularidadeDia + oa.Obra.PopularidadeSemana + oa.Obra.PopularidadeMes + oa.Obra.PopularidadeAno)
            })
            .OrderByDescending(a => a.Pontos)
            .Take(10)
            .Select(a => a.Ator)
            .ToList();
    }

    private List<Obra> GetObras(Func<Obra, int> orderSelector) =>
        _context.Obras
            .Include(o => o.Fotos)
            .Include(o => o.Generos)
            .OrderByDescending(orderSelector)
            .Take(10)
            .ToList();
}
