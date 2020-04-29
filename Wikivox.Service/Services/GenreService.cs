using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wikivox.Data;
using Wikivox.Data.Interfaces;
using Wikivox.Models;

namespace Wikivox.Service.Services
{
    public class GenreService : IGenre
    {
        private readonly WikivoxDbContext _context;
        public GenreService(WikivoxDbContext context)  {  _context = context; }

        public void Add(Genre newGenre)
        {
            _context.Add(newGenre);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Genre.Where(g => g.Id == id)
               .ToList().ForEach(g => _context.Genre.Remove(g));
            _context.SaveChanges();
        }

        public Genre Get(int id)
        {
            return GetAll().FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Genre> GetAll()
        {
            return _context.Genre;
        }

        public void Update(Genre newGenre)
        {
            throw new NotImplementedException();
        }
    }
}
