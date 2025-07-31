using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWatchList.Data;
using MyWatchList.Models;

namespace MyWatchList.Pages.Obras
{
    public class IndexModel : PageModel
    {
        private readonly MyWatchList.Data.AppDbContext _context;

        public IndexModel(MyWatchList.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Obra> Obra { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Obra = await _context.Obras.ToListAsync();
        }
    }
}
