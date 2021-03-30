using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniAnimeDB.Models;

namespace MiniAnimeDB.Data
{
    public class MiniAnimeDBContext : DbContext
    {
        public MiniAnimeDBContext (DbContextOptions<MiniAnimeDBContext> options)
            : base(options)
        {
        }
        public DbSet<MiniAnimeDB.Models.Anime> Anime { get; set; }
        public DbSet<MiniAnimeDB.Models.Character> Character { get; set; }
        public DbSet<MiniAnimeDB.Models.Person> Person { get; set; }
        public DbSet<MiniAnimeDB.Models.TagA> TagA { get; set; }
        public DbSet<MiniAnimeDB.Models.TagC> TagC { get; set; }
        public DbSet<MiniAnimeDB.Models.AnimePerson> AnimePerson { get; set; }
        public DbSet<MiniAnimeDB.Models.AnimeTagA> AnimeTagA { get; set; }
        public DbSet<MiniAnimeDB.Models.CharacterTagC> CharacterTagC { get; set; }
        public DbSet<MiniAnimeDB.Models.AnimeCharacter> AnimeCharacter { get; set; }
        public DbSet<MiniAnimeDB.Models.PersonCharacter> PersonCharacter { get; set; }

    }
}
