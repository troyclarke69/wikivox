using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Wikivox.Data.Interfaces;
using Wikivox.Models;
using Wikivox.VModels;
using System.Threading.Tasks;

namespace Wikivox.Controllers
{
    public class MainController : Controller
    {
        private readonly IGenre _genre;
        private readonly IArtist _artist;
        private readonly IArtistGenre _artistGenre;
        private readonly IImage _image;
        private readonly IArtistMusician _artistMusician;

        public MainController(IGenre genre, IArtist artist, IArtistGenre artistGenre, IImage image,
                IArtistMusician artistMusician)
        {
            _genre = genre;
            _artist = artist;
            _artistGenre = artistGenre;
            _image = image;
            _artistMusician = artistMusician;
        }

        public async Task<IActionResult> Index(int artistId, MainIndexModel srch)
        {
            var url = "https://localhost:44334/api/SearchRest/searchArtistsByGenres?"; // put in appSettings
            //var url = "https://wikivox.azurewebsites.net/api/SearchRest/searchArtistsByGenres?"; // put in appSettings
            var terms = string.Empty;

            if (srch.GenreListing == null)
            {
                // default genre search
                terms = "term=1";
            }
            else
            {
                //loop thru list and obtain search terms
                foreach(var g in srch.GenreListing)
                {
                    if (g.isMarked)
                    {
                        if (terms == string.Empty)
                        {
                            terms += "term=" + g.Id;
                        }
                        else
                        {
                            terms += "&term=" + g.Id;
                        }
                    }
                }
            }

            List<Artist> artistModel = new List<Artist>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url + terms))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    artistModel = JsonConvert.DeserializeObject<List<Artist>>(apiResponse);
                }
            }

            // by default, load one artist in object for launch page
            Random rnd = new Random();
            int artistNum = _artist.GetAll().Count();

            //var x = x - 1;
            
            int defaultArtistId = rnd.Next(1, artistNum - 1);
            var artistInfoModel = _artist.Get(defaultArtistId);

            // if an artist has been selected, gather details to display
            if (artistId != 0)
            {
                artistInfoModel = _artist.Get(artistId);
            }

            var genreModel = _genre.GetAll();
            var artistGenreModel = _artistGenre.GetAll();

            // genre list
            var genres = genreModel.Select
               (g => new GenreListingModel
               {
                   Id = g.Id,
                   Name = g.Name
               }
               ).ToList();

            // artist list
            var artists = artistModel.Select
               (a => new ArtistListingModel
               {
                   Id = a.Id,
                   ArtistName = a.ArtistName
               }
               ).ToList();

            var artistGenres = artistGenreModel.Select
               (r => new ArtistGenreModel
               {
                   Id = r.Id,
                   Artist = r.Artist,
                   Genre = r.Genre
               }
               ).ToList();

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
                PrimaryImage = _image.GetPrimaryImageByEntity(2, artistInfoModel.Id, 1)
            };
               
            var model = new MainIndexModel
            {
                GenreListing = genres,
                ArtistListing = artists,
                ArtistGenre = artistGenres,
                ArtistInfo = artistInfo
            };

            return View(model);
        }

        public IActionResult Detail(int id)
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
                PrimaryImage = _image.GetPrimaryImageByEntity(2, artistInfoModel.Id, 1)
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

            var model = new MainDetailModel
            {
                Artist = artistInfo,
                Musicians = musicians
            };

            return View(model);

        }

        public IActionResult GenreFilter(int genreId)
        {
            return RedirectToAction("Index", new { id = genreId });
        }

        public IActionResult ArtistFilter(int artistId)
        {
            return RedirectToAction("Index", new { artistId = artistId });
        }
    }
}