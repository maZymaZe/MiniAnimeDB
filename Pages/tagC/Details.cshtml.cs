using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniAnimeDB.Data;
using MiniAnimeDB.Models;

namespace MiniAnimeDB.Pages.tagC
{
    public class DetailsModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public DetailsModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        public TagC TagC { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TagC = await _context.TagC.Include(s=>s.WithTags).ThenInclude(ct=>ct.Character).ThenInclude(rs=>rs.Roles).ThenInclude(am=>am.Anime).AsNoTracking().FirstOrDefaultAsync(m => m.TagCID == id);

            if (TagC == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
