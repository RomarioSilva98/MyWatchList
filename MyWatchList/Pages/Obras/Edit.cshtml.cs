using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWatchList.Data;
using MyWatchList.Models;
using System.Diagnostics;

namespace MyWatchList.Pages.Obras
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EditModel> _logger;

        public EditModel(AppDbContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Obra Obra { get; set; }

        [BindProperty]
        public string? GenerosTexto { get; set; }

        [BindProperty]
        public string? FotosTexto { get; set; }

        [BindProperty]
        public string? VideosTexto { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            _logger.LogInformation("Carregando obra ID {ObraId} para edição", id);

            try
            {
                Obra = await _context.Obras
                    .Include(o => o.Generos)
                    .Include(o => o.Fotos)
                    .Include(o => o.Videos)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (Obra == null)
                {
                    _logger.LogWarning("Obra ID {ObraId} não encontrada", id);
                    return NotFound();
                }

                GenerosTexto = Obra.Generos.Any() ? string.Join(", ", Obra.Generos.Select(g => g.Nome)) : "";
                FotosTexto = Obra.Fotos.Any() ? string.Join(Environment.NewLine, Obra.Fotos.Select(f => f.Url)) : "";
                VideosTexto = Obra.Videos.Any() ? string.Join(Environment.NewLine, Obra.Videos.Select(v => v.Url)) : "";

                _logger.LogDebug("Dados carregados: {GenerosCount} gêneros, {FotosCount} fotos, {VideosCount} vídeos",
                    Obra.Generos.Count, Obra.Fotos.Count, Obra.Videos.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar obra ID {ObraId}", id);
                throw;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("Iniciando salvamento da obra ID {ObraId}", Obra?.Id);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState inválido. Erros: {Errors}",
                    string.Join("; ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)));
                return Page();
            }

            try
            {
                var obraNoBanco = await _context.Obras
                    .Include(o => o.Generos)
                    .Include(o => o.Fotos)
                    .Include(o => o.Videos)
                    .FirstOrDefaultAsync(o => o.Id == Obra.Id);

                if (obraNoBanco == null)
                {
                    _logger.LogError("Obra ID {ObraId} não encontrada no banco de dados", Obra.Id);
                    return NotFound();
                }

                _logger.LogDebug("Dados recebidos do formulário:");
                _logger.LogDebug("Título: {Titulo}", Obra.Titulo);
                _logger.LogDebug("Gêneros: {GenerosTexto}", GenerosTexto);
                _logger.LogDebug("Fotos: {FotosTexto}", FotosTexto);
                _logger.LogDebug("Vídeos: {VideosTexto}", VideosTexto);

                // Atualiza campos básicos
                obraNoBanco.Titulo = Obra.Titulo;
                obraNoBanco.Sinopse = Obra.Sinopse;
                obraNoBanco.NotaMedia = Obra.NotaMedia;
                obraNoBanco.PopularidadeDia = Obra.PopularidadeDia;
                obraNoBanco.PopularidadeSemana = Obra.PopularidadeSemana;
                obraNoBanco.PopularidadeMes = Obra.PopularidadeMes;
                obraNoBanco.PopularidadeAno = Obra.PopularidadeAno;

                // Atualiza relações apenas se houver dados fornecidos
                if (!string.IsNullOrWhiteSpace(GenerosTexto))
                {
                    UpdateGeneros(obraNoBanco);
                }
                else
                {
                    _logger.LogInformation("Nenhum gênero fornecido, mantendo os existentes");
                }

                if (!string.IsNullOrWhiteSpace(FotosTexto))
                {
                    UpdateFotos(obraNoBanco);
                }
                else
                {
                    _logger.LogInformation("Nenhuma foto fornecida, mantendo as existentes");
                }

                if (!string.IsNullOrWhiteSpace(VideosTexto))
                {
                    UpdateVideos(obraNoBanco);
                }
                else
                {
                    _logger.LogInformation("Nenhum vídeo fornecido, mantendo os existentes");
                }

                _logger.LogDebug("Dados após atualização:");
                _logger.LogDebug("Gêneros: {GenerosCount}", obraNoBanco.Generos.Count);
                _logger.LogDebug("Fotos: {FotosCount}", obraNoBanco.Fotos.Count);
                _logger.LogDebug("Vídeos: {VideosCount}", obraNoBanco.Videos.Count);

                await _context.SaveChangesAsync();
                _logger.LogInformation("Obra ID {ObraId} salva com sucesso", Obra.Id);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Erro de banco de dados ao salvar obra ID {ObraId}", Obra?.Id);
                ModelState.AddModelError(string.Empty, "Erro ao salvar no banco de dados. Verifique os logs.");
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao salvar obra ID {ObraId}", Obra?.Id);
                ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado. Verifique os logs.");
                return Page();
            }

            return RedirectToPage("./Details", new { id = Obra.Id });
        }

        private void UpdateGeneros(Obra obra)
        {
            _logger.LogDebug("Atualizando gêneros. Texto original: {GenerosTexto}", GenerosTexto);

            obra.Generos.Clear();
            var generos = GenerosTexto
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(g => g.Trim())
                .Where(g => !string.IsNullOrWhiteSpace(g))
                .Select(g => new Genero { Nome = g, ObraId = obra.Id })
                .ToList();

            obra.Generos = generos;
            _logger.LogDebug("Adicionados {GenerosCount} gêneros", generos.Count);
        }

        private void UpdateFotos(Obra obra)
        {
            _logger.LogDebug("Atualizando fotos. Texto original: {FotosTexto}", FotosTexto);

            obra.Fotos.Clear();
            var fotos = FotosTexto
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(f => f.Trim())
                .Where(f => !string.IsNullOrWhiteSpace(f))
                .Select(f => new Foto { Url = f, ObraId = obra.Id })
                .ToList();

            obra.Fotos = fotos;
            _logger.LogDebug("Adicionadas {FotosCount} fotos", fotos.Count);
        }

        private void UpdateVideos(Obra obra)
        {
            _logger.LogDebug("Atualizando vídeos. Texto original: {VideosTexto}", VideosTexto);

            obra.Videos.Clear();
            var videos = VideosTexto
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(v => v.Trim())
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Select(v => new Video { Url = v, ObraId = obra.Id })
                .ToList();

            obra.Videos = videos;
            _logger.LogDebug("Adicionados {VideosCount} vídeos", videos.Count);
        }
    }
}