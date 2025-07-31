using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using MyWatchList.Data;
using MyWatchList.Models;

namespace MyWatchList.Pages.Obras
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Obra Obra { get; set; } = default!;

        [BindProperty]
        public string? GenerosTexto { get; set; }

        [BindProperty]
        public string? FotosTexto { get; set; }

        [BindProperty]
        public string? VideosTexto { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var generos = (GenerosTexto ?? "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(nome => new Genero { Nome = nome.Trim(), Obra = Obra })
                .ToList();

            var fotos = (FotosTexto ?? "")
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(url => new Foto { Url = url.Trim(), Obra = Obra })
                .ToList();

            var videos = (VideosTexto ?? "")
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(url => new Video { Url = url.Trim(), Obra = Obra })
                .ToList();

            Obra.Generos = generos;
            Obra.Fotos = fotos;
            Obra.Videos = videos;

            _context.Obras.Add(Obra);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = Obra.Id });
        }
    }
}
