using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Please enter a first name.")]
        public string FName { get; set; }
        [Required(ErrorMessage = "Please enter a Last Name.")]
        public string LName { get; set; }
        [Required(ErrorMessage = "Please enter a phone number.")]
        [Phone(ErrorMessage = "Please enter a valid phone number 111-111-1111")]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid Email")]
        public string Email { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
