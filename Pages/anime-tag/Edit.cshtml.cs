using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniAnimeDB.Data;
using MiniAnimeDB.Models;

namespace MiniAnimeDB.Pages.anime_tag
{
    [Authorize(Roles = "admin")]
    public class EditModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public EditModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AnimeTagA AnimeTagA { get; set; }
        public string CurrentFilterTag { get; set; }
        public string TagPub { get; set; }
        public string CurrentFilterAni { get; set; }
        public string AniPub { get; set; }

        public bool CheckRepeat()
        {
            foreach (var ata in _context.AnimeTagA)
            {
                if (ata.AnimeID == AnimeTagA.AnimeID && ata.TagAID == AnimeTagA.TagAID) return false;
            }
            return true;
        }
        public bool CheckAniVaild()
        {
            foreach (var an in _context.Anime)
            {
                if (an.ID == AnimeTagA.AnimeID) return true;
            }
            return false;
        }
        public bool CheckTagValid()
        {
            foreach (var tg in _context.TagA)
            {
                if (tg.TagAID == AnimeTagA.TagAID) return true;
            }
            return false;
        }
        public int bid=0; 
        public async Task<IActionResult> OnGetAsync(int? id, string SearchingStringTag, string SearchingStringAni)
        {
            if (id == null)
            {
                return NotFound();
            }
            bid = Convert.ToInt32(id);
            AnimeTagA = await _context.AnimeTagA
                .Include(a => a.Anime)
                .Include(a => a.TagA).FirstOrDefaultAsync(m => m.AnimeTagAID == id);

            TagPub = "???";
            AniPub = "???";

            if (AnimeTagA == null)
            {
                return NotFound();
            }
            //ViewData["AnimeID"] = new SelectList(_context.Anime, "ID", "Title");
            //ViewData["TagAID"] = new SelectList(_context.TagA, "TagAID", "Tag");
            ViewData["Anis"] = new List<Anime>(_context.Anime);
            ViewData["TagAs"] = new List<TagA>(_context.TagA);
            if (!String.IsNullOrEmpty(SearchingStringTag))
            {
                CurrentFilterTag = SearchingStringTag;
                int flag = 0;
                foreach (var ta in _context.TagA)
                {
                    if (ta.Tag.ToUpper().Equals(SearchingStringTag.ToUpper()))
                    {
                        CurrentFilterTag = ta.Tag;
                        TagPub = ta.TagAID.ToString();
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                    foreach (var ta in _context.TagA)
                    {
                        if (ta.Tag.ToUpper().Contains(SearchingStringTag.ToUpper()))
                        {
                            CurrentFilterTag = ta.Tag;
                            TagPub = ta.TagAID.ToString();
                            break;
                        }
                    }
            }

            if (!String.IsNullOrEmpty(SearchingStringAni))
            {
                CurrentFilterAni = SearchingStringAni;
                foreach (var an in _context.Anime)
                {
                    if (an.Title.ToUpper().Contains(SearchingStringAni.ToUpper()))
                    {
                        CurrentFilterAni = an.Title;
                        AniPub = an.ID.ToString();
                        break;
                    }
                }
            }
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
            _context.Attach(AnimeTagA).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimeTagAExists(AnimeTagA.AnimeTagAID))
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

        private bool AnimeTagAExists(int id)
        {
            return _context.AnimeTagA.Any(e => e.AnimeTagAID == id);
        }
    }
}
