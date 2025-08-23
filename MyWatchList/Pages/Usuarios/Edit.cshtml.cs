using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWatchList.Data;
using MyWatchList.Helpers;
using MyWatchList.Models;

namespace MyWatchList.Pages.Usuarios
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty] public Usuario Usuario { get; set; } = default!;
        [BindProperty] public string SenhaAntiga { get; set; }
        [BindProperty] public string NovaSenha { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null) return NotFound();

            Usuario = usuario;
            return Page();
        }

        // 👉 Form de dados básicos
        public async Task<IActionResult> OnPostSalvarPerfilAsync()
        {
            Console.WriteLine("=== INICIANDO OnPostSalvarPerfilAsync ===");

            // IGNORA a validação do campo Senha para este método
            ModelState.Remove("Usuario.Senha");
            ModelState.Remove("SenhaAntiga");
            ModelState.Remove("NovaSenha");

            Console.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Erros de validação:");
                foreach (var error in ModelState)
                {
                    if (error.Value.Errors.Count > 0)
                    {
                        Console.WriteLine($"{error.Key}: {error.Value.Errors[0].ErrorMessage}");
                    }
                }
                return Page();
            }

           

            var usuarioBanco = await _context.Usuario.FirstOrDefaultAsync(u => u.Id == Usuario.Id);
            if (usuarioBanco == null)
            {
                
                return NotFound();
            }

           

            if (_context.Usuario.Any(u => u.Email == Usuario.Email && u.Id != Usuario.Id))
            {
             
                ModelState.AddModelError("Usuario.Email", "Já existe uma conta com este e-mail.");
                return Page();
            }

            usuarioBanco.Nome = Usuario.Nome;
            usuarioBanco.Email = Usuario.Email;
            usuarioBanco.FotoPerfil = Usuario.FotoPerfil;
            usuarioBanco.Biografia = Usuario.Biografia;

           

            try
            {
                await _context.SaveChangesAsync();
                Console.WriteLine("✅ Alterações salvas com sucesso no banco!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao salvar: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }

            return RedirectToPage(new { id = Usuario.Id });
        }

        // 👉 Form só para alterar senha
        public async Task<IActionResult> OnPostAlterarSenhaAsync()
        {
            var usuarioBanco = await _context.Usuario.FirstOrDefaultAsync(u => u.Id == Usuario.Id);
            if (usuarioBanco == null) return NotFound();

            if (string.IsNullOrEmpty(SenhaAntiga) || string.IsNullOrEmpty(NovaSenha))
            {
                ModelState.AddModelError(string.Empty, "Preencha a senha atual e a nova senha.");
                return Page();
            }

            if (!PasswordHelper.VerifyPassword(usuarioBanco, SenhaAntiga, usuarioBanco.Senha))
            {
                ModelState.AddModelError("SenhaAntiga", "A senha atual está incorreta.");
                return Page();
            }

            usuarioBanco.Senha = PasswordHelper.HashPassword(usuarioBanco, NovaSenha);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = Usuario.Id });
        }

        public async Task<IActionResult> OnPostExcluirAsync(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null) return NotFound();

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            HttpContext.Session.Clear();

            return RedirectToPage("/Index");
        }
    }

}

