using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Wikivox.Data.Interfaces;
using Wikivox.Models;
using Wikivox.VModels;
using Microsoft.AspNetCore.Http;

namespace Wikivox.Controllers
{
    public class AlbumPostController : Controller
    {
        private readonly IArtist _artist;
        private readonly IImage _image;
        private readonly IAlbum _album;
        private readonly IAlbumSong _albumSong;

        public AlbumPostController(IArtist artist, IImage image, IAlbum album, IAlbumSong albumSong)
        {
            _artist = artist;
            _image = image;
            _album = album;
            _albumSong = albumSong;
        }
        
        public IActionResult Index(int id)
        {
            var artistInfoModel = _artist.Get(id);

            //empty album object
            var album = new AlbumListingModel
            {
                Id = 0,
                Name = string.Empty,
                YrReleased = 0
            };

            var artistInfo = new ArtistListingModel
            {
                Id = artistInfoModel.Id,
                ArtistName = artistInfoModel.ArtistName,
                Bio = artistInfoModel.Bio,
                YrFormed = artistInfoModel.YrFormed,
                YrEnded = artistInfoModel.YrEnded,
                isActive = artistInfoModel.isActive,
                HomeCountry = artistInfoModel.HomeCountry,
                HomeTown = artistInfoModel.HomeTown,
                PrimaryImage = _image.GetPrimaryImageByEntity(2, artistInfoModel.Id, 2)
            };

            var model = new AlbumPostIndexModel
            {
                Artist = artistInfo,
                Album = album
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAlbum(AlbumPostIndexModel model)
        {
            if (model == null)
            {
                RedirectToAction("ArtistInfo", new { id = model.Artist.Id });
            }

            var artistModel = model.Artist;
            var albumModel = model.Album;
        
            //add the album - populate an Album instance with values from the form
            var album = new Album
            {
                Id = albumModel.Id,
                Name = albumModel.Name,
                YrReleased = albumModel.YrReleased              
            };

            _album.Add(album);

            _albumSong.Add(album.Id, artistModel.Id, 0, 0);

            return RedirectToAction("Index", "AlbumMod", 
                new { artistId = artistModel.Id, albumId = album.Id });
        }
    }
}