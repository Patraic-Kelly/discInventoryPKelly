using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Please enter a name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be 2 characters or more.")]
        public string DiscName { get; set; }
        [Required(ErrorMessage ="Please enter a date.")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Please select a status.")]
        public int? StatusId { get; set; }
        [Required(ErrorMessage = "Please select a Media Type.")]
        public int? MediaId { get; set; }
        [Required(ErrorMessage = "Please select a Genre.")]
        public int? GenreId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual MediaType Media { get; set; }
        public virtual DiscStatus Status { get; set; }
        public virtual ICollection<DiscHasArtist> DiscHasArtists { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
