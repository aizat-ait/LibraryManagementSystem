using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string SerialNumber { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public bool IsAvailable { get; set; }
    }
}
