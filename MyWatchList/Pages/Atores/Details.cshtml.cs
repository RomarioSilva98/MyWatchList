using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using MyWatchList.Data;
using MyWatchList.Models;
using Microsoft.EntityFrameworkCore;


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

        public List<Obra> TodasObras { get; set; } = new();
        public List<Obra> ObrasDoAtor { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id, bool? editar)
        {
            Ator = await _context.Atores
                .Include(a => a.Obras)
                    .ThenInclude(oa => oa.Obra)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (Ator == null) return NotFound();

            // 🔹 Preenche as obras já associadas ao ator
            ObrasDoAtor = Ator.Obras
                .Select(oa => oa.Obra)
                .ToList();

            // 🔹 Busca as obras que ainda não estão associadas
            var obrasIdsDoAtor = ObrasDoAtor.Select(o => o.Id).ToList();

            TodasObras = await _context.Obras
                .Where(o => !obrasIdsDoAtor.Contains(o.Id))
                .ToListAsync();

            Editando = editar ?? false;
            return Page();
        }


        public async Task<IActionResult> OnPostEditarAsync()
        {
            Ator = await _context.Atores.FindAsync(Ator.Id);
            if (Ator == null) return NotFound();
            Editando = true;
            return await OnGetAsync(Ator.Id, true);
        }

        public async Task<IActionResult> OnPostSalvarAsync()
        {
            if (!ModelState.IsValid)
            {
                Editando = true;
                return await OnGetAsync(Ator.Id, true);
            }

            var atorExistente = await _context.Atores.FindAsync(Ator.Id);
            if (atorExistente == null) return NotFound();

            atorExistente.Nome = Ator.Nome;
            atorExistente.DataNascimento = Ator.DataNascimento;
            atorExistente.Foto = Ator.Foto;
            atorExistente.Biografia = Ator.Biografia;

            await _context.SaveChangesAsync();
            return RedirectToPage(new { id = Ator.Id });
        }

        public async Task<IActionResult> OnPostDeletarAsync()
        {
            var ator = await _context.Atores.FindAsync(Ator.Id);
            if (ator == null) return NotFound();

            _context.Atores.Remove(ator);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAdicionarObraAsync(int atorId, int obraId)
        {
            if (!_context.Obras.Any(o => o.Id == obraId) || !_context.Atores.Any(a => a.Id == atorId))
                return NotFound();

            var jaExiste = await _context.ObraAtores.AnyAsync(oa => oa.AtorId == atorId && oa.ObraId == obraId);
            if (!jaExiste)
            {
                _context.ObraAtores.Add(new ObraAtor
                {
                    AtorId = atorId,
                    ObraId = obraId
                });
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(new { id = atorId });
        }
    }
}