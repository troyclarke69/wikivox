using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Wikivox.Data.Interfaces;
using Wikivox.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wikivox.Controllers
{
    [Route("api/[controller]")]
    public class SearchRestController : Controller
    {
        private readonly IArtist _artist;
        private readonly IGenre _genre;
        private readonly IAlbum _album;
        private readonly IMusician _musician;
        private readonly IArtistGenre _artistGenre;

        public SearchRestController(IArtist artist, IGenre genre, IAlbum album, IMusician musician, IArtistGenre artistGenre)
        {
            _artist = artist;
            _genre = genre;
            _album = album;
            _musician = musician;
            _artistGenre = artistGenre;
        }

        [Produces("application/json")]
        [HttpGet("searchGenre")]
        public IActionResult SearchGenre()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = _genre.GetAll()
                    .Where(p => p.Name.ToUpper().Contains(term.ToUpper()))
                        .Select(p => p.Name).ToList();

                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        //[Produces("application/json")]
        //[HttpGet("searchByGenres")]
        //// https://localhost:44334/api/SearchRest/searchArtistsByGenres?term=1&term=2
        //public IActionResult SearchByGenres()
        //{
        //    try
        //    {
        //        string term = HttpContext.Request.Query["term"].ToString();

        //        // default
        //        var artistList = _artistGenre.GetAll()
        //                .Where(p => p.Genre.Id.ToString().Equals(1))
        //                .Select(p => p.Artist).ToList();
        //        //artistList.Clear();

        //        var artists = _artistGenre.GetAll()
        //                .Where(p => p.Genre.Id.ToString().Equals(1))
        //                .Select(p => p.Artist).ToList();
        //        //artists.Clear();

        //        foreach (var a in term)
        //        {
        //            artists = _artistGenre.GetAll()
        //                .Where(p => p.Genre.Id.ToString().Equals(a))
        //                .Select(p => p.Artist).ToList();
        //        }

        //        artistList.Add(artists);

        //        return Ok(artistList);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        [Produces("application/json")]
        [HttpGet("searchArtistsByGenres")]
        // https://localhost:44334/api/SearchRest/searchArtistsByGenres?term=1&term=2
        public IActionResult SearchArtistsByGenres()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();

                IEnumerable<string> terms = term.Split(',');

                int c = terms.Count();
                // ** for brevity > only allow up to 3 genres picked **
                if (c > 3)
                {
                    c = 3;
                }

                // ** CONTAINS returns genres 10, 11, 12, etc if passed in value = 1 **
                // ** this is NOT the graceful way I hoped to achieve this, however other attempts failed to work **

                if (c == 1)
                {
                    var artists = _artistGenre.GetAll()
                          .Where(p => p.Genre.Id.ToString().Equals(((string[])terms)[0]))
                          .Select(p => p.Artist).OrderBy(p => p.ArtistName).ToList();

                    return Ok(artists.Distinct());
                }
                if (c == 2)
                {
                    var artists = _artistGenre.GetAll()
                          .Where(p => p.Genre.Id.ToString().Equals(((string[])terms)[0]) ||
                                   p.Genre.Id.ToString().Equals(((string[])terms)[1]) )
                          .Select(p => p.Artist).OrderBy(p => p.ArtistName).ToList();

                    return Ok(artists.Distinct());
                }
                if (c == 3)
                {
                    var artists = _artistGenre.GetAll()
                          .Where(p => p.Genre.Id.ToString().Equals(((string[])terms)[0]) ||
                                   p.Genre.Id.ToString().Equals(((string[])terms)[1]) ||
                                   p.Genre.Id.ToString().Equals(((string[])terms)[2]))
                          .Select(p => p.Artist).OrderBy(p => p.ArtistName).ToList();

                    return Ok(artists.Distinct());
                }
                
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("searchArtist")]
        // https://localhost:44334/api/SearchRest/searchArtist?term=U  >> returns U2
        public IActionResult SearchArtist()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = _artist.GetAll()
                    .Where(p => p.ArtistName.ToUpper().StartsWith(term.ToUpper()))
                        .Select(p => p.ArtistName).ToList();

                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("searchAlbum")]
        public IActionResult SearchAlbum()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = _album.GetAll()
                    .Where(p => p.Name.ToUpper().StartsWith(term.ToUpper()))
                        .Select(p => p.Name).ToList();

                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("searchMusician")]
        public IActionResult SearchMusician()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = _musician.GetAll()
                    .Where(p => p.MusicianName.ToUpper().StartsWith(term.ToUpper()))
                        .Select(p => p.MusicianName).ToList();

                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        public IActionResult SetGenre()
        {
            //var x = 1;
            return null;
        }
    }
}
