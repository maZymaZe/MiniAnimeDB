using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniAnimeDB.Models
{
    public class AnimeTagA
    {
        public int AnimeTagAID { get; set; }
        public int AnimeID { get; set; }
        public int TagAID { get; set; }

        public Anime Anime { get; set; }
        public TagA TagA { get; set; }
    }
}
