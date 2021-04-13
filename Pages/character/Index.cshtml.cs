using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniAnimeDB.Data;
using MiniAnimeDB.Models;

namespace MiniAnimeDB.Pages.character
{
    public class IndexModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public IndexModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        //public IList<Character> Character { get;set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Character> Characters { get; set; }

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
            IQueryable<Character> CharacterIQ = from s in _context.Character select s;
            if (!String.IsNullOrEmpty(searchingString))
            {
                CharacterIQ = CharacterIQ.Where(s => s.Name.ToUpper().Contains(searchingString.ToUpper()));
            }
            int pageSize = 10;
            Characters = await PaginatedList<Character>.CreateAsync(CharacterIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
