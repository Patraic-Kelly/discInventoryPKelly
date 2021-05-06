using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiscInventory.Models
{
    public partial class MediaType
    {
        public MediaType()
        {
            Discs = new HashSet<Disc>();
        }
        public int MediaId { get; set; }
        public string MediaDesc { get; set; }

        public virtual ICollection<Disc> Discs { get; set; }
    }
}
