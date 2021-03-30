using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniAnimeDB.Data;
using MiniAnimeDB.Models;

namespace MiniAnimeDB.Pages.character_tag
{
    public class EditModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public EditModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["CharacterID"] = new SelectList(_context.Character, "CharacterID", "CharacterID");
           ViewData["TagCID"] = new SelectList(_context.TagC, "TagCID", "TagCID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CharacterTagC).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterTagCExists(CharacterTagC.CharacterTagCID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CharacterTagCExists(int id)
        {
            return _context.CharacterTagC.Any(e => e.CharacterTagCID == id);
        }
    }
}
