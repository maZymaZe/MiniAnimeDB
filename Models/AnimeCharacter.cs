using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniAnimeDB.Models
{
    public class AnimeCharacter
    {
        public int AnimeCharacterID { get; set; }
        public int AnimeID { get; set; }
        public int CharacterID { get; set; }

        public Anime Anime { get; set; }
        public Character Character { get; set; }
    }
}
