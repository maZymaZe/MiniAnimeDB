using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniAnimeDB.Models
{
    public class PersonCharacter
    {
        public int PersonCharacterID { get; set; }
        public int CharacterID { get; set; }
        public int PersonID { get; set; }

        public Character Character { get; set; }
        public Person Person { get; set; }
    }
}
