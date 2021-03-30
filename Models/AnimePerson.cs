using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniAnimeDB.Models
{
    public class AnimePerson
    {
        public int AnimePersonID { get; set; }
        public int AnimeID { get; set; }
        public int PersonID { get; set; }
        public string Position { get; set; }

        public Anime Anime { get; set; }
        public Person Person { get; set; }


    }
}
