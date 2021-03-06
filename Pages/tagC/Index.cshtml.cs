using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniAnimeDB.Data;
using MiniAnimeDB.Models;

namespace MiniAnimeDB.Pages.tagC
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public IndexModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        //public IList<TagC> TagC { get;set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<TagC> TagCs { get; set; }

        public async Task OnGetAsync(string currentFilter, string searchingString, int? pageIndex)
        {
            ViewData["Tags"] = new List<TagC>(_context.TagC);
            if (searchingString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchingString = currentFilter;
            }
            CurrentFilter = searchingString;
            IQueryable<TagC> TagCIQ = from s in _context.TagC select s;
            if (!String.IsNullOrEmpty(searchingString))
            {
                TagCIQ = TagCIQ.Where(s => s.Tag.ToUpper().Contains(searchingString.ToUpper()));
            }
            int pageSize = 10;
            TagCs = await PaginatedList<TagC>.CreateAsync(TagCIQ.AsNoTracking(), pageIndex ?? 1, pageSize); 
        }
    }
}
