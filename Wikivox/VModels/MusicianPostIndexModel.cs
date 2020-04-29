using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox.Models;

namespace Wikivox.VModels
{
    public class MusicianPostIndexModel
    {
        public ArtistListingModel Artist { get; set; }
        public MusicianListingModel Musician { get; set; }
        public IEnumerable<MusicianListingModel> Musicians { get; set; }
        public List<InstrumentListingModel> Instruments { get; set; }
    }
}
