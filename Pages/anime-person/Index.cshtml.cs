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

namespace MiniAnimeDB.Pages.anime_person
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public IndexModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        public IList<AnimePerson> AnimePerson { get;set; }

        public async Task OnGetAsync()
        {
            AnimePerson = await _context.AnimePerson
                .Include(a => a.Anime)
                .Include(a => a.Person).ToListAsync();
        }
    }
}
