using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wikivox.Data.Interfaces;
using Wikivox.Models;
using Wikivox.VModels;

namespace Wikivox.Controllers
{
    public class MusicianPostController : Controller
    {
        private readonly IArtist _artist;
        private readonly IArtistMusician _artistMusician;
        private readonly IMusician _musician;
        private readonly IImage _image;

        public MusicianPostController(IArtist artist, IArtistMusician artistMusician, IMusician musician, IImage image)
        {
            _artist = artist;
            _artistMusician = artistMusician;
            _musician = musician;
            _image = image;
        }

        public IActionResult Index(int id)
        {
            var artistInfoModel = _artist.Get(id);
            var musicianListModel = _artistMusician.GetAllByArtist(id);

            var artistInfo = new ArtistListingModel
            {
                Id = id,
                ArtistName = artistInfoModel.ArtistName,
                Bio = artistInfoModel.Bio,
                YrFormed = artistInfoModel.YrFormed,
                YrEnded = artistInfoModel.YrEnded,
                isActive = artistInfoModel.isActive,
                HomeCountry = artistInfoModel.HomeCountry,
                HomeTown = artistInfoModel.HomeTown,
                PrimaryImage = _image.GetPrimaryImageByEntity(2, artistInfoModel.Id, 2)
            };

            //existing musicians
            var musicians = musicianListModel.Select
               (r => new MusicianListingModel
               {
                   Id = r.Musician.Id,
                   Bio = r.Musician.Bio,
                   Birth = r.Musician.Birth,
                   Death = r.Musician.Death,
                   FirstName = r.Musician.FirstName,
                   LastName = r.Musician.LastName,
                   MusicianName = r.Musician.MusicianName,
                   HomeCountry = r.Musician.HomeCountry,
                   HomeTown = r.Musician.HomeTown,
                   isActive = r.Musician.isActive,
                   PrimaryImage = _image.GetPrimaryImageByEntity(5, r.Musician.Id, 1)
               }
               ).ToList();

            //new musician
            var musician = new MusicianListingModel
               {
                   Id = 0,
                   MusicianName = string.Empty,
                   LastName = string.Empty,
                   FirstName = string.Empty,
                   Bio = string.Empty,
                   Birth = DateTime.Now,
                   Death = DateTime.Now,
                   isActive = 0,
                   HomeTown = string.Empty,
                   HomeCountry = string.Empty
               };

            var model = new MusicianPostIndexModel
            {
                Artist = artistInfo,
                Musicians = musicians,
                Musician = musician
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMusician(MusicianPostIndexModel model)
        {
            if (model == null)
            {
                RedirectToAction("ArtistInfo", new { id = model.Artist.Id });
            }

            var artistModel = _artist.Get(model.Artist.Id);
            var musicianModel = model.Musician;

            //add the musician - populate an Musician instance with values from the form
            var musician = new Musician
            {
                Id = musicianModel.Id,  //is 0 here
                MusicianName = musicianModel.MusicianName,
                LastName = musicianModel.LastName,
                FirstName = musicianModel.FirstName,
                Bio = musicianModel.Bio,
                Birth = musicianModel.Birth,
                Death = musicianModel.Death,
                HomeCountry = musicianModel.HomeCountry,
                HomeTown = musicianModel.HomeTown,
                isActive = musicianModel.isActive
            };

            //insert the musician record
            _musician.Add(musician);

            //add the artist association
            var artist = new Artist
            {
                Id = artistModel.Id,
                ArtistName = artistModel.ArtistName,
                Bio = artistModel.Bio,
                YrFormed = artistModel.YrFormed,
                YrEnded = artistModel.YrEnded,
                isActive = artistModel.isActive,
                HomeTown = artistModel.HomeTown,
                HomeCountry = artistModel.HomeCountry
            };

            var artistMusician = new ArtistMusician
            {
                Artist = artist,
                Musician = musician
            };
            _artistMusician.Add(artistMusician);

            //return RedirectToAction("Index", "MusicianPost", 
            //        new { id = model.Artist.Id });

            return RedirectToAction("Index", "MusicianMod",
                    new { artistId = model.Artist.Id, musicianId = musician.Id });
        }
    }
}