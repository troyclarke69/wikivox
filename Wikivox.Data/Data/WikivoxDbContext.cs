using Microsoft.EntityFrameworkCore;

namespace Wikivox.Data
{
    public class WikivoxDbContext : DbContext
    {
        public WikivoxDbContext(DbContextOptions<WikivoxDbContext> options) : base(options)
        { }
 
        public DbSet<Wikivox.Models.Artist> Artist { get; set; }
        public DbSet<Wikivox.Models.Album> Album { get; set; }
        public DbSet<Wikivox.Models.AlbumSong> AlbumSong { get; set; }
        public DbSet<Wikivox.Models.ArtistGenre> ArtistGenre { get; set; }
        public DbSet<Wikivox.Models.ArtistMusician> ArtistMusician { get; set; }
        public DbSet<Models.Genre> Genre { get; set; }
        public DbSet<Wikivox.Models.Instrument> Instrument { get; set; }
        public DbSet<Wikivox.Models.Musician> Musician { get; set; }
        public DbSet<Wikivox.Models.MusicianInstrument> MusicianInstrument { get; set; }
        public DbSet<Wikivox.Models.Song> Song { get; set; }
        public DbSet<Wikivox.Models.Entity> Entity { get; set; }
        public DbSet<Wikivox.Models.Image> Image { get; set; }
        public DbSet<Wikivox.Models.ArtistSong> ArtistSong { get; set; }



    }
}
