using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Wikivox.Data.Interfaces;
using Wikivox.Models;
using Wikivox.VModels;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Wikivox.Controllers
{
    public class SongPostController : Controller
    {
        private readonly IArtist _artist;
        private readonly IImage _image;
        private readonly IAlbum _album;
        private readonly IAlbumSong _albumSong;
        private readonly ISong _song;
        private readonly IArtistSong _artistSong;

        public SongPostController(IArtist artist, IImage image, IAlbum album, IAlbumSong albumSong,
                            ISong song, IArtistSong artistSong)
        {
            _artist = artist;
            _image = image;
            _album = album;
            _albumSong = albumSong;
            _song = song;
            _artistSong = artistSong;
        }

        public IActionResult Index(int albumId, int artistId) //artistId always passed, albumId optional
        {

            var artistModel = _artist.Get(artistId);
            var albumModel = _albumSong.GetAllAlbumsByArtist(artistId).FirstOrDefault(); //return 1, may be 0, 1, or M.
            var songModel = _albumSong.GetRandomSongsByArtist(artistId); //may be 0, 1, or M.
            var allSongsModel = _albumSong.GetAllSongsByArtist(artistId);

            //var artistAlbumInfoModel = _albumSong.GetAlbumByAlbum(albumId);
            //var songModel = _albumSong.GetAllSongsByAlbum(albumId);
            if (albumId != 0)
            {
                albumModel = _albumSong.GetAlbumByAlbum(albumId);
                songModel = _albumSong.GetAllSongsByAlbum(albumId);
            }

            //empty song object - bind to form
            var song = new SongListingModel
            {
                Id = 0,
                Name = string.Empty,
                Duration = string.Empty,
                TrackNumber = 0
            };

            //all existing songs on album, if any
            var songs = songModel.Select
              (r => new SongListingModel
              {
                  Id = r.Song.Id,
                  Name = r.Song.Name,
                  Duration = r.Song.Duration,
                  TrackNumber = r.TrackNumber
                   //PrimaryImage = _image.GetPrimaryImageByEntity(1, r.Album.Id, 1)
               }
              ).ToList();

            var album = new AlbumListingModel
            {
                Id = albumModel.Album.Id,
                Name = albumModel.Album.Name,
                YrReleased = albumModel.Album.YrReleased,
                PrimaryImage = _image.GetPrimaryImageByEntity(1, albumModel.Album.Id, 1)
            };

            var artistInfo = new ArtistListingModel
            {
                Id = artistModel.Id,
                ArtistName = artistModel.ArtistName,
                Bio = artistModel.Bio,
                YrFormed = artistModel.YrFormed,
                YrEnded = artistModel.YrEnded,
                isActive = artistModel.isActive,
                HomeCountry = artistModel.HomeCountry,
                HomeTown = artistModel.HomeTown,
                PrimaryImage = _image.GetPrimaryImageByEntity(2, artistModel.Id, 2)
            };

            var model = new SongPostIndexModel
            {
                Artist = artistInfo,
                Album = album,
                Songs = songs,
                Song = song
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSong(SongPostIndexModel model)
        {
            if (model == null)
            {
                RedirectToAction("ArtistInfo", new { id = model.Artist.Id });
            }

            var artistModel = model.Artist;
            var songModel = model.Song;
            
            //add the song - populate an Song instance with values from the form
            var newSong = new Song
            {               
                Name = songModel.Name,
                Duration = songModel.Duration,
            };

            _song.Add(newSong);

            //insert association record for AlbumId, ArtistId, SongId
            if (model.Album.Id != 0)
            {
                _albumSong.Add(model.Album.Id, artistModel.Id, model.Song.TrackNumber, newSong.Id);
            }
            else
            {
                //song is not associated to an album
                _albumSong.Add(0, artistModel.Id, model.Song.TrackNumber, newSong.Id);
            }

            return RedirectToAction("Index", "SongPost", 
                    new { albumId = model.Album.Id, artistId = model.Artist.Id });
        }
    }
}