using System.Collections.Generic;
using Wikivox.Models;

namespace Wikivox.Data.Interfaces
{
    public interface IArtist
    {
        IEnumerable<Artist> GetAll();
        Artist Get(int id);
        //IEnumerable<Artist> GetArtistsByGenre(int id);
        void Add(Artist newArtist);
        void Update(Artist newArtist);
        void Delete(int id);
    }
}
