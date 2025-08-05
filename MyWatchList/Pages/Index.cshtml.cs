using Microsoft.AspNetCore.Mvc;
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

    [BindProperty(SupportsGet = true)]
    public string Query { get; set; }

    public Obra? ObraBuscada { get; set; }
    public int? UsuarioId { get; set; }
    public bool IsAdmin { get; set; }


    public void OnGet()
    {
        UsuarioId = HttpContext.Session.GetInt32("UsuarioId");
        IsAdmin = HttpContext.Session.GetString("UsuarioTipo") == "Admin";

        if (!string.IsNullOrWhiteSpace(Query))
        {
            ObraBuscada = _context.Obras
                .Include(o => o.Fotos)
                .Include(o => o.Generos)
                .FirstOrDefault(o => o.Titulo.Contains(Query));
        }

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
            .ToList();
    }


    private List<Obra> GetObras(Func<Obra, int> orderSelector) =>
        _context.Obras
            .Include(o => o.Fotos)
            .Include(o => o.Generos)
            .OrderByDescending(orderSelector)
            .Take(10)
            .ToList();


    public async Task<IActionResult> OnPostAdicionarWatchlistAsync(int obraId)
    {
        UsuarioId = HttpContext.Session.GetInt32("UsuarioId");
        if (UsuarioId == null) return RedirectToPage("/Usuarios/Login");

        var existe = await _context.UsuarioObrasWatchlist
            .AnyAsync(u => u.UsuarioId == UsuarioId && u.ObraId == obraId);

        if (!existe)
        {
            _context.UsuarioObrasWatchlist.Add(new UsuarioObraWatchlist
            {
                UsuarioId = UsuarioId.Value,
                ObraId = obraId
            });
            await _context.SaveChangesAsync();
        }

        return RedirectToPage(new { query = Query });
    }

}
