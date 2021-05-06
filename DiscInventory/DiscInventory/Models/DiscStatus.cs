using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiscInventory.Models
{
    public partial class DiscStatus
    {
        public DiscStatus()
        {
            Discs = new HashSet<Disc>();
        }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }

        public virtual ICollection<Disc> Discs { get; set; }
    }
}
