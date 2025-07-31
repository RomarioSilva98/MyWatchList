using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWatchList.Data;
using MyWatchList.Models;

namespace MyWatchList.Pages.Atores
{
    public class CreateModel : PageModel
    {
        private readonly MyWatchList.Data.AppDbContext _context;

        public CreateModel(MyWatchList.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Ator Ator { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Atores.Add(Ator);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
