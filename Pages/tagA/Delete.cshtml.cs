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
    [Authorize(Roles = "admin")]
    public class DeleteModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public DeleteModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TagA TagA { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TagA = await _context.TagA.FirstOrDefaultAsync(m => m.TagAID == id);

            if (TagA == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TagA = await _context.TagA.FindAsync(id);

            if (TagA != null)
            {
                _context.TagA.Remove(TagA);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
