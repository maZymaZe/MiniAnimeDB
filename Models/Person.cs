using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MiniAnimeDB.Models
{
    public class Person
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public ICollection<AnimePerson> Staffs { get; set; }
        public ICollection<PersonCharacter> Casts { get; set; }

        public Person()
        {
            Name = "";
            Country = "";



        }
    }
}
