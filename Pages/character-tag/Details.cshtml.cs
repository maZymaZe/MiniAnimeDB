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

namespace MiniAnimeDB.Pages.character_tag
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public DetailsModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        public CharacterTagC CharacterTagC { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CharacterTagC = await _context.CharacterTagC
                .Include(c => c.Character)
                .Include(c => c.TagC).FirstOrDefaultAsync(m => m.CharacterTagCID == id);

            if (CharacterTagC == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
