using System;
using System.Collections.Generic;
using System.Linq;
using Wikivox.Data;
using Wikivox.Data.Interfaces;
using Wikivox.Models;

namespace Wikivox.Service.Services
{
    public class ArtistService :  IArtist
    {
        private readonly WikivoxDbContext _context;
        public ArtistService(WikivoxDbContext context) { _context = context; }

        public void Add(Artist newArtist)
        {
            _context.Add(newArtist);
            _context.SaveChanges();
        }

        public void Add(Album newAlbum)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            _context.Artist.Where(g => g.Id == id)
               .ToList().ForEach(g => _context.Artist.Remove(g));
            _context.SaveChanges();
        }

        public Artist Get(int id)
        {
            return GetAll()
                .FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Artist> GetAll()
        {
            return _context.Artist;
        }

        public void Update(Artist newArtist)
        {
            _context.Artist.Update(newArtist);
            _context.SaveChanges();
        }
    }
}
