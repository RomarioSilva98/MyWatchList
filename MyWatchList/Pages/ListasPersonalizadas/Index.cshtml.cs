using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWatchList.Data;
using MyWatchList.Models;

namespace MyWatchList.Pages.ListasPersonalizadas;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _accessor;

    public IndexModel(AppDbContext context, IHttpContextAccessor accessor)
    {
        _context = context;
        _accessor = accessor;
    }

    public List<ListaPersonalizada> ListasDoUsuario { get; set; } = new();
    public List<Obra> TodasObras { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        var userId = _accessor.HttpContext?.Session.GetInt32("UsuarioId");
        if (userId == null)
            return RedirectToPage("/Usuarios/Login");

        ListasDoUsuario = await _context.ListaPersonalizada
            .Where(l => l.UsuarioId == userId)
            .Include(l => l.Obras)
                .ThenInclude(lo => lo.Obra)
            .ToListAsync();

        TodasObras = await _context.Obras.OrderBy(o => o.Titulo).ToListAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostRemoverObraAsync(int listaId, int obraId)
    {
        var item = await _context.ListaObras.FindAsync(listaId, obraId);
        if (item != null)
        {
            _context.ListaObras.Remove(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostAdicionarObraAsync(int listaId, int obraId)
    {
        var existe = await _context.ListaObras.AnyAsync(lo => lo.ListaId == listaId && lo.ObraId == obraId);
        if (!existe)
        {
            _context.ListaObras.Add(new ListaObra { ListaId = listaId, ObraId = obraId });
            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditarListaAsync(int Id, string Nome, string Descricao)
    {
        var lista = await _context.ListaPersonalizada.FindAsync(Id);
        if (lista == null) return NotFound();

        lista.Nome = Nome;
        lista.Descricao = Descricao;
        await _context.SaveChangesAsync();
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostExcluirListaAsync(int Id)
    {
        var lista = await _context.ListaPersonalizada
            .Include(l => l.Obras)
            .FirstOrDefaultAsync(l => l.Id == Id);

        if (lista == null) return NotFound();

        _context.ListaObras.RemoveRange(lista.Obras);
        _context.ListaPersonalizada.Remove(lista);
        await _context.SaveChangesAsync();
        return RedirectToPage();
    }

}
