using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Wikivox.Data.Interfaces;
using Wikivox.Models;
using Wikivox.VModels;

namespace Wikivox.Controllers
{
    public class ArtistModController : Controller
    {
        private readonly IGenre _genre;
        private readonly IArtist _artist;
        private readonly IArtistGenre _artistGenre;
        private readonly IImage _image;
        private readonly IArtistMusician _artistMusician;
        private readonly IMusician _musician;
        private readonly IAlbum _album;
        private readonly ISong _song;

        public ArtistModController(IGenre genre, IArtist artist, IArtistGenre artistGenre, IImage image,
                IArtistMusician artistMusician, IMusician musician, IAlbum album, ISong song)
        {
            _genre = genre;
            _artist = artist;
            _artistGenre = artistGenre;
            _image = image;
            _artistMusician = artistMusician;
            _album = album;
            _musician = musician;
            _song = song;
        }

        public IActionResult Index(int id)
        {
            var artistInfoModel = _artist.Get(id);
            var musicianModel = _artistMusician.GetAllByArtist(id);

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

            var musicians = musicianModel.Select
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

            //get all genres
            var genreModel = _genre.GetAll();

            // genre list
            var genres = genreModel.Select
               (g => new GenreListingModel
               {
                   Id = g.Id,
                   Name = g.Name,
                   isMarked = _artistGenre.isMarked(g.Id, id)
               }
               ).ToList();

            var model = new ArtistModIndexModel
            {
                Artist = artistInfo,
                Musicians = musicians,
                Genres = genres
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditArtist(ArtistModIndexModel model)
        {
            if (model == null)
            {
                RedirectToAction("ArtistMod", new { id = model.Artist.Id });
            }

            var artistModel = model.Artist;
            var genreModel = model.Genres;
            //var musicianModel = model.Musicians;  //not editing musicians here - model will be passed but will be null

            //update the artist - populate an Artist instance with values from the form
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
            _artist.Update(artist);


            //now update the genre list ...
            var genres = genreModel.Select
               (g => new GenreListingModel
               {
                   Id = g.Id,
                   Name = g.Name,
                   isMarked = g.isMarked
               }
               ).ToList();

            //... so loop through returned genres model to update table...           
            foreach(var g in genres)
            {
                if (_artistGenre.isMarked(g.Id, model.Artist.Id))
                {
                    //marked is true in DB, so now determine the appropriate action
                    if (!g.isMarked)
                    {
                        //it has been unchecked, so remove the row in the DB
                        _artistGenre.Delete(g.Id, model.Artist.Id);
                    } // else - it is checked = NO ACTION REQD.
                }
                else
                {
                    //marked is false in DB, so now determine the appropriate action
                    if (g.isMarked)
                    {
                        //it has been checked, so add the row in the DB
                        _artistGenre.Add(g.Id, model.Artist.Id);
                    } // else - it is unchecked = NO ACTION REQD.
                }
            }

            @ViewBag.Message = "Saved";
            //should we be handling genre checkbox updates via onclick="this.form.submit();" ??
            return RedirectToAction("Index", "ArtistMod", new { id = model.Artist.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMusician(ArtistModIndexModel model)
        {
            var artist = model.Artist;
            var genreModel = model.Genres;
            var musicianModel = model.Musicians;

            var genres = genreModel.Select
               (g => new GenreListingModel
               {
                   Id = g.Id,
                   Name = g.Name,
                   isMarked = g.isMarked
               }
               ).ToList();



            //... so loop through genres to update table...


            //... and call the artist.update service to update artist


            //where do we go from here? back to ArtistInfo/Index ??

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditGenre(ArtistModIndexModel model)
        {

            var artist = model.Artist;
            var genreModel = model.Genres;
            var musicianModel = model.Musicians;

            //var genres = genreModel.Select
            //   (g => new GenreListingModel
            //   {
            //       Id = g.Id,
            //       Name = g.Name,
            //       isMarked = g.isMarked
            //   }
            //   ).ToList();



            //... so loop through genres to update table...


            //... and call the artist.update service to update artist


            //where do we go from here? back to ArtistInfo/Index ??

            return View();
        }

        public IActionResult SetGenre()
        {
            return View();
        }
    }
}