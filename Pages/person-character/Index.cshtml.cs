using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniAnimeDB.Data;
using MiniAnimeDB.Models;

namespace MiniAnimeDB.Pages.person_character
{
    public class IndexModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public IndexModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        public IList<PersonCharacter> PersonCharacter { get;set; }

        public async Task OnGetAsync()
        {
            PersonCharacter = await _context.PersonCharacter
                .Include(p => p.Character)
                .Include(p => p.Person).ToListAsync();
        }
    }
}
