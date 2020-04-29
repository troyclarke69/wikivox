using System.Collections.Generic;
using Wikivox.Models;

namespace Wikivox.Data.Interfaces
{
    public interface IGenre
    {
        IEnumerable<Genre> GetAll();
        Genre Get(int id);
        void Add(Genre newGenre);
        void Update(Genre newGenre);
        void Delete(int id);

    }
}
