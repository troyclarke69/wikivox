using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox.Models
{
    public class ArtistMusician
    {
        public int Id { get; set; }
        public Artist Artist { get; set; }
        public Musician Musician { get; set; }
          
    }
}
