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

namespace MiniAnimeDB.Pages.person
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public IndexModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        //public IList<Person> Person { get;set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Person> Persons { get; set; }

        public async Task OnGetAsync(string currentFilter, string searchingString, int? pageIndex)
        {
            ViewData["Persons"] = new List<Person>(_context.Person);
            if (searchingString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchingString = currentFilter;
            }
            CurrentFilter = searchingString;
            IQueryable<Person> PersonIQ = from s in _context.Person select s;
            if (!String.IsNullOrEmpty(searchingString))
            {
                PersonIQ = PersonIQ.Where(s => s.Name.ToUpper().Contains(searchingString.ToUpper()));
            }
            int pageSize = 10;
            Persons = await PaginatedList<Person>.CreateAsync(PersonIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
