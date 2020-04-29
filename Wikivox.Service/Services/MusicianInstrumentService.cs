using System;
using System.Collections.Generic;
using System.Linq;
using Wikivox.Data;
using Wikivox.Data.Interfaces;
using Wikivox.Models;

namespace Wikivox.Service.Services
{
    public class MusicianInstrumentService : IMusicianInstrument
    {
        private readonly WikivoxDbContext _context;
        public MusicianInstrumentService(WikivoxDbContext context) { _context = context; }
        //public void Add(MusicianInstrument newMusicianInstrument)
        //{
        //    _context.Add(newMusicianInstrument);
        //    _context.SaveChanges();
        //}

        //public void Delete(int id)
        //{
        //    _context.MusicianInstrument.Where(g => g.Id == id)
        //       .ToList().ForEach(g => _context.MusicianInstrument.Remove(g));
        //    _context.SaveChanges();
        //}

        public void Add(int musicianId, int instrumentId)
        {
            var musician = _context.Musician.FirstOrDefault(g => g.Id == musicianId);
            var instrument = _context.Instrument.FirstOrDefault(g => g.Id == instrumentId);
           
            var newMusicianInstrument = new MusicianInstrument
            {
                Musician = musician,
                Instrument = instrument
            };

            _context.Add(newMusicianInstrument);
            _context.SaveChanges();
        }

        public void Delete(int musicianId, int instrumentId)
        {
            _context.MusicianInstrument.Where(a => a.Musician.Id == musicianId
                && a.Instrument.Id == instrumentId)
              .ToList().ForEach(a => _context.MusicianInstrument.Remove(a));

            _context.SaveChanges();
        }

        public MusicianInstrument Get(int id)
        {
            return GetAll().FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<MusicianInstrument> GetAll()
        {
            return _context.MusicianInstrument;
        }

        public bool IsMarked(int musicianId, int instrumentId)
        {
            var marked = _context.MusicianInstrument
                .Where(a => a.Musician.Id == musicianId && a.Instrument.Id == instrumentId);
            var result = false;
            if (marked.Any()) result = true;
            return result;
        }

        public void Update(MusicianInstrument newMusicianInstrument)
        {
            throw new NotImplementedException();
        }
    }
}
