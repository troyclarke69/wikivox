using System.Collections.Generic;

namespace Wikivox.VModels
{
    public class ArtistPostIndexModel
    {
        public ArtistListingModel Artist { get; set; }
        public List<GenreListingModel> Genres { get; set; }
        public List<MusicianListingModel> Musicians { get; set; }
    }
}
