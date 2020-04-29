using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Wikivox.Data.Interfaces;
using Wikivox.Models;
using Wikivox.VModels;
using Microsoft.AspNetCore.Http;

namespace Wikivox.Controllers
{
    public class AlbumModController : Controller
    {
        private readonly IArtist _artist;
        private readonly IImage _image;
        private readonly IAlbumSong _albumSong;

        public AlbumModController(IArtist artist, IImage image, IAlbumSong albumSong)
        {
            _artist = artist;
            _image = image;
            _albumSong = albumSong;
        }

        public IActionResult Index(int artistId, int albumId)
        {

            var artistInfoModel = _artist.Get(artistId);
            var albumModel = _albumSong.GetAlbumByArtist(albumId);

            var album = new AlbumListingModel
            {
                Id = albumModel.Album.Id,
                Name = albumModel.Album.Name,
                YrReleased = albumModel.Album.YrReleased
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
        public IActionResult UploadImage(List<IFormFile> files)
        {
            var entity = TempData["entity"].ToString();
            var resource = _artist.Get(int.Parse(TempData["artistId"].ToString())).ArtistName.ToLower();
            var entityId = int.Parse(TempData["entityId"].ToString());
            var resourceId = int.Parse(TempData["resourceId"].ToString());

            _image.UploadImage(files, entity, resource, entityId, resourceId);
            return RedirectToAction("Index", "ArtistInfo", new { id = TempData["artistId"] });
        }
    }
}