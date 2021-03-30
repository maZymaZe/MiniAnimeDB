using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniAnimeDB.Models
{
    public class CharacterTagC
    {
        public int CharacterTagCID { get; set; }
        public int CharacterID { get; set; }
        public int TagCID { get; set; }

        public Character Character{ get; set; }
        public TagC TagC { get; set; }
    }
}
