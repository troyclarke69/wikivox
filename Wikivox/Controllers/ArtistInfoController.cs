using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Wikivox.Data.Interfaces;
using Wikivox.VModels;

namespace Wikivox.Controllers
{
    public class ArtistInfoController : Controller
    {
        private readonly IGenre _genre;
        private readonly IArtist _artist;
        private readonly IArtistGenre _artistGenre;
        private readonly IImage _image;
        private readonly IArtistMusician _artistMusician;
        private readonly IMusician _musician;
        private readonly IAlbum _album;
        private readonly ISong _song;
        private readonly IAlbumSong _albumSong;

        public ArtistInfoController(IGenre genre, IArtist artist, IArtistGenre artistGenre, IImage image,
                IArtistMusician artistMusician, IMusician musician, IAlbum album, ISong song, IAlbumSong albumSong)
        {
            _genre = genre;
            _artist = artist;
            _artistGenre = artistGenre;
            _image = image;
            _artistMusician = artistMusician;
            _album = album;
            _musician = musician;
            _song = song;
            _albumSong = albumSong;
        }

        public IActionResult Index(int id) //Artist.Id passed in
        { 
            //GET //Artist //Musician(s) //Album(s) //"Featured" Song(s)
            var artistInfoModel = _artist.Get(id);
            var musicianModel = _artistMusician.GetAllByArtist(id);
            var albumModel = _albumSong.GetAllAlbumsByArtist(id);
            var songModel = _albumSong.GetRandomSongsByArtist(id);

            //Load the data objects >>

            //if (songModel or albumModel != null) ** What to do if null? **

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

            var albums = albumModel.Select
               (r => new AlbumListingModel
               {
                   Id = r.Album.Id,
                   Name = r.Album.Name,
                   YrReleased = r.Album.YrReleased,
                   PrimaryImage = _image.GetPrimaryImageByEntity(1, r.Album.Id, 1)
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

            var model = new MainDetailModel
            {
                Artist = artistInfo,
                Musicians = musicians,
                Albums = albums,
                Songs = songs
            };

            return View(model);

        }
    }
}