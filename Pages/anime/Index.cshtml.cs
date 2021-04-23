using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniAnimeDB.Data;
using MiniAnimeDB.Models;

namespace MiniAnimeDB.Pages.anime
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly MiniAnimeDB.Data.MiniAnimeDBContext _context;

        public IndexModel(MiniAnimeDB.Data.MiniAnimeDBContext context)
        {
            _context = context;
        }

        //public IList<Anime> Anime { get;set; }
        public string TitleSort { get; set; }
        public string AirSort { get; set; }
        public string EndSort { get; set; }
        public string GroupSort { get; set; }
        public string TypeSort { get; set; }
        public string RatingSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Anime> Animes { get; set; }

        
        public async Task OnGetAsync(string sortOrder,string currentFilter,string searchingString,int? pageIndex)
        {
            CurrentSort = sortOrder;
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_asc" : "";
            if (sortOrder == "aired_desc")
            {
                AirSort = "aired_asc";
            }else if (sortOrder == "aired_asc")
            {
                AirSort = "Aired";
            }
            else
            {
                AirSort = "aired_desc";
            }
            if (sortOrder == "ended_desc")
            {
                EndSort = "ended_asc";
            }
            else if (sortOrder == "ended_asc")
            {
               EndSort = "Ended";
            }
            else
            {
                EndSort = "ended_desc";
            }
            GroupSort = (sortOrder == "Group") ? "group_asc" : "Group";
            TypeSort = (sortOrder == "Type") ? "type_asc" : "Type";
            if (sortOrder == "rating_desc")
            {
                RatingSort = "rating_asc";
            }
            else if (sortOrder == "rating_asc")
            {
                RatingSort = "Rating";
            }
            else 
            {
                RatingSort = "rating_desc";
            }
            
            if (searchingString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchingString = currentFilter;
            }
            CurrentFilter = searchingString;
            IQueryable<Anime> AnimesIQ = from s in _context.Anime select s;
            if (!String.IsNullOrEmpty(searchingString))
            {
                AnimesIQ = AnimesIQ.Where(s => s.Title.ToUpper().Contains(searchingString.ToUpper()));
            }
            AnimesIQ = sortOrder switch
            {
                "title_asc" => AnimesIQ.OrderBy(s => s.Title),
                "aired_desc" => AnimesIQ.OrderByDescending(s => s.Aired),
                "aired_asc" => AnimesIQ.OrderBy(s => s.Aired),
                "ended_desc" => AnimesIQ.OrderByDescending(s => s.Ended),
                "ended_asc" => AnimesIQ.OrderBy(s => s.Ended),
                "rating_desc" => AnimesIQ.OrderByDescending(s => s.Rating),
                "rating_asc" => AnimesIQ.OrderBy(s => s.Rating),
                "group_asc" => AnimesIQ.OrderBy(s => s.Group),
                "type_asc" => AnimesIQ.OrderBy(s => s.Type),
                _ => AnimesIQ.OrderBy(s => s.Title),
            };
            int pageSize = 10;
            Animes = await PaginatedList<Anime>.CreateAsync(AnimesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
