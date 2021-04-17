using System;
using System.Collections.Generic;

#nullable disable

namespace DiscInventory.Models
{
    public partial class Borrower
    {
        public Borrower()
        {
            Rentals = new HashSet<Rental>();
        }

        public int BorrowerId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
