using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox.VModels
{
    public class ArtistListingModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string ArtistName { get; set; }
        //public string LastName { get; set; }
        //public string FirstName { get; set; }
        public string Bio { get; set; }
        [Display(Name = "Year Formed")]
        public int YrFormed { get; set; }
        [Display(Name = "Year Ended")]
        public int YrEnded { get; set; }
        public int isActive { get; set; }
        [Display(Name = "Home Town")]
        public string HomeTown { get; set; }
        [Display(Name = "Home Country")]
        public string HomeCountry { get; set; }
        public string PrimaryImage { get; set; }
    }
}
