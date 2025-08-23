using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWatchList.Data;
using MyWatchList.Helpers;
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
       

        if (!ModelState.IsValid)
        {
            
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
            if (_context.Usuario.Any(u => u.Email == Usuario.Email))
            {
                ModelState.AddModelError("Usuario.Email", "Já existe uma conta com este e-mail.");
                return Page();
            }
            Usuario.Tipo = TipoUsuario.Comum;
            Usuario.Senha = PasswordHelper.HashPassword(Usuario, Usuario.Senha);
            _context.Usuario.Add(Usuario);
            _context.SaveChanges();

           
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
