using System;
using System.Collections.Generic;

#nullable disable

namespace DiscInventory.Models
{
    public partial class Artist
    {
        public Artist()
        {
            DiscHasArtists = new HashSet<DiscHasArtist>();
        }

        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string ArtistLastName { get; set; }
        public int ArtistTypeId { get; set; }

        public virtual ArtistType ArtistType { get; set; }
        public virtual ICollection<DiscHasArtist> DiscHasArtists { get; set; }
    }
}
