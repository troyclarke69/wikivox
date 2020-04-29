using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox.Models
{
    public class Musician
    {
        public int Id { get; set; }
        public string MusicianName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Bio { get; set; }
        public DateTime Birth { get; set; }
        public DateTime Death { get; set; }
        public int isActive { get; set; }
        public string HomeTown { get; set; }
        public string HomeCountry { get; set; }
    }
}
