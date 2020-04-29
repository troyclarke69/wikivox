using Wikivox.Models;

namespace Wikivox.VModels
{
    public class ImageListingModel
    {
        public int Id { get; set; }
        public Entity Entity { get; set; } //ex. 1 = Album, 2 = Musician, 3 = Album, 4 = Song, 5 = Genre
        public int ResourceId { get; set; } //ex. Id of Album, etc
        public string ImgUrl { get; set; }
    }
}
