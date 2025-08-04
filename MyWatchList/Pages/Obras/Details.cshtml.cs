using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWatchList.Data;
using MyWatchList.Models;

namespace MyWatchList.Pages.Obras;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;

    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    public Obra Obra { get; set; }
    public int? UsuarioId { get; set; }

    [BindProperty] public string Texto { get; set; }
    [BindProperty] public int Nota { get; set; }
    [BindProperty] public string Status { get; set; }
    [BindProperty] public string Progresso { get; set; }
    [BindProperty] public int NotaAvaliacao { get; set; }

    [BindProperty] public int ComentarioId { get; set; }
    [BindProperty] public string NovoTexto { get; set; }
    [BindProperty] public string NovoStatus { get; set; }
    [BindProperty] public string NovoProgresso { get; set; }
    [BindProperty] public int? NovaNota { get; set; }



    public async Task<IActionResult> OnPostComentarAsync(int id)
    {
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
        if (usuarioId == null) return RedirectToPage("/Usuarios/Login");

        var comentario = new Comentario
        {
            ObraId = id,
            UsuarioId = usuarioId.Value,
            Texto = Texto,
            Nota = Nota,
            Status = Status,
            Progresso = Progresso,
            Data = DateTime.Now
        };

        _context.Comentarios.Add(comentario);
        await _context.SaveChangesAsync();

        await RecalcularNotaMedia(id);
        return RedirectToPage(new { id });
    }
  
    public async Task<IActionResult> OnPostEditarComentarioAsync(int id)
    {
        ViewData["EditandoComentarioId"] = ComentarioId;
        await OnGetAsync(id);
        return Page();
    }


    public async Task<IActionResult> OnPostSalvarEdicaoComentarioAsync(int id)
    {
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
        if (usuarioId == null) return RedirectToPage("/Usuarios/Login");

        var comentario = await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == ComentarioId && c.UsuarioId == usuarioId);
        if (comentario != null)
        {
            comentario.Texto = NovoTexto?.Trim() ?? comentario.Texto;
            comentario.Status = NovoStatus ?? comentario.Status;
            comentario.Progresso = NovoProgresso ?? comentario.Progresso;
            comentario.Nota = NovaNota;

            await _context.SaveChangesAsync();
            await RecalcularNotaMedia(id);
        }

        return RedirectToPage(new { id });
    }


    [BindProperty] public int comentarioId { get; set; }
    public async Task<IActionResult> OnPostDeletarComentarioAsync(int id)
    {
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
        if (usuarioId == null) return RedirectToPage("/Usuarios/Login");

        var comentario = await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == comentarioId && c.UsuarioId == usuarioId);
        if (comentario != null)
        {
            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();
            await RecalcularNotaMedia(id);
        }

        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostAvaliarAsync(int id)
    {
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
        if (usuarioId == null) return RedirectToPage("/Usuarios/Login");

        // Adiciona avaliação invisível (sem texto)
        var avaliacao = new Comentario
        {
            ObraId = id,
            UsuarioId = usuarioId.Value,
            Nota = NotaAvaliacao,
            Texto = "Avaliação sem comentário",
            Status = "Avaliado",
            Progresso = "",
            Data = DateTime.Now
        };

        _context.Comentarios.Add(avaliacao);
        await _context.SaveChangesAsync();

        await RecalcularNotaMedia(id);
        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostAdicionarWatchlistAsync(int id)
    {
        UsuarioId = HttpContext.Session.GetInt32("UsuarioId");
        if (UsuarioId == null) return RedirectToPage("/Usuarios/Login");

        var existe = await _context.UsuarioObrasWatchlist
            .AnyAsync(u => u.UsuarioId == UsuarioId && u.ObraId == id);

        if (!existe)
        {
            _context.UsuarioObrasWatchlist.Add(new UsuarioObraWatchlist
            {
                UsuarioId = UsuarioId.Value,
                ObraId = id
            });
            await _context.SaveChangesAsync();
        }

        return RedirectToPage(new { id });
    }

    private async Task RecalcularNotaMedia(int obraId)
    {
        var novaMedia = await _context.Comentarios
            .Where(c => c.ObraId == obraId && c.Nota > 0)
            .AverageAsync(c => (double?)c.Nota) ?? 0;

        var obra = await _context.Obras.FindAsync(obraId);
        if (obra != null)
        {
            obra.NotaMedia = (float)Math.Round(novaMedia, 1);
            await _context.SaveChangesAsync();
        }
    }

    public List<Temporada> Temporadas { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        UsuarioId = HttpContext.Session.GetInt32("UsuarioId");

        Obra = await _context.Obras
            .Include(o => o.Generos)
            .Include(o => o.Fotos)
            .Include(o => o.Videos)
            .Include(o => o.Elenco).ThenInclude(e => e.Ator)
            .Include(o => o.Comentarios).ThenInclude(c => c.Usuario)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (Obra == null)
            return NotFound();

        if (Obra.TipoObra != "Filme")
        {
            Temporadas = await _context.Temporadas
                .Where(t => t.ObraId == Obra.Id)
                .Include(t => t.Episodios)
                .ToListAsync();
        }

        return Page();
    }

}
