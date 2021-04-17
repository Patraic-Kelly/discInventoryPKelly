using System;
using System.Collections.Generic;

#nullable disable

namespace DiscInventory.Models
{
    public partial class Disc
    {
        public Disc()
        {
            DiscHasArtists = new HashSet<DiscHasArtist>();
            Rentals = new HashSet<Rental>();
        }

        public int DiscId { get; set; }
        public string DiscName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int StatusId { get; set; }
        public int MediaId { get; set; }
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual MediaType Media { get; set; }
        public virtual DiscStatus Status { get; set; }
        public virtual ICollection<DiscHasArtist> DiscHasArtists { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
