using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWatchList.Data;
using MyWatchList.Models;

namespace MyWatchList.Pages.Obras;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;

    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    public Obra Obra { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Obra = await _context.Obras
            .Include(o => o.Generos)
            .Include(o => o.Fotos)
            .Include(o => o.Videos)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Obra == null)
            return NotFound();

        return Page();
    }
}
