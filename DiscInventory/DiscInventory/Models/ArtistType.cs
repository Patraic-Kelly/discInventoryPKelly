using System;
using System.Collections.Generic;

#nullable disable

namespace DiscInventory.Models
{
    public partial class ArtistType
    {
        public ArtistType()
        {
            Artists = new HashSet<Artist>();
        }

        public int ArtistTypeId { get; set; }
        public string ArtistTypeDesc { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
    }
}
