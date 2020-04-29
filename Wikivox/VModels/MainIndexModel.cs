using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox.VModels
{
    public class MainIndexModel
    {
        // ViewModel for Main/Index

        public IEnumerable<ArtistGenreModel> ArtistGenre { get; set; }
        public List<GenreListingModel> GenreListing { get; set; }
        public IEnumerable<ArtistListingModel> ArtistListing { get; set; }
        public ArtistListingModel ArtistInfo { get; set; }
    }
}
