using System;
using System.Collections.Generic;
using System.Text;
using Wikivox.Models;

namespace Wikivox.Data.Interfaces
{
    public interface IMusician
    {
        IEnumerable<Musician> GetAll();
        Musician Get(int id);
        IEnumerable<Musician> GetAllByArtist(int id);
        IEnumerable<Musician> GetAllByInstrument(int id);
        void Add(Musician newMusician);
        void Update(Musician newMusician);
        void Delete(int id);
    }
}
