using System;
using System.Collections.Generic;
using System.Text;
using Wikivox.Models;

namespace Wikivox.Data.Interfaces
{
    public interface IArtistMusician
    {
        IEnumerable<ArtistMusician> GetAll();
        ArtistMusician Get(int id);
        IEnumerable<ArtistMusician> GetAllByArtist(int id);
        void Add(ArtistMusician newArtistMusician);
        void Update(ArtistMusician newArtistMusician);
        void Delete(int id);
    }
}
