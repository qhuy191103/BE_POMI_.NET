using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Đảm bảo rằng bạn đang sử dụng EF Core
using restapi.Data;
using restapi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly ApplicationDBcontext _context;

        public InvoiceController(ApplicationDBcontext context)
        {
            _context = context;
        }

        // GET: api/invoice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _context.Invoices.ToListAsync();
        }

        // GET: api/invoice/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        // POST: api/invoice
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, invoice);
        }
    }
}
