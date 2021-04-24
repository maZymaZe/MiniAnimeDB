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

namespace MiniAnimeDB.Pages.anime_person
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public CreateModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string SearchingStringPerson, string SearchingStringAni)
        {
            ViewData["Anis"] = new List<Anime>(_context.Anime);
            ViewData["Persons"] = new List<Person>(_context.Person);
            PersonPub = "???";
            if (!String.IsNullOrEmpty(SearchingStringPerson))
            {
                CurrentFilterPerson = SearchingStringPerson;
                int flag = 0;
                foreach (var ps in _context.Person)
                {
                    if (ps.Name.ToUpper().Equals(SearchingStringPerson.ToUpper()))
                    {
                        CurrentFilterPerson = ps.Name;
                        PersonPub = ps.PersonID.ToString();
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0) foreach (var ps in _context.Person)
                    {
                        if (ps.Name.ToUpper().Contains(SearchingStringPerson.ToUpper()))
                        {
                            CurrentFilterPerson = ps.Name;
                            PersonPub = ps.PersonID.ToString();
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
                    if (an.Title.ToUpper().Contains(SearchingStringAni.ToUpper()))
                    {
                        CurrentFilterAni = an.Title;
                        AniPub = an.ID.ToString();
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
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
        public AnimePerson AnimePerson { get; set; }
        public string CurrentFilterPerson { get; set; }
        public string PersonPub { get; set; }
        public string CurrentFilterAni { get; set; }
        public string AniPub { get; set; }

        public bool CheckRepeat()
        {
            foreach (var ap in _context.AnimePerson)
            {
                if (ap.AnimeID == AnimePerson.AnimeID && ap.PersonID == AnimePerson.PersonID && ap.Position == AnimePerson.Position) return false;
            }
            return true;
        }
        public bool CheckAniVaild()
        {
            foreach (var an in _context.Anime)
            {
                if (an.ID == AnimePerson.AnimeID) return true;
            }
            return false;
        }
        public bool CheckPersonValid()
        {
            foreach (var ps in _context.Person)
            {
                if (ps.PersonID == AnimePerson.PersonID) return true;
            }
            return false;
        }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !CheckAniVaild() || !CheckRepeat() || !CheckPersonValid())
            {
                return Page();
            }

            _context.AnimePerson.Add(AnimePerson);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
