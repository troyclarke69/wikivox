﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox.Models
{
    public class AlbumSong
    {
        public int Id { get; set; }
        public Album Album { get; set; }
        public Song Song { get; set; }
        public int TrackNumber { get; set; }
        public Artist Artist { get; set; }
    }
}
