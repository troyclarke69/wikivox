using System;
using System.Collections.Generic;
using System.Linq;
using Wikivox.Data;
using Wikivox.Data.Interfaces;
using Wikivox.Models;

namespace Wikivox.Service.Services
{
    public class AlbumService : IAlbum
    {
        private readonly WikivoxDbContext _context;
        public AlbumService(WikivoxDbContext context) { _context = context; }
        public void Add(Album newAlbum)
        {
            _context.Add(newAlbum);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Album.Where(g => g.Id == id)
               .ToList().ForEach(g => _context.Album.Remove(g));
            _context.SaveChanges();
        }

        public Album Get(int id)
        {
            return GetAll().FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Album> GetAlbumsByArtist(int id)
        {
            //return _context.Album.Where(a => a.Artist.Id == id);
            throw new NotImplementedException();
        }

        public IEnumerable<Album> GetAll()
        {
            return _context.Album;
        }

        public void Update(Album newAlbum)
        {
            throw new NotImplementedException();
        }
    }
}
