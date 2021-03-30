using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniAnimeDB.Models
{
    public class TagC
    {
        public int TagCID { get; set; }
        public string Tag { get; set; }

        public ICollection<CharacterTagC> WithTags { get; set; }

    }
}
