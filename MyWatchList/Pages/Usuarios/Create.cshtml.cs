using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWatchList.Data;
using MyWatchList.Models;

namespace MyWatchList.Pages.Usuarios;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Usuario Usuario { get; set; }

    public void OnGet() { }

    public IActionResult OnPost()
    {
        // ⚠️ Log de tentativa
        System.Diagnostics.Debug.WriteLine("===> Tentando cadastrar usuário...");

        if (!ModelState.IsValid)
        {
            System.Diagnostics.Debug.WriteLine("===> ModelState inválido:");
            foreach (var entry in ModelState)
            {
                var key = entry.Key;
                var errors = entry.Value.Errors;
                foreach (var error in errors)
                {
                    Console.WriteLine($" - {key}: {error.ErrorMessage}");
                }
            }
            return Page();
        }

        try
        {
            Usuario.Tipo = TipoUsuario.Comum;
            _context.Usuario.Add(Usuario);
            _context.SaveChanges();

            System.Diagnostics.Debug.WriteLine("===> Usuário salvo com sucesso!");
            return RedirectToPage("/Usuarios/Login");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("===> Erro ao salvar no banco: " + ex.Message);
            ModelState.AddModelError(string.Empty, "Erro ao salvar no banco de dados.");
            return Page();
        }
    }

}
