using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryManagementDBContext _context;

        public BookController(LibraryManagementDBContext context)
        {
            _context = context;
        }

        // POST: api/Book
        [HttpPost]
        public IActionResult AddBook(BookViewModel b)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var book = new BookModel
            {
                Author = b.Author,
                Publisher = b.Publisher,
                SerialNumber = b.SerialNumber,
                Title = b.Title,
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            return Ok(book);
        }

        // GET: api/Book
        [HttpGet]
        public ActionResult<IEnumerable<BookViewModel>> GetBooks()
        {
            var books = _context.Books.Include(h => h.BorrowHistories)
                .Select(b => new BookViewModel
                {
                    Id = b.Id,
                    Author = b.Author,
                    Publisher = b.Publisher,
                    SerialNumber = b.SerialNumber,
                    Title = b.Title,
                    IsAvailable = !b.BorrowHistories.Any(h => h.ReturnDate == null)
                }).ToList();

            if (books == null)
            {
                return NotFound();
            }

            return Ok(books);
        }

        // GET: api/Book/:id
        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var b = GetById(id);

            if (b == null)
            {
                return NotFound();
            }

            var book = new BookViewModel
            {
                Id = b.Id,
                Author = b.Author,
                Publisher = b.Publisher,
                SerialNumber = b.SerialNumber,
                Title = b.Title
            };
            
            if(b.BorrowHistories != null)
            {
                book.IsAvailable = !b.BorrowHistories.Any(h => h.ReturnDate == null);
            }

            return Ok(book);
        }

        [HttpPost("Edit")]
        public IActionResult Edit([FromBody]BookViewModel b)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            var existing = GetById(b.Id);

            if (existing != null)
            {
                existing.Id = b.Id;
                existing.Title = b.Title;
                existing.SerialNumber = b.SerialNumber;
                existing.Author = b.Author;
                existing.Publisher = b.Publisher;

                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        // DELETE: api/Book/id
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();

            return Ok();
        }

        private BookModel GetById(int id)
        {
            return _context.Books.FirstOrDefault(u => u.Id == id);
        }
    }
}
