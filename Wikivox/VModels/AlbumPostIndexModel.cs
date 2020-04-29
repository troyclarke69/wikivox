using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox.VModels
{
    public class AlbumPostIndexModel
    {
        public ArtistListingModel Artist { get; set; }
        public AlbumListingModel Album { get; set; }

        //public List<AlbumListingModel> Albums { get; set; }
        //public List<SongListingModel> Songs { get; set; }
    }
}
