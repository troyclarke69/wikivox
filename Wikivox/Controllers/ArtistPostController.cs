using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Wikivox.Data.Interfaces;
using Wikivox.Models;
using Wikivox.VModels;

namespace Wikivox.Controllers
{
    public class ArtistPostController : Controller
    {
        private readonly IGenre _genre;
        private readonly IArtist _artist;
        private readonly IArtistGenre _artistGenre;
        private readonly IImage _image;
        private readonly IArtistMusician _artistMusician;
        private readonly IMusician _musician;
        private readonly IAlbum _album;
        private readonly ISong _song;

        public ArtistPostController(IGenre genre, IArtist artist, IArtistGenre artistGenre, IImage image,
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

        public IActionResult Index()
        {
            var artistInfoModel = _artist.Get(1);
            //var musicianModel = _artistMusician.GetAllByArtist(1);
            //var musicianModel = _musician.Get(1);
            var genreModel = _genre.GetAll();

            var artistInfo = new ArtistListingModel
            {
                Id = 0,
                ArtistName = string.Empty,
                Bio = string.Empty,
                YrFormed = 0,
                YrEnded = 0,
                isActive = 1,
                HomeCountry = string.Empty,
                HomeTown = string.Empty
                //PrimaryImage = _image.GetPrimaryImageByEntity(2, artistInfoModel.Id, 2)
            };
            //var musicians = musicianModel.Select
            //   (r => new MusicianListingModel
            //   {
            //       Id = 0,
            //       Bio = string.Empty,
            //       Birth = System.DateTime.Now,
            //       Death = System.DateTime.Now,
            //       FirstName = string.Empty,
            //       LastName = string.Empty,
            //       MusicianName = string.Empty,
            //       HomeCountry = string.Empty,
            //       HomeTown = string.Empty,
            //       isActive = 1
            //       //PrimaryImage = _image.GetPrimaryImageByEntity(5, r.Musician.Id, 1)
            //   }
            //   ).ToList();
      
            var genres = genreModel.Select
               (g => new GenreListingModel
               {
                   Id = g.Id,
                   Name = g.Name
                   //isMarked = _artistGenre.isMarked(g.Id, id)
               }
               ).ToList();

            var model = new ArtistPostIndexModel
            {
                Artist = artistInfo,
                //Musicians = musicians,
                Genres = genres
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddArtist(ArtistPostIndexModel model)
        {
            if (model == null)
            {
                RedirectToAction("ArtistInfo", new { id = model.Artist.Id });
            }

            var artistModel = model.Artist;
            var genreModel = model.Genres;

            //add the artist - populate an Artist instance with values from the form
            var artist = new Artist
            {
                Id = artistModel.Id,  //is 0 here
                ArtistName = artistModel.ArtistName,
                Bio = artistModel.Bio,
                YrFormed = artistModel.YrFormed,
                YrEnded = artistModel.YrEnded,
                HomeCountry = artistModel.HomeCountry,
                HomeTown = artistModel.HomeTown,
                isActive = artistModel.isActive
            };
            _artist.Add(artist);

            //now add the genre list ...
            var genres = genreModel.Select
               (g => new GenreListingModel
               {
                   Id = g.Id,
                   Name = g.Name,
                   isMarked = g.isMarked
               }
               ).ToList();

            //... so loop through returned genres model to update table...           
            foreach (var g in genres)
            {
                if (g.isMarked)
                {
                    _artistGenre.Add(g.Id, artist.Id);
                };               
            }

            //should go back to details page ... ArtistMod??
            //temp!!! redirect to main page
            return RedirectToAction("Index", "Main", new { genreId = 1, artistId = model.Artist.Id });
        }
    }
}