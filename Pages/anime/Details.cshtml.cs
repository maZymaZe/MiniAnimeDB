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

namespace MiniAnimeDB.Pages.anime
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public DetailsModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        public Anime Anime { get; set; }
        public Anime Anime2 { get; set; }
        public Anime Anime3 { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Anime = await _context.Anime.Include(s=>s.Staffs).ThenInclude(e=>e.Person).AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);
            Anime2 = await _context.Anime.Include(r => r.Roles).ThenInclude(c => c.Character).ThenInclude(cs => cs.Casts).ThenInclude(vo => vo.Person).AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);
            Anime3 = await _context.Anime.Include(ts=>ts.WithTags).ThenInclude(ta=>ta.TagA).AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);
            if (Anime == null)
            {
                return NotFound();
            }
            if (Anime2 == null)
            {
                return NotFound();
            }
            if (Anime3 == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
