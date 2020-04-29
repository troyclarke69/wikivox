using System;
using System.Collections.Generic;
using System.Linq;
using Wikivox.Data;
using Wikivox.Data.Interfaces;
using Wikivox.Models;

namespace Wikivox.Service.Services
{
    public class InstrumentService : IInstrument
    {
        private readonly WikivoxDbContext _context;
        public InstrumentService(WikivoxDbContext context) { _context = context; }
        public void Add(Instrument newInstrument)
        {
            _context.Add(newInstrument);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Instrument.Where(g => g.Id == id)
               .ToList().ForEach(g => _context.Instrument.Remove(g));
            _context.SaveChanges();
        }

        public Instrument Get(int id)
        {
            return GetAll().FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Instrument> GetAll()
        {
            return _context.Instrument;
        }

        public void Update(Instrument newInstrument)
        {
            throw new NotImplementedException();
        }
    }
}
