using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniAnimeDB.Data;
using MiniAnimeDB.Models;

namespace MiniAnimeDB.Pages.person
{
    public class DetailsModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public DetailsModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        public Person Person { get; set; }
        public Person Person2 { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person = await _context.Person.Include(s=>s.Staffs).ThenInclude(an=>an.Anime).FirstOrDefaultAsync(m => m.PersonID == id);
            Person2 = await _context.Person.Include(s => s.Casts).ThenInclude(ch=>ch.Character).ThenInclude(rl=>rl.Roles).ThenInclude(an=>an.Anime).FirstOrDefaultAsync(m => m.PersonID == id);
            if (Person == null)
            {
                return NotFound();
            }
            if (Person2 == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
