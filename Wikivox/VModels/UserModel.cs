using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Wikivox.VModels
{
    public class UserModel
    {
        public int[] SelectedSongIds { get; set; }
        public IEnumerable<SelectListItem> SongList { get; set; }

    }
}
