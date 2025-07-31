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

namespace MyWatchList.Pages.Atores
{
    public class EditModel : PageModel
    {
        private readonly MyWatchList.Data.AppDbContext _context;

        public EditModel(MyWatchList.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Ator Ator { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ator =  await _context.Atores.FirstOrDefaultAsync(m => m.Id == id);
            if (ator == null)
            {
                return NotFound();
            }
            Ator = ator;
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

            _context.Attach(Ator).State = EntityState.Modified;

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

            return RedirectToPage("./Index");
        }

        private bool AtorExists(int id)
        {
            return _context.Atores.Any(e => e.Id == id);
        }
    }
}
