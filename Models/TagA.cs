using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniAnimeDB.Models
{
    public class TagA
    {
        public int TagAID { get; set; }
        public string Tag { get; set; }

        public ICollection<AnimeTagA> WithTags { get; set; }
    }
}
