using System.Collections.Generic;
using Wikivox.Models;

namespace Wikivox.Data.Interfaces
{
    public interface IAlbum
    {
        IEnumerable<Album> GetAll();
        IEnumerable<Album> GetAlbumsByArtist(int id);
        Album Get(int id);
        void Add(Album newAlbum);
        void Update(Album newAlbum);
        void Delete(int id);
    }
}
