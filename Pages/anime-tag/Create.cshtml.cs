using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniAnimeDB.Data;
using MiniAnimeDB.Models;

namespace MiniAnimeDB.Pages.anime_tag
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public CreateModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string SearchingStringTag, string SearchingStringAni)
        {
            //ViewData["AnimeID"] = new SelectList(_context.Anime, "ID", "Title");
            //ViewData["TagAID"] = new SelectList(_context.TagA, "TagAID", "Tag");
            ViewData["Anis"] = new List<Anime>(_context.Anime);
            ViewData["TagAs"] = new List<TagA>(_context.TagA);
            TagPub = "???";
            if (!String.IsNullOrEmpty(SearchingStringTag))
            {
                CurrentFilterTag = SearchingStringTag;
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
            AniPub = "???";
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !CheckAniVaild() || !CheckRepeat() || !CheckTagValid())
            {
                return Page();
            }
            _context.AnimeTagA.Add(AnimeTagA);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
