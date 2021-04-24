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

namespace MiniAnimeDB.Pages.character_tag
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public CreateModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string SearchingStringCha, string SearchingStringTag)
        {
            //ViewData["CharacterID"] = new SelectList(_context.Character, "CharacterID", "Name");
            //ViewData["TagCID"] = new SelectList(_context.TagC, "TagCID", "Tag");
            ViewData["Tags"] = new List<TagC>(_context.TagC);
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
            TagPub = "???";
            if (!String.IsNullOrEmpty(SearchingStringTag))
            {
                CurrentFilterTag = SearchingStringTag;
                int flag = 0;
                foreach (var tg in _context.TagC)
                {
                    if (tg.Tag.ToUpper().Equals(SearchingStringTag.ToUpper()))
                    {
                        CurrentFilterTag = tg.Tag;
                        TagPub = tg.TagCID.ToString();
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                    foreach (var tg in _context.TagC)
                    {
                        if (tg.Tag.ToUpper().Contains(SearchingStringTag.ToUpper()))
                        {
                            CurrentFilterTag = tg.Tag;
                            TagPub = tg.TagCID.ToString();
                            break;
                        }
                    }
            }
            return Page();
        }

        [BindProperty]
        public CharacterTagC CharacterTagC { get; set; }
        public string CurrentFilterCha { get; set; }
        public string ChaPub { get; set; }
        public string CurrentFilterTag { get; set; }
        public string TagPub { get; set; }

        public bool CheckRepeat()
        {
            foreach (var ct in _context.CharacterTagC)
            {
                if (ct.TagCID == CharacterTagC.TagCID && ct.CharacterID == CharacterTagC.CharacterID) return false;
            }
            return true;
        }
        public bool CheckTagVaild()
        {
            foreach (var tg in _context.TagC)
            {
                if (tg.TagCID == CharacterTagC.TagCID) return true;
            }
            return false;
        }
        public bool CheckChaValid()
        {
            foreach (var ch in _context.Character)
            {
                if (ch.CharacterID == CharacterTagC.CharacterID) return true;
            }
            return false;
        }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !CheckTagVaild() || !CheckRepeat() || !CheckChaValid())
            {
                return Page();
            }

            _context.CharacterTagC.Add(CharacterTagC);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
