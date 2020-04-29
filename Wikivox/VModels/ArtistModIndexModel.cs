using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox.VModels
{
    public class ArtistModIndexModel
    {
        public ArtistListingModel Artist { get; set; }
        public List<GenreListingModel> Genres { get; set; }
        public IEnumerable<MusicianListingModel> Musicians { get; set; }
    }
}
