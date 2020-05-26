using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class BorrowHistoryModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Book")]
        public int BookId { get; set; }

        public virtual BookModel Book { get; set; }
        
        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        public virtual UserModel Customer { get; set; }

        [Display(Name = "Borrow Date")]
        public DateTime BorrowDate { get; set; }

        [Display(Name = "Return Date")]
        public DateTime? ReturnDate { get; set; }
    }
}
