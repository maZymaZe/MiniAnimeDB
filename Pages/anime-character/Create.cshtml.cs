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

namespace MiniAnimeDB.Pages.anime_character
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;
       

        public bool CheckRepeat()
        {
            foreach (var ac in _context.AnimeCharacter)
            {
                if (ac.AnimeID == AnimeCharacter.AnimeID && ac.CharacterID == AnimeCharacter.CharacterID) return false;
            }
            return true;
        }
        public bool CheckAniVaild()
        {
            foreach (var an in _context.Anime)
            {
                if (an.ID == AnimeCharacter.AnimeID) return true;
            }
            return false;
        }
        public bool CheckChaValid()
        {
            foreach (var ch in _context.Character)
            {
                if (ch.CharacterID == AnimeCharacter.CharacterID) return true;
            }
            return false;
        }
        public CreateModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string SearchingStringCha, string SearchingStringAni)
        {
            //ViewData["AnimeID"] = new SelectList(_context.Anime, "ID", "Title");
            //ViewData["CharacterID"] = new SelectList(_context.Character, "CharacterID", "Name");
            ViewData["Anis"] = new List<Anime>(_context.Anime);
            ViewData["Chas"] = new List<Character>(_context.Character);
            ChaPub = "???";
            if (!String.IsNullOrEmpty(SearchingStringCha))
            {
                CurrentFilterCha = SearchingStringCha;
                int flag = 0;
                foreach (var ch in _context.Character)
                {
                    if (ch.Name.ToUpper().Equals(SearchingStringCha.ToUpper()))
                    {
                        CurrentFilterCha = ch.Name;
                        ChaPub = ch.CharacterID.ToString();
                        flag = 1;
                        break;
                    }
                }

                if(flag==0)foreach (var ch in _context.Character)
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
                int flag = 0;
                foreach (var an in _context.Anime)
                {
                    if (an.Title.ToUpper().Equals(SearchingStringAni.ToUpper()))
                    {
                        CurrentFilterAni = an.Title;
                        AniPub = an.ID.ToString();
                        flag = 1;
                        break;
                    }
                }
                if(flag==0)foreach (var an in _context.Anime)
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
        public AnimeCharacter AnimeCharacter { get; set; }
        public string CurrentFilterCha { get; set; }
        public string ChaPub { get; set; }
        public string CurrentFilterAni { get; set; }
        public string AniPub { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !CheckAniVaild() || !CheckRepeat() || !CheckChaValid())
            {
                return Page();
            }

            _context.AnimeCharacter.Add(AnimeCharacter);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
