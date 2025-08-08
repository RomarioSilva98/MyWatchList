using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWatchList.Data;
using MyWatchList.Models;

namespace MyWatchList.Pages.Usuarios
{
    public class EditModel : PageModel
    {
        private readonly MyWatchList.Data.AppDbContext _context;

        public EditModel(MyWatchList.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Usuario Usuario { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario =  await _context.Usuario.FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            Usuario = usuario;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var usuarioBanco = await _context.Usuario.FirstOrDefaultAsync(u => u.Id == Usuario.Id);
            if (usuarioBanco == null)
            {
                return NotFound();
            }

            // Atualiza manualmente os campos desejados
            usuarioBanco.Nome = Usuario.Nome;
            usuarioBanco.Email = Usuario.Email;
            usuarioBanco.Senha = Usuario.Senha;
            usuarioBanco.FotoPerfil = Usuario.FotoPerfil;
            usuarioBanco.Biografia = Usuario.Biografia;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Usuarios/Edit", new { id = Usuario.Id });
        }

        public async Task<IActionResult> OnPostExcluirAsync(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
                return NotFound();

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            // Limpa a sessão se o usuário estava logado
            HttpContext.Session.Clear();

            return RedirectToPage("/Index");
        }


        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
