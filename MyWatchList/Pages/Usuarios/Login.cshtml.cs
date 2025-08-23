using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWatchList.Data;
using MyWatchList.Models;
using MyWatchList.Helpers;
using MyWatchList.Helpers;

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
        var usuario = _context.Usuario.FirstOrDefault(u => u.Email == Email);
        if (usuario == null)
        {
            MensagemErro = "Email ou senha inválidos.";
            return Page();
        }

        bool isHash = PasswordHelper.LooksHashed(usuario.Senha);
        bool ok = isHash
            ? PasswordHelper.VerifyPassword(usuario, Senha, usuario.Senha)
            : usuario.Senha == Senha; // compatibilidade com senhas em texto puro

        if (!ok)
        {
            MensagemErro = "Email ou senha inválidos.";
            return Page();
        }

        // Auto-upgrade: se ainda estava em texto puro, aplica hash agora
        if (!isHash)
        {
            usuario.Senha = PasswordHelper.HashPassword(usuario, Senha);
            _context.SaveChanges();
        }

        var session = _httpContextAccessor.HttpContext.Session;
        session.SetInt32("UsuarioId", usuario.Id);
        session.SetString("UsuarioTipo", usuario.Tipo.ToString());

        return RedirectToPage("/Index");
    }
}
