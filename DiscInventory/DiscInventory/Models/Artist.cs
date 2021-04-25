using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Please enter artist name.")]
        public string ArtistName { get; set; }
        public string ArtistLastName { get; set; }
        [Required(ErrorMessage = "Please select a type.")]
        public int ArtistTypeId { get; set; }

        public virtual ArtistType ArtistType { get; set; }
        public virtual ICollection<DiscHasArtist> DiscHasArtists { get; set; }
    }
}
