using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox.Models
{
    public class Artist
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string ArtistName { get; set; }
        public string Bio { get; set; }
        [Display(Name = "Year Formed")]
        public int YrFormed { get; set; }
        [Display(Name = "Year Ended")]
        public int YrEnded { get; set; }
        [Display(Name = "Active")]
        public int isActive { get; set; }
        [Display(Name = "Home Town")]
        public string HomeTown { get; set; }
        [Display(Name = "Home Country")]
        public string HomeCountry { get; set; }

        
    }
}
