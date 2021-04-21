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

namespace MiniAnimeDB.Pages.person_character
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public CreateModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string SearchingStringCha, string SearchingStringPer)
        {
            //ViewData["CharacterID"] = new SelectList(_context.Character, "CharacterID", "Name");
            //ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "Name");
            ViewData["Pers"] = new List<Person>(_context.Person);
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
            PerPub = "???";
            if (!String.IsNullOrEmpty(SearchingStringPer))
            {
                CurrentFilterPer = SearchingStringPer;
                foreach (var ps in _context.Person)
                {
                    if (ps.Name.ToUpper().Contains(SearchingStringPer.ToUpper()))
                    {
                        CurrentFilterPer = ps.Name;
                        PerPub = ps.PersonID.ToString();
                        break;
                    }
                }

            }
            return Page();
        }

        [BindProperty]
        public PersonCharacter PersonCharacter { get; set; }
        public string CurrentFilterCha { get; set; }
        public string ChaPub { get; set; }
        public string CurrentFilterPer { get; set; }
        public string PerPub { get; set; }

        public bool CheckRepeat()
        {
            foreach (var pc in _context.PersonCharacter)
            {
                if (pc.CharacterID == PersonCharacter.CharacterID && pc.PersonID == PersonCharacter.PersonID) return false;
            }
            return true;
        }
        public bool CheckPerVaild()
        {
            foreach (var ps in _context.Person)
            {
                if (ps.PersonID == PersonCharacter.PersonID) return true;
            }
            return false;
        }
        public bool CheckChaValid()
        {
            foreach (var ch in _context.Character)
            {
                if (ch.CharacterID == PersonCharacter.CharacterID) return true;
            }
            return false;
        }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !CheckPerVaild() || !CheckRepeat() || !CheckChaValid())
            {
                return Page();
            }

            _context.PersonCharacter.Add(PersonCharacter);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
