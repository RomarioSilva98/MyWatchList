using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWatchList.Data;
using MyWatchList.Models;

namespace MyWatchList.Pages.Atores
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Ator Ator { get; set; }

        [BindProperty]
        public bool Editando { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, bool? editar)
        {
            Ator = await _context.Atores.FirstOrDefaultAsync(a => a.Id == id);

            if (Ator == null)
            {
                return NotFound();
            }

            Editando = editar ?? false;
            return Page();
        }

        public async Task<IActionResult> OnPostEditarAsync()
        {
            // Carrega o ator atual para edição
            Ator = await _context.Atores.FindAsync(Ator.Id);

            if (Ator == null)
            {
                return NotFound();
            }

            Editando = true;
            return Page();
        }

        public async Task<IActionResult> OnPostSalvarAsync()
        {
            if (!ModelState.IsValid)
            {
                Editando = true;
                return Page();
            }

            var atorExistente = await _context.Atores.FindAsync(Ator.Id);

            if (atorExistente == null)
            {
                return NotFound();
            }

            // Atualiza apenas os campos permitidos
            atorExistente.Nome = Ator.Nome;
            atorExistente.DataNascimento = Ator.DataNascimento;
            atorExistente.Foto = Ator.Foto;
            atorExistente.Biografia = Ator.Biografia;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AtorExists(Ator.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage(new { id = Ator.Id });
        }

        public async Task<IActionResult> OnPostDeletarAsync()
        {
            var ator = await _context.Atores.FindAsync(Ator.Id);

            if (ator == null)
            {
                return NotFound();
            }

            _context.Atores.Remove(ator);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool AtorExists(int id)
        {
            return _context.Atores.Any(e => e.Id == id);
        }
    }
}