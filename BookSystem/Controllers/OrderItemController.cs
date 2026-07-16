using BookSystem.Data;
using BookSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OrderItemSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetAll()
        {
            return Ok(await _context.OrderItems.Include(b => b.Book).Include(b => b.Order).ToListAsync());


        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetById( int id)
        {
            var OrderItem = await _context.OrderItems.Include(oi => oi.Book).Include(oi => oi.Order).FirstOrDefaultAsync(oi => oi.Id == id);

            if (OrderItem == null)
                return NotFound("❌ OrderItem not found!");

            return Ok(OrderItem);

        }

        [HttpPost]
        public async Task<ActionResult<OrderItem>> Create(OrderItem OrderItem)
        {

            var bookExit = await _context.Books.AnyAsync(b => b.Id == OrderItem.BookId);
            if (!bookExit)
                return BadRequest("❌ Book not found!");


            var orderExit = await _context.Orders.AnyAsync(o => o.Id == OrderItem.OrderId);
            if (!orderExit)
                return BadRequest("❌ Order not found!");


            _context.OrderItems.Add(OrderItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = OrderItem.Id }, OrderItem);
        
        }

        // ✅ Update: PUT api/OrderItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderItem updatedOrderItem)
        {

            if (id != updatedOrderItem.Id)
                return BadRequest("❌ ID mismatch!");

            var bookExit = await _context.Books.AnyAsync(b => b.Id == updatedOrderItem.BookId);
            if (!bookExit)
                return BadRequest("❌ Book not found!");


            var orderExit = await _context.Orders.AnyAsync(o => o.Id == updatedOrderItem.OrderId);
            if (!orderExit)
                return BadRequest("❌ Order not found!");


            _context.Entry(updatedOrderItem).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var OrderItem = await _context.OrderItems.FindAsync(id);

            if (OrderItem == null)
                return NotFound("❌ OrderItem not found!");

            _context.OrderItems.Remove(OrderItem);
            await _context.SaveChangesAsync();
            return NoContent();
        
        
        }
    
    }

}
