using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wikivox.Data.Interfaces;
using Wikivox.Models;
using Wikivox.VModels;

namespace Wikivox.Controllers
{
    public class MusicianModController : Controller
    {
        private readonly IArtist _artist;
        private readonly IArtistMusician _artistMusician;
        private readonly IMusician _musician;
        private readonly IImage _image;
        private readonly IInstrument _instrument;
        private readonly IMusicianInstrument _musicianInstrument;


        public MusicianModController(IArtist artist, IArtistMusician artistMusician, IMusician musician, 
                           IImage image, IInstrument instrument, IMusicianInstrument musicianInstrument)
        {
            _artist = artist;
            _artistMusician = artistMusician;
            _musician = musician;
            _image = image;
            _instrument = instrument;
            _musicianInstrument = musicianInstrument;
        }

        public IActionResult Index(int artistId, int musicianId)
        {
            var artistInfoModel = _artist.Get(artistId);
            var musicianListModel = _artistMusician.GetAllByArtist(artistId);
            var musicianModel = _musician.Get(musicianId);

            var instrumentModel = _instrument.GetAll();
            var instruments = instrumentModel.Select
               (g => new InstrumentListingModel
               {
                   Id = g.Id,
                   Name = g.Name,
                   IsMarked = _musicianInstrument.IsMarked(musicianId, g.Id)
               }
               ).ToList();

            var artistInfo = new ArtistListingModel
            {
                Id = artistId,
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
                Id = musicianId,
                MusicianName = musicianModel.MusicianName,
                LastName = musicianModel.LastName,
                FirstName = musicianModel.FirstName,
                Bio = musicianModel.Bio,
                Birth = musicianModel.Birth,
                Death = musicianModel.Death,
                isActive = musicianModel.isActive,
                HomeTown = musicianModel.HomeTown,
                HomeCountry = musicianModel.HomeCountry
            };

            var model = new MusicianPostIndexModel
            {
                Artist = artistInfo,
                Musicians = musicians,
                Musician = musician,
                Instruments = instruments
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMusician(MusicianPostIndexModel model)
        {
            if (model == null)
            {
                RedirectToAction("ArtistMod", new { id = model.Artist.Id });
            }

            var artistModel = model.Artist;
            var musicianModel = model.Musician;
            var instrumentModel = model.Instruments;
          
            var artist = new Artist
            {
                Id = artistModel.Id,
                ArtistName = artistModel.ArtistName,
                Bio = artistModel.Bio,
                YrFormed = artistModel.YrFormed,
                YrEnded = artistModel.YrEnded,
                HomeCountry = artistModel.HomeCountry,
                HomeTown = artistModel.HomeTown,
                isActive = artistModel.isActive
            };

            var musician = new Musician
            {
                Id = musicianModel.Id,
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

            //update the Musician **** No Artist Update here ****
            _musician.Update(musician);


            //now update the musicianInstrument list ...
            var instruments = instrumentModel.Select
               (g => new InstrumentListingModel
               {
                   Id = g.Id,
                   Name = g.Name,
                   IsMarked = g.IsMarked
               }
               ).ToList();

            //... so loop through returned genres model to update table...           
            foreach (var g in instruments)
            {
                if (_musicianInstrument.IsMarked(model.Musician.Id, g.Id))
                {
                    //marked is true in DB, so now determine the appropriate action
                    if (!g.IsMarked)
                    {
                        //it has been unchecked, so remove the row in the DB
                        _musicianInstrument.Delete(model.Musician.Id, g.Id);
                    } // else - it is checked = NO ACTION REQD.
                }
                else
                {
                    //marked is false in DB, so now determine the appropriate action
                    if (g.IsMarked)
                    {
                        //it has been checked, so add the row in the DB
                        _musicianInstrument.Add(model.Musician.Id, g.Id);
                    } // else - it is unchecked = NO ACTION REQD.
                }
            }

            @ViewBag.Message = "Saved";
            //should we be handling genre checkbox updates via onclick="this.form.submit();" ??
            return RedirectToAction("Index", "ArtistMod", new { id = model.Artist.Id });
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