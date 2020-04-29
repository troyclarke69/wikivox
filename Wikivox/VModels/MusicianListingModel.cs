using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox.VModels
{
    public class MusicianListingModel
    {
        public int Id { get; set; }
        [Display(Name = "Showbiz Name")]
        public string MusicianName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public string Bio { get; set; }
        public DateTime Birth { get; set; }
        public DateTime Death { get; set; }
        [Display(Name = "Active")]
        public int isActive { get; set; }
        [Display(Name = "Home Town")]
        public string HomeTown { get; set; }
        [Display(Name = "Home Country")]
        public string HomeCountry { get; set; }
        public string PrimaryImage { get; set; }
    }
}
