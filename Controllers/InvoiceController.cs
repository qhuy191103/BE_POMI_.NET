using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restapi.Data;
using restapi.Models; // Đảm bảo bạn import đúng namespace chứa model
using System.Linq;
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

        // GET: api/Invoice/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoicesByUser(int userId)
        {
            var invoices = await _context.Invoices
                .Where(i => i.UserId == userId)
                .Include(i => i.InvoiceDetails)
                .ToListAsync();

            if (invoices == null || !invoices.Any())
            {
                return NotFound("Không tìm thấy hóa đơn.");
            }

            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.InvoiceDetails)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return NotFound("Hóa đơn không tồn tại.");
            }

            return Ok(invoice);
        }


        // POST: api/Invoice
        // Thêm mới hóa đơn
        [HttpPost]
        public async Task<ActionResult<Invoice>> CreateInvoice([FromBody] Invoice invoice)
        {
            if (invoice == null)
            {
                return BadRequest("Dữ liệu hóa đơn không hợp lệ.");
            }

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, invoice);
        }

        // PUT: api/Invoice/{id}
        // Cập nhật hóa đơn
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, [FromBody] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest("ID không hợp lệ.");
            }

            _context.Entry(invoice).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
                {
                    return NotFound("Hóa đơn không tồn tại.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Invoice/{id}
        // Xóa hóa đơn
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound("Hóa đơn không tồn tại.");
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Kiểm tra xem hóa đơn có tồn tại không
        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }
    }
}
