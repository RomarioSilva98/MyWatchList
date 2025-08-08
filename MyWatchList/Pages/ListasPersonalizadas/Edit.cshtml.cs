using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWatchList.Data;

namespace MyWatchList.Pages.ListasPersonalizadas
{
    public class EditModel : PageModel
    {
        private readonly MyWatchList.Data.AppDbContext _context;

        public EditModel(MyWatchList.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ListaPersonalizada ListaPersonalizada { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listapersonalizada =  await _context.ListaPersonalizada.FirstOrDefaultAsync(m => m.Id == id);
            if (listapersonalizada == null)
            {
                return NotFound();
            }
            ListaPersonalizada = listapersonalizada;
           ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Email");
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

            _context.Attach(ListaPersonalizada).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListaPersonalizadaExists(ListaPersonalizada.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ListaPersonalizadaExists(int id)
        {
            return _context.ListaPersonalizada.Any(e => e.Id == id);
        }
    }
}
