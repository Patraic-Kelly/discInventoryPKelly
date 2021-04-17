using System;
using System.Collections.Generic;

#nullable disable

namespace DiscInventory.Models
{
    public partial class Rental
    {
        public int RentalId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int BorrowerId { get; set; }
        public int DiscId { get; set; }

        public virtual Borrower Borrower { get; set; }
        public virtual Disc Disc { get; set; }
    }
}
