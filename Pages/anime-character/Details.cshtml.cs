using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniAnimeDB.Data;
using MiniAnimeDB.Models;

namespace MiniAnimeDB.Pages.anime_character
{
    public class DetailsModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public DetailsModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        public AnimeCharacter AnimeCharacter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnimeCharacter = await _context.AnimeCharacter
                .Include(a => a.Anime)
                .Include(a => a.Character).FirstOrDefaultAsync(m => m.AnimeCharacterID == id);

            if (AnimeCharacter == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
