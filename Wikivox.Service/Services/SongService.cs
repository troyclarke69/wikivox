using System;
using System.Collections.Generic;
using System.Linq;
using Wikivox.Data;
using Wikivox.Data.Interfaces;
using Wikivox.Models;

namespace Wikivox.Service.Services
{
    public class SongService : ISong
    {
        private readonly WikivoxDbContext _context;
        public SongService(WikivoxDbContext context) { _context = context; }
        public void Add(Song newSong)
        {
            _context.Add(newSong);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Song.Where(g => g.Id == id)
               .ToList().ForEach(g => _context.Song.Remove(g));
            _context.SaveChanges();
        }

        public Song Get(int id)
        {
            return GetAll().FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Song> GetAll()
        {
            return _context.Song;
        }

        public void Update(Song newSong)
        {
            throw new NotImplementedException();
        }
    }
}
