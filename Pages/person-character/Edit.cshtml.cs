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

namespace MiniAnimeDB.Pages.person_character
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
        public PersonCharacter PersonCharacter { get; set; }
        public string CurrentFilterCha { get; set; }
        public string ChaPub { get; set; }
        public string CurrentFilterPer { get; set; }
        public string PerPub { get; set; }

        public bool CheckRepeat()
        {
            foreach (var pc in _context.PersonCharacter.AsNoTracking())
            {
                if (pc.CharacterID == PersonCharacter.CharacterID && pc.PersonID == PersonCharacter.PersonID) return false;
            }
            return true;
        }
        public bool CheckPerVaild()
        {
            foreach (var ps in _context.Person.AsNoTracking())
            {
                if (ps.PersonID == PersonCharacter.PersonID) return true;
            }
            return false;
        }
        public bool CheckChaValid()
        {
            foreach (var ch in _context.Character.AsNoTracking())
            {
                if (ch.CharacterID == PersonCharacter.CharacterID) return true;
            }
            return false;
        }
        public int bid = 0;
        public async Task<IActionResult> OnGetAsync(int? id, string SearchingStringCha, string SearchingStringPer)
        {
            if (id == null)
            {
                return NotFound();
            }
            bid = Convert.ToInt32(id);
            PersonCharacter = await _context.PersonCharacter
                .Include(p => p.Character)
                .Include(p => p.Person).FirstOrDefaultAsync(m => m.PersonCharacterID == id);

            if (PersonCharacter == null)
            {
                return NotFound();
            }
            //ViewData["CharacterID"] = new SelectList(_context.Character, "CharacterID", "Name");
            //ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "Name");
            ViewData["Pers"] = new List<Person>(_context.Person);
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
                        flag = 0;
                        break;
                    }
                }
                if (flag == 0)
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
                int flag = 0;
                foreach (var ps in _context.Person)
                {
                    if (ps.Name.ToUpper().Equals(SearchingStringPer.ToUpper()))
                    {
                        CurrentFilterPer = ps.Name;
                        PerPub = ps.PersonID.ToString();
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !CheckPerVaild() || !CheckRepeat() || !CheckChaValid())
            {
                return Page();
            }

            _context.Attach(PersonCharacter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonCharacterExists(PersonCharacter.PersonCharacterID))
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

        private bool PersonCharacterExists(int id)
        {
            return _context.PersonCharacter.Any(e => e.PersonCharacterID == id);
        }
    }
}
