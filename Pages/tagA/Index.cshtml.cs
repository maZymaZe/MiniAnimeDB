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

namespace MiniAnimeDB.Pages.tagA
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public IndexModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        //public IList<TagA> TagA { get;set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<TagA> TagAs { get; set; }

        public async Task OnGetAsync(string currentFilter, string searchingString, int? pageIndex)
        {
            if (searchingString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchingString = currentFilter;
            }
            CurrentFilter = searchingString;
            IQueryable<TagA> TagAIQ = from s in _context.TagA select s;
            if (!String.IsNullOrEmpty(searchingString))
            {
                TagAIQ = TagAIQ.Where(s => s.Tag.ToUpper().Contains(searchingString.ToUpper()));
            }
            int pageSize = 10;
            TagAs = await PaginatedList<TagA>.CreateAsync(TagAIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
