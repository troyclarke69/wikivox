using System.ComponentModel.DataAnnotations;
using Wikivox.Models;

namespace Wikivox.VModels
{
    public class AlbumListingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Rel.")]
        public int YrReleased { get; set; }

        public Artist Artist { get; set; }
        public string PrimaryImage { get; set; }
    }
}
