using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookSystem.Data;
using BookSystem.Models;

namespace BookSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //primay constructor
    public class BookController(ApplicationDbContext _context) : ControllerBase
    {
        

        // ✅ Get: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            return Ok(await _context.Books.ToListAsync());
        }

        // ✅ Get by id: api/Book/5
        [HttpGet("{id}")]
        
        public async Task<ActionResult<Book>> GetById(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return NotFound("❌ Book not found!");

            return Ok(book);
        }

        // ✅ Create: POST api/Book
        [HttpPost]
        public async Task<ActionResult<Book>> Create(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        // ✅ Update: PUT api/Book/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Book updatedBook)
        {
            if (id != updatedBook.Id)
                return BadRequest("❌ ID mismatch!");

            _context.Entry(updatedBook).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ✅ Delete: DELETE api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return NotFound("❌ Book not found!");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
