using System;
using System.Collections.Generic;
using System.Text;
using Wikivox.Models;

namespace Wikivox.Data.Interfaces
{
    public interface IArtistSong
    {
        IEnumerable<ArtistSong> GetAll();
        ArtistSong Get(int id);
        void Add(ArtistSong newArtistSong);
        void Update(ArtistSong newArtistSong);
        void Delete(int id);
    }
}
