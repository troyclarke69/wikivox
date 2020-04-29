using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wikivox.Models;

namespace Wikivox.VModels
{
    public class ArtistGenreModel
    {
        public int Id { get; set; }
        public Artist Artist { get; set; }
        public Genre Genre { get; set; }
    }
}
