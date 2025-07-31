using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyWatchList.Pages.Usuarios;

public class LogoutModel : PageModel
{
    public IActionResult OnGet()
    {
        HttpContext.Session.Clear(); // ?? Encerra a sess�o
        return RedirectToPage("/Index"); // ?? Redireciona para a home
    }
}