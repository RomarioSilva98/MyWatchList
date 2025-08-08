using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWatchList.Data;

namespace MyWatchList.Pages.ListasPersonalizadas
{
    public class DetailsModel : PageModel
    {
        private readonly MyWatchList.Data.AppDbContext _context;

        public DetailsModel(MyWatchList.Data.AppDbContext context)
        {
            _context = context;
        }

        public ListaPersonalizada ListaPersonalizada { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listapersonalizada = await _context.ListaPersonalizada.FirstOrDefaultAsync(m => m.Id == id);
            if (listapersonalizada == null)
            {
                return NotFound();
            }
            else
            {
                ListaPersonalizada = listapersonalizada;
            }
            return Page();
        }
    }
}
