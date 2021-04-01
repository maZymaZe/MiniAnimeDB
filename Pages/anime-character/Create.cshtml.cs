using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniAnimeDB.Data;
using MiniAnimeDB.Models;

namespace MiniAnimeDB.Pages.anime_character
{
    public class CreateModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public CreateModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AnimeID"] = new SelectList(_context.Anime, "ID", "Title");
        ViewData["CharacterID"] = new SelectList(_context.Character, "CharacterID", "Name");
            return Page();
        }

        [BindProperty]
        public AnimeCharacter AnimeCharacter { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AnimeCharacter.Add(AnimeCharacter);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
