﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniAnimeDB.Data;
using MiniAnimeDB.Models;

namespace MiniAnimeDB.Pages.anime_tag
{
    public class DeleteModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public DeleteModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AnimeTagA AnimeTagA { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnimeTagA = await _context.AnimeTagA
                .Include(a => a.Anime)
                .Include(a => a.TagA).FirstOrDefaultAsync(m => m.AnimeTagAID == id);

            if (AnimeTagA == null)
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

            AnimeTagA = await _context.AnimeTagA.FindAsync(id);

            if (AnimeTagA != null)
            {
                _context.AnimeTagA.Remove(AnimeTagA);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}