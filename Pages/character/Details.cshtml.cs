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

namespace MiniAnimeDB.Pages.character
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public DetailsModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        public Character Character { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Character = await _context.Character.Include(s=>s.Roles).ThenInclude(an=>an.Anime).Include(s=>s.Casts).ThenInclude(ps=>ps.Person).Include(tc=>tc.WithTags).ThenInclude(tt=>tt.TagC).AsNoTracking().FirstOrDefaultAsync(m => m.CharacterID == id);

            if (Character == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
