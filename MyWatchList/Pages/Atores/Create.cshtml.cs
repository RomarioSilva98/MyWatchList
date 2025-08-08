using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWatchList.Data;
using MyWatchList.Models;

namespace MyWatchList.Pages.Atores
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        // bind na propriedade Ator (nome dos inputs: Ator.Nome, Ator.Foto, ...)
        [BindProperty]
        public Ator Ator { get; set; } = new(); // inicializa para evitar nulls

        public async Task<IActionResult> OnPostAsync()
        {
            // Se ModelState inválido mostra erros (e volta pra view)
            if (!ModelState.IsValid)
            {
                // opcional: log dos erros para debug (se tiver logger)
                // foreach (var kv in ModelState) { ... }
                return Page();
            }

            try
            {
                _context.Atores.Add(Ator);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                // captura erro do banco e mostra no topo da página
                ModelState.AddModelError(string.Empty, "Erro ao salvar no banco: " + ex.Message);
                return Page();
            }
        }
    }
}
