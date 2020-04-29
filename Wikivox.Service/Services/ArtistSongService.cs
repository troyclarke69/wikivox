using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wikivox.Data;
using Wikivox.Data.Interfaces;
using Wikivox.Models;

namespace Wikivox.Service.Services
{
    public class ArtistSongService : IArtistSong
    {
        private readonly WikivoxDbContext _context;
        public ArtistSongService(WikivoxDbContext context) { _context = context; }
        public void Add(ArtistSong newArtistSong)
        {
            _context.Add(newArtistSong);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.ArtistSong.Where(g => g.Id == id)
               .ToList().ForEach(g => _context.ArtistSong.Remove(g));
            _context.SaveChanges();
        }

        public ArtistSong Get(int id)
        {
            return GetAll().FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<ArtistSong> GetAll()
        {
            return _context.ArtistSong;
        }

        public void Update(ArtistSong newArtistSong)
        {
            throw new NotImplementedException();
        }
    }
}
