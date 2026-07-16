using BookSystem.Data;
using BookSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OrderSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            return Ok(await _context.Orders.Include(o=>o.Customer).Include(o=>o.OrderItems).ThenInclude(b=>b.Book) .ToListAsync());


        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            var order = await _context.Orders.Include(o => o.Customer).Include(o => o.OrderItems).ThenInclude(b => b.Book).FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound("❌ Order not found!");

            return Ok(order);

        }
        

        [HttpPost]
        public async Task<ActionResult<Order>> Create(Order order)
        {

            order.OrderDate = DateTime.Now;
            var CustomerExit = await _context.Customers.AnyAsync(c => c.Id == order.CustomerId);
            if (!CustomerExit)
                return BadRequest("❌ Customer not found!");
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        
        }

        // ✅ Update: PUT api/Order/5
        [HttpPut("{id}")]  
        public async Task<IActionResult> Update(int id, Order updatedOrder)
        {

            if (id != updatedOrder.Id)
                return BadRequest("❌ ID mismatch!");

            var CustomerExit = await _context.Customers.AnyAsync(c => c.Id == updatedOrder.CustomerId);
            if (!CustomerExit)
                return BadRequest("❌ Customer not found!");


            _context.Entry(updatedOrder).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Order = await _context.Orders.FindAsync(id);

            if (Order == null)
                return NotFound("❌ Order not found!");

            _context.Orders.Remove(Order);
            await _context.SaveChangesAsync();
            return NoContent();
        
        }



    }

}
