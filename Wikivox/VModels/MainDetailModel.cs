using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox.VModels
{
    public class MainDetailModel
    {
        public ArtistListingModel Artist { get; set; }
        public List<AlbumListingModel> Albums { get; set; }
        public IEnumerable<MusicianListingModel> Musicians { get; set; }
        public IEnumerable<SongListingModel> Songs { get; set; }
    }
}
