using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox.Models
{
    public class ArtistGenre
    {
        public int Id { get; set; }
        public Artist Artist { get; set; }
        public Genre Genre { get; set; }
    }
}
