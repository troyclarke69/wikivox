using System.ComponentModel.DataAnnotations;

namespace Wikivox.VModels
{
    public class SongListingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public string PrimaryImage { get; set; }
        [Display(Name = "Track #")]
        public int TrackNumber { get; set; }
    }
}
