using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniAnimeDB.Data;
using MiniAnimeDB.Models;

namespace MiniAnimeDB.Pages.anime_tag
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
        ViewData["TagAID"] = new SelectList(_context.TagA, "TagAID", "Tag");
            return Page();
        }

        [BindProperty]
        public AnimeTagA AnimeTagA { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AnimeTagA.Add(AnimeTagA);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
