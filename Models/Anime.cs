using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniAnimeDB.Models
{
    public class Anime
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int? Eps { get; set; }
        public double? Rating { get; set; }
        public string Country { get; set; }
        public string Group { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Aired { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Ended { get; set; }

        public ICollection<AnimePerson> Staffs { get; set; }
        public ICollection<AnimeCharacter> Roles { get; set; }
        public ICollection<AnimeTagA> WithTags { get; set; }

    }
}
