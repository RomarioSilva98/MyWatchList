using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWatchList.Data;
using MyWatchList.Models;

namespace MyWatchList.Pages.Usuarios;

public class LoginModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoginModel(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    [BindProperty] public string Email { get; set; }
    [BindProperty] public string Senha { get; set; }
    public string MensagemErro { get; set; }

    public void OnGet() { }

    public IActionResult OnPost()
    {
        var usuario = _context.Usuario.FirstOrDefault(u => u.Email == Email && u.Senha == Senha);
        if (usuario == null)
        {
            MensagemErro = "Email ou senha inválidos.";
            return Page();
        }

        var session = _httpContextAccessor.HttpContext.Session;
        session.SetInt32("UsuarioId", usuario.Id);
        session.SetString("UsuarioTipo", usuario.Tipo.ToString());

        return RedirectToPage("/Index");
    }
}
