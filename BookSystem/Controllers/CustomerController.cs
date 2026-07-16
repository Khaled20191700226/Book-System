using BookSystem.Data;
using BookSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public CustomerController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Customers.ToListAsync());
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return NotFound();

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById),
            new { id = customer.Id },
            customer);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Customer updatedCustomer)
    {
        if (id != updatedCustomer.Id) return BadRequest();

        _context.Entry(updatedCustomer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();

    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return NotFound();

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
