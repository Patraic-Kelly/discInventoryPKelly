using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace DiscInventory.Models
{
    public partial class Rental
    {
        public int RentalId { get; set; }
        [Required(ErrorMessage = "Please enter date of rental.")]
        public DateTime BorrowDate { get; set; }
        [Required(ErrorMessage = "Please enter Due Date.")]
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        [Required(ErrorMessage = "Please select borrower.")]
        public int BorrowerId { get; set; }
        [Required(ErrorMessage = "Please select disc.")]
        public int DiscId { get; set; }

        public virtual Borrower Borrower { get; set; }
        public virtual Disc Disc { get; set; }
    }
}
