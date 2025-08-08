using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWatchList.Data;
using MyWatchList.Models;

namespace MyWatchList.Pages.ListasPersonalizadas;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateModel(AppDbContext context, IHttpContextAccessor accessor)
    {
        _context = context;
        _httpContextAccessor = accessor;
    }

    [BindProperty]
    [ValidateNever]
    public ListaPersonalizada ListaPersonalizada { get; set; } = new();

    [BindProperty]
    public string? ObraIds { get; set; }  // CSV: "1,3,5"

    public List<Obra> TodasObras { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        TodasObras = await _context.Obras.OrderBy(o => o.Titulo).ToListAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var userId = _httpContextAccessor.HttpContext.Session.GetInt32("UsuarioId");
        if (userId == null)
            return RedirectToPage("/Usuarios/Login");

        // ✅ Fix: Set UsuarioId antes da validação
        ListaPersonalizada.UsuarioId = userId.Value;

        if (!ModelState.IsValid)
        {
            TodasObras = await _context.Obras.OrderBy(o => o.Titulo).ToListAsync();
            return Page();
        }

        // ✅ Criação da lista
        _context.ListaPersonalizada.Add(ListaPersonalizada);
        await _context.SaveChangesAsync();

        // ✅ Adiciona obras
        var obraIds = (ObraIds ?? "")
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        foreach (var obraId in obraIds)
        {
            _context.ListaObras.Add(new ListaObra
            {
                ListaId = ListaPersonalizada.Id,
                ObraId = obraId
            });
        }

        await _context.SaveChangesAsync();
        return RedirectToPage("./Index", new { id = ListaPersonalizada.Id });
    }


}
