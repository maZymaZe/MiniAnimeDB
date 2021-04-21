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

namespace MiniAnimeDB.Pages.character_tag
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
        public CharacterTagC CharacterTagC { get; set; }
        public string CurrentFilterCha { get; set; }
        public string ChaPub { get; set; }
        public string CurrentFilterTag { get; set; }
        public string TagPub { get; set; }

        public bool CheckRepeat()
        {
            foreach (var ct in _context.CharacterTagC.AsNoTracking())
            {
                if (ct.TagCID == CharacterTagC.TagCID && ct.CharacterID == CharacterTagC.CharacterID) return false;
            }
            return true;
        }
        public bool CheckTagVaild()
        {
            foreach (var tg in _context.TagC.AsNoTracking())
            {
                if (tg.TagCID==CharacterTagC.TagCID) return true;
            }
            return false;
        }
        public bool CheckChaValid()
        {
            foreach (var ch in _context.Character.AsNoTracking())
            {
                if (ch.CharacterID == CharacterTagC.CharacterID) return true;
            }
            return false;
        }
        public int bid = 0;
        public async Task<IActionResult> OnGetAsync(int? id, string SearchingStringCha, string SearchingStringTag)
        {
            if (id == null)
            {
                return NotFound();
            }
            bid = Convert.ToInt32(id);
            CharacterTagC = await _context.CharacterTagC
                .Include(c => c.Character)
                .Include(c => c.TagC).FirstOrDefaultAsync(m => m.CharacterTagCID == id);

            if (CharacterTagC == null)
            {
                return NotFound();
            }
            //ViewData["CharacterID"] = new SelectList(_context.Character, "CharacterID", "Name");
            //ViewData["TagCID"] = new SelectList(_context.TagC, "TagCID", "Tag");
            ViewData["Tags"] = new List<TagC>(_context.TagC);
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
            TagPub = "???";
            if (!String.IsNullOrEmpty(SearchingStringTag))
            {
                CurrentFilterTag = SearchingStringTag;
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !CheckTagVaild() || !CheckRepeat() || !CheckChaValid())
            {
                return Page();
            }

            _context.Attach(CharacterTagC).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterTagCExists(CharacterTagC.CharacterTagCID))
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

        private bool CharacterTagCExists(int id)
        {
            return _context.CharacterTagC.Any(e => e.CharacterTagCID == id);
        }
    }
}
