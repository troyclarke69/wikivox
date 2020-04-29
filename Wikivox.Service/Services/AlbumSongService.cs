using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using Wikivox.Data;
using Wikivox.Data.Interfaces;
using Wikivox.Models;

namespace Wikivox.Service.Services
{
    public class AlbumSongService : IAlbumSong
    {
        private readonly WikivoxDbContext _context;
        public AlbumSongService(WikivoxDbContext context) { _context = context; }

        public void Add(int albumId, int artistId, int trackNumber, int songId)
        {
            var album = _context.Album.FirstOrDefault(g => g.Id == albumId);
            var artist = _context.Artist.FirstOrDefault(a => a.Id == artistId);
            var song = _context.Song.FirstOrDefault(a => a.Id == songId);

            var newAlbumSong = new AlbumSong
            {
                Artist = artist,
                Album = album,
                TrackNumber = trackNumber,
                Song = song
            }; 

            _context.Add(newAlbumSong);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.AlbumSong.Where(g => g.Id == id)
               .ToList().ForEach(g => _context.AlbumSong.Remove(g));
            _context.SaveChanges();
        }

        public AlbumSong Get(int id)
        {
            return GetAll().FirstOrDefault(g => g.Id == id);
        }

        public AlbumSong GetAlbumByAlbum(int id)
        {
            return _context.AlbumSong
                .Include(a => a.Album)
                .Include(a => a.Artist)
                .Where(a => a.Album.Id == id).FirstOrDefault();
        }

        public AlbumSong GetAlbumByArtist(int id)
        {
            return _context.AlbumSong
                .Include(a => a.Album)
                .Include(a => a.Artist)
                .Where(a => a.Album.Id == id).FirstOrDefault();
        }

        public IEnumerable<AlbumSong> GetAll()
        {
            return _context.AlbumSong;
        }

        public IEnumerable<AlbumSong> GetAllAlbumsByArtist(int id)
        {
            int? songId = null;
            var albums = _context.AlbumSong
                .Include(a => a.Album)
                .Include(a => a.Artist)
                .Include(a => a.Song)
                .Where(a => a.Artist.Id == id  && 
                    (a.Song.Id == songId || a.Song.Id == 0));
          
            return albums.OrderByDescending(a => a.Album.Id);
        }

        public IEnumerable<AlbumSong> GetAllSongsByAlbum(int id)
        {
            return _context.AlbumSong
                .Include(a => a.Album)
                .Include(a => a.Song)
                .Include(a => a.Artist)
                .Where(a => a.Album.Id == id && a.Song.Id > 0);
        }

        public IEnumerable<AlbumSong> GetAllSongsByArtist(int id)
        {
            var songs = _context.AlbumSong
                .Include(a => a.Song)
                .Include(a => a.Artist)
                .Where(a => a.Artist.Id == id && a.Song.Id > 0);

            //take only top 10 for now ..........................
            return songs.Take(10);
        }

        public IEnumerable<AlbumSong> GetRandomSongsByArtist(int id)
        {
            var songs = _context.AlbumSong
                .Include(a => a.Song)
                .Include(a => a.Artist)
                .Where(a => a.Artist.Id == id && a.Song.Id > 0);

            return songs.Take(10);
        }

        public AlbumSong GetSongByAlbum(int id)
        {
            throw new NotImplementedException();
        }

        public AlbumSong GetSongByArtist(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(AlbumSong newAlbumSong)
        {
            throw new NotImplementedException();
        }
    }
}
