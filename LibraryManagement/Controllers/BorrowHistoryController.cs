using LibraryManagement.Common;
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
    public class BorrowHistoryController : ControllerBase
    {
        private readonly LibraryManagementDBContext _context;

        public BorrowHistoryController(LibraryManagementDBContext context)
        {
            _context = context;
        }

        // POST: api/BorrowHistory
        [HttpPost]
        public IActionResult AddBorrowHistory(BorrowHistoryViewModel b)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var now = Utility.GetTurkeyCurrentDateTime();

            var borrowHistory = new BorrowHistoryModel
            {
                Id = b.Id,
                BookId = b.BookId,
                CustomerId = b.CustomerId,
                BorrowDate = now,
                ReturnDate = b.ReturnDate
            };

            _context.BorrowHistories.Add(borrowHistory);
            _context.SaveChanges();

            return Ok(borrowHistory);
        }

        // GET: api/BorrowHistory
        [HttpGet]
        public ActionResult<IEnumerable<BorrowHistoryViewModel>> GetBorrowHistories()
        {
            var borrowHistories = _context.BorrowHistories.Select(b => new BorrowHistoryViewModel
            {
                Id = b.Id,
                BookId = b.BookId,
                CustomerId = b.CustomerId,
                BorrowDate = b.BorrowDate,
                ReturnDate = b.ReturnDate
            }).ToList();

            if(borrowHistories == null)
            {
                return NotFound();
            }

            return Ok(borrowHistories);
        }

        // GET: api/BorrowHistory/:id
        [HttpGet("{id}")]
        public IActionResult GetBorrowHistory(int id)
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

            var borrowHistory = new BorrowHistoryViewModel
            {
                Id = b.Id,
                BookId = b.BookId,
                CustomerId = b.CustomerId,
                BorrowDate = b.BorrowDate,
                ReturnDate = b.ReturnDate
            };

            return Ok(borrowHistory);
        }
               
        [HttpPost("Edit")]
        public IActionResult Edit([FromBody]BorrowHistoryViewModel b)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            var existing = GetById(b.Id);

            if (existing != null)
            {
                existing.Id = b.Id;
                existing.BookId = b.BookId;
                existing.CustomerId = b.CustomerId;
                existing.BorrowDate = b.BorrowDate;
                existing.ReturnDate = b.ReturnDate;

                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        // DELETE: api/BorrowHistory/:id
        [HttpDelete("{id}")]
        public IActionResult DeleteBorrowHistory(int id)
        {
            if (id <= 0)
            {
                return BadRequest("");
            }

            var borrowHistoryModel = GetById(id);
            if (borrowHistoryModel == null)
            {
                return NotFound();
            }

            _context.BorrowHistories.Remove(borrowHistoryModel);
            _context.SaveChanges();

            return Ok();
        }

        private BorrowHistoryModel GetById(int id)
        {
            return _context.BorrowHistories.FirstOrDefault(u => u.Id == id);
        }
    }
}
