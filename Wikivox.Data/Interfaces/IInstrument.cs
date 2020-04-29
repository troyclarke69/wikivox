using System;
using System.Collections.Generic;
using System.Text;
using Wikivox.Models;

namespace Wikivox.Data.Interfaces
{
    public interface IInstrument
    {
        IEnumerable<Instrument> GetAll();
        Instrument Get(int id);
        void Add(Instrument newInstrument);
        void Update(Instrument newInstrument);
        void Delete(int id);
    }
}
