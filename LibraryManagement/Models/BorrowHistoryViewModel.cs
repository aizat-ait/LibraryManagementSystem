using System;

namespace LibraryManagement.Models
{
    public class BorrowHistoryViewModel
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        
        public int CustomerId { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
