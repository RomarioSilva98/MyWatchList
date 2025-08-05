using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWatchList.Data;
using MyWatchList.Models;

namespace MyWatchList.Pages.Usuarios;

public class MyWatchlistModel : PageModel
{
    private readonly AppDbContext _context;

    public MyWatchlistModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Obra> Obras { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
        if (usuarioId == null)
            return RedirectToPage("/Usuarios/Login");

        Obras = await _context.UsuarioObrasWatchlist
            .Where(w => w.UsuarioId == usuarioId)
            .Include(w => w.Obra).ThenInclude(o => o.Fotos)
            .Select(w => w.Obra)
            .ToListAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostRemoverAsync(int obraId)
    {
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
        if (usuarioId == null)
            return RedirectToPage("/Usuarios/Login");

        var item = await _context.UsuarioObrasWatchlist
            .FirstOrDefaultAsync(x => x.ObraId == obraId && x.UsuarioId == usuarioId);

        if (item != null)
        {
            _context.UsuarioObrasWatchlist.Remove(item);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage();
    }
}
