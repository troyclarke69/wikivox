using System;
using System.Collections.Generic;
using System.Linq;
using Wikivox.Data;
using Wikivox.Data.Interfaces;
using Wikivox.Models;

namespace Wikivox.Service.Services
{
    public class MusicianService : IMusician
    {
        private readonly WikivoxDbContext _context;
        public MusicianService(WikivoxDbContext context) { _context = context; }
        public void Add(Musician newMusician)
        {
            _context.Add(newMusician);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Musician.Where(g => g.Id == id)
               .ToList().ForEach(g => _context.Musician.Remove(g));
            _context.SaveChanges();
        }

        public Musician Get(int id)
        {
            return GetAll().FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Musician> GetAll()
        {
            return _context.Musician;
        }

        public IEnumerable<Musician> GetAllByArtist(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Musician> GetAllByInstrument(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Musician newMusician)
        {
            _context.Musician.Update(newMusician);
            _context.SaveChanges();
        }
    }
}
