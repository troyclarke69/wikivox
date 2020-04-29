using System.Collections.Generic;

namespace Wikivox.VModels
{
    public class AlbumInfoIndexModel
    {
        public ArtistListingModel Artist { get; set; }
        public AlbumListingModel Album { get; set; }
        public List<SongListingModel> Songs { get; set; }
    }
}
