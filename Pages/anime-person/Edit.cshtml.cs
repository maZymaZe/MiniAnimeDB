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

namespace MiniAnimeDB.Pages.anime_person
{
    public class EditModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public EditModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AnimePerson AnimePerson { get; set; }
        public string CurrentFilterPerson { get; set; }
        public string PersonPub { get; set; }
        public string CurrentFilterAni { get; set; }
        public string AniPub { get; set; }

        public bool CheckRepeat()
        {
            foreach (var ap in _context.AnimePerson.AsNoTracking())
            {
                if (ap.AnimeID == AnimePerson.AnimeID && ap.PersonID == AnimePerson.PersonID && ap.Position == AnimePerson.Position) return false;
            }
            return true;
        }
        public bool CheckAniVaild()
        {
            foreach (var an in _context.Anime.AsNoTracking())
            {
                if (an.ID == AnimePerson.AnimeID) return true;
            }
            return false;
        }
        public bool CheckPersonValid()
        {
            foreach (var ps in _context.Person.AsNoTracking())
            {
                if (ps.PersonID == AnimePerson.PersonID) return true;
            }
            return false;
        }
        public int bid = 0;

        public async Task<IActionResult> OnGetAsync(int? id, string SearchingStringPerson, string SearchingStringAni)
        {
            if (id == null)
            {
                return NotFound();
            }
            bid = Convert.ToInt32(id);
            AnimePerson = await _context.AnimePerson
                .Include(a => a.Anime)
                .Include(a => a.Person).FirstOrDefaultAsync(m => m.AnimePersonID == id);

            if (AnimePerson == null)
            {
                return NotFound();
            }
            PersonPub = "???";
            AniPub = "???";

            //ViewData["AnimeID"] = new SelectList(_context.Anime, "ID", "Title");
            //ViewData["PersonID"] = new SelectList(_context.Person, "PersonID", "Name");
            ViewData["Anis"] = new List<Anime>(_context.Anime);
            ViewData["Persons"] = new List<Person>(_context.Person);
            if (!String.IsNullOrEmpty(SearchingStringPerson))
            {
                CurrentFilterPerson = SearchingStringPerson;
                foreach (var ps in _context.Person)
                {
                    if (ps.Name.ToUpper().Contains(SearchingStringPerson.ToUpper()))
                    {
                        CurrentFilterPerson = ps.Name;
                        PersonPub = ps.PersonID.ToString();
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
            if (!ModelState.IsValid||!CheckAniVaild()||!CheckPersonValid()||!CheckRepeat())
            {
                return Page();
            }

            _context.Attach(AnimePerson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimePersonExists(AnimePerson.AnimePersonID))
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

        private bool AnimePersonExists(int id)
        {
            return _context.AnimePerson.Any(e => e.AnimePersonID == id);
        }
    }
}
