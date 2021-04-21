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

namespace MiniAnimeDB.Pages.anime_character
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
        public AnimeCharacter AnimeCharacter { get; set; }
        public string CurrentFilterCha { get; set; }
        public string ChaPub { get; set; }
        public string CurrentFilterAni { get; set; }
        public string AniPub { get; set; }

        public bool CheckRepeat()
        {
            foreach (var ac in _context.AnimeCharacter.AsNoTracking())
            {
                if (ac.AnimeID == AnimeCharacter.AnimeID && ac.CharacterID == AnimeCharacter.CharacterID) return false;
            }
            return true;
        }
        public bool CheckAniVaild()
        {
            foreach (var an in _context.Anime.AsNoTracking())
            {
                if (an.ID == AnimeCharacter.AnimeID) return true;
            }
            return false;
        }
        public bool CheckChaValid()
        {
            foreach (var ch in _context.Character.AsNoTracking())
            {
                if (ch.CharacterID == AnimeCharacter.CharacterID) return true;
            }
            return false;
        }
        public int bid = 0;
        public async Task<IActionResult> OnGetAsync(int? id, string SearchingStringCha, string SearchingStringAni)
        {
            if (id == null)
            {
                return NotFound();
            }
            bid = Convert.ToInt32(id);
            AnimeCharacter = await _context.AnimeCharacter
                .Include(a => a.Anime)
                .Include(a => a.Character).FirstOrDefaultAsync(m => m.AnimeCharacterID == id);

            if (AnimeCharacter == null)
            {
                return NotFound();
            }
            //ViewData["AnimeID"] = new SelectList(_context.Anime, "ID", "Title");
            //ViewData["CharacterID"] = new SelectList(_context.Character, "CharacterID", "Name");
            ViewData["Anis"] = new List<Anime>(_context.Anime);
            ViewData["Chas"] = new List<Character>(_context.Character);
            ChaPub = "???";
            if (!String.IsNullOrEmpty(SearchingStringCha))
            {
                CurrentFilterCha = SearchingStringCha;
                foreach (var ch in _context.Character)
                {
                    if (ch.Name.ToUpper().Contains(SearchingStringCha.ToUpper()))
                    {
                        CurrentFilterCha = ch.Name;
                        ChaPub = ch.CharacterID.ToString();
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !CheckAniVaild() || !CheckRepeat() || !CheckChaValid())
            {
                return Page();
            }

            _context.Attach(AnimeCharacter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimeCharacterExists(AnimeCharacter.AnimeCharacterID))
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

        private bool AnimeCharacterExists(int id)
        {
            return _context.AnimeCharacter.Any(e => e.AnimeCharacterID == id);
        }
    }
}
