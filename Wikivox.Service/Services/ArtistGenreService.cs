using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Wikivox.Data;
using Wikivox.Data.Interfaces;
using Wikivox.Models;

namespace Wikivox.Service.Services
{
    public class ArtistGenreService : IArtistGenre
    {
        private readonly WikivoxDbContext _context;
        public ArtistGenreService(WikivoxDbContext context) { _context = context; }

        public void Add(ArtistGenre newArtistGenre)
        {
            _context.Add(newArtistGenre);
            _context.SaveChanges();
        }

        public IEnumerable<ArtistGenre> GetArtistsByGenre(int id)
        {
            return _context.ArtistGenre
                .Include(d => d.Artist)
                .Where(d => d.Genre.Id == id);
        }

        //public void Delete(int id)
        //{
        //    _context.ArtistGenre.Where(g => g.Id == id)
        //       .ToList().ForEach(g => _context.ArtistGenre.Remove(g));
        //    _context.SaveChanges();
        //}

        public ArtistGenre Get(int id)
        {
            return GetAll().FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<ArtistGenre> GetAll()
        {
            return _context.ArtistGenre
                .Include(g => g.Genre)
                .Include(g => g.Artist);
        }

        public IEnumerable<ArtistGenre> GetGenresByArtist(int id)
        {
            return _context.ArtistGenre
                .Include(a => a.Genre)
                .Where(a => a.Artist.Id == id);
        }

        public bool isMarked(int genreId, int artistId)
        {
            var marked = _context.ArtistGenre.Where(a => a.Genre.Id == genreId && a.Artist.Id == artistId);
            var result = false;
            if (marked.Any()) result = true;
            return result;
        }

        public void Add(int genreId, int artistId)
        {
            var genre = _context.Genre.FirstOrDefault(g => g.Id == genreId);
            var artist = _context.Artist.FirstOrDefault(a => a.Id == artistId);
            var newArtistGenre = new ArtistGenre
            {
                Artist = artist,
                Genre = genre
            };

            _context.Add(newArtistGenre);
            _context.SaveChanges();
        }

        public void Delete(int genreId, int artistId)
        {
            //var genre = new Genre
            //{ Id = genreId };
            //var artist = new Artist
            //{ Id = artistId };

            _context.ArtistGenre.Where(a => a.Artist.Id == artistId 
                && a.Genre.Id == genreId)
              .ToList().ForEach(a => _context.ArtistGenre.Remove(a));

            //_context.ArtistGenre.Remove(delArtistGenre);
            _context.SaveChanges();
        }

        public ArtistGenre GetByArtistGenre(int genreId, int artistId)
        {
            throw new NotImplementedException();
        }
    }
}
