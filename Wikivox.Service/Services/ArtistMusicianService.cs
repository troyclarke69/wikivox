using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Wikivox.Data;
using Wikivox.Data.Interfaces;
using Wikivox.Models;

namespace Wikivox.Service.Services
{
    public class ArtistMusicianService : IArtistMusician
    {
        private readonly WikivoxDbContext _context;
        public ArtistMusicianService(WikivoxDbContext context) { _context = context; }
        public void Add(ArtistMusician newArtistMusician)
        {
            _context.Add(newArtistMusician);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.ArtistMusician.Where(g => g.Id == id)
               .ToList().ForEach(g => _context.ArtistMusician.Remove(g));
            _context.SaveChanges();
        }

        public ArtistMusician Get(int id)
        {
            return GetAll().FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<ArtistMusician> GetAll()
        {
            return _context.ArtistMusician;
        }

        public IEnumerable<ArtistMusician> GetAllByArtist(int id)
        {
            return _context.ArtistMusician
                .Include(m => m.Musician)
                .Where(m => m.Artist.Id == id);
        }

        public void Update(ArtistMusician newArtistMusician)
        {
            throw new NotImplementedException();
        }
    }
}
