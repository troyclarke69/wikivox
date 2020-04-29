using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox.Models
{
    public class MusicianInstrument
    {
        public int Id { get; set; }
        public Musician Musician { get; set; }
        public Instrument Instrument { get; set; }
    }
}
