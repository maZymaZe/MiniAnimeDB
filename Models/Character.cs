using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniAnimeDB.Models
{
    public class Character
    {
        public int CharacterID { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Sex { get; set; }

        public ICollection<AnimeCharacter> Roles { get; set; }
        public ICollection<PersonCharacter> Casts { get; set; }
        public ICollection<CharacterTagC> WithTags { get; set; }

    }
}
