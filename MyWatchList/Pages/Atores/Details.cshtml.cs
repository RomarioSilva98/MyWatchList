using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWatchList.Data;
using MyWatchList.Models;

namespace MyWatchList.Pages.Atores
{
    public class DetailsModel : PageModel
    {
        private readonly MyWatchList.Data.AppDbContext _context;

        public DetailsModel(MyWatchList.Data.AppDbContext context)
        {
            _context = context;
        }

        public Ator Ator { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ator = await _context.Atores.FirstOrDefaultAsync(m => m.Id == id);
            if (ator == null)
            {
                return NotFound();
            }
            else
            {
                Ator = ator;
            }
            return Page();
        }
    }
}
