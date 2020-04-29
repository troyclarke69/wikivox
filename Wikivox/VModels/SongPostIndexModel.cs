using System.Collections.Generic;
using Wikivox.Models;

namespace Wikivox.VModels
{
    public class SongPostIndexModel
    {
        //Relationships: Artist 1 : 1 Album 1 : M Songs. Song to create new
        public ArtistListingModel Artist { get; set; }
        public AlbumListingModel Album { get; set; }
        public SongListingModel Song { get; set; }
        public IEnumerable<SongListingModel> Songs { get; set; }
    }
}
