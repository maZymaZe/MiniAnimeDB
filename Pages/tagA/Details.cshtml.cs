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
    public class DetailsModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public DetailsModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        public TagA TagA { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TagA = await _context.TagA.Include(s=>s.WithTags).ThenInclude(an=>an.Anime).AsNoTracking().FirstOrDefaultAsync(m => m.TagAID == id);

            if (TagA == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
