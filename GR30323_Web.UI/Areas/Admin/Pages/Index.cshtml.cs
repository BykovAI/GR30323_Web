using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GR30323_Web.Domain.Entities;
using GR30323_Web.UI.Data;

namespace GR30323_Web.UI.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GR30323_Web.UI.Data.AppDbContext _context;

        public IndexModel(GR30323_Web.UI.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Car> Car { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Car = await _context.Cars
                .Include(c => c.Category).ToListAsync();
        }
    }
}
