using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Wikivox.Data.Interfaces;
using Wikivox.Models;
using Wikivox.VModels;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Wikivox.Controllers
{
    public class AlbumInfoController : Controller
    {
        private readonly IArtist _artist;
        private readonly IImage _image;
        private readonly IAlbum _album;
        private readonly IAlbumSong _albumSong;

        public AlbumInfoController(IArtist artist, IImage image, IAlbum album, IAlbumSong albumSong)
        {
            _artist = artist;
            _image = image;
            _album = album;
            _albumSong = albumSong;
        }

        public IActionResult Index(int id)  //Album.Id passed in
        {
            var artistAlbumInfoModel = _albumSong.GetAlbumByAlbum(id);
            var songModel = _albumSong.GetAllSongsByAlbum(id);

            var album = new AlbumListingModel
            {
                Id = artistAlbumInfoModel.Album.Id,
                Name = artistAlbumInfoModel.Album.Name,
                YrReleased = artistAlbumInfoModel.Album.YrReleased,
                PrimaryImage = _image.GetPrimaryImageByEntity(1, artistAlbumInfoModel.Album.Id, 1)
            };

            var artistInfo = new ArtistListingModel
            {
                Id = artistAlbumInfoModel.Artist.Id,
                ArtistName = artistAlbumInfoModel.Artist.ArtistName,
                Bio = artistAlbumInfoModel.Artist.Bio,
                YrFormed = artistAlbumInfoModel.Artist.YrFormed,
                YrEnded = artistAlbumInfoModel.Artist.YrEnded,
                isActive = artistAlbumInfoModel.Artist.isActive,
                HomeCountry = artistAlbumInfoModel.Artist.HomeCountry,
                HomeTown = artistAlbumInfoModel.Artist.HomeTown,
                PrimaryImage = _image.GetPrimaryImageByEntity(2, artistAlbumInfoModel.Artist.Id, 2)
            };

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

            var model = new AlbumInfoIndexModel
            {
                Artist = artistInfo,
                Album = album,
                Songs = songs
            };

            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddSong(AlbumInfoIndexModel model)
        //{
        //    if (model == null)
        //    {
        //        RedirectToAction("ArtistInfo", new { id = model.Artist.Id });
        //    }

        //    var artistModel = model.Artist;
        //    var albumModel = model.Album;

        //    //add the album - populate an Album instance with values from the form
        //    var album = new Album
        //    {
        //        Id = albumModel.Id,
        //        Name = albumModel.Name,
        //        YrReleased = albumModel.YrReleased
        //    };

        //    _album.Add(album);

        //    _albumSong.Add(album.Id, artistModel.Id, 0);

        //    return RedirectToAction("Index", "AlbumMod",
        //        new { artistId = artistModel.Id, albumId = album.Id });
        //}
    }
}