using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restapi.Models;
using System.Linq;
using System.Threading.Tasks;
using restapi.Models;
using restapi.Data;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly ApplicationDBcontext _context;

        public InvoiceDetailController(ApplicationDBcontext context)
        {
            _context = context;
        }

        // GET: api/InvoiceDetail/{invoiceId}
        // Lấy tất cả chi tiết hóa đơn theo ID hóa đơn
        [HttpGet("{invoiceId}")]
        public async Task<ActionResult<IEnumerable<InvoiceDetail>>> GetInvoiceDetails(int invoiceId)
        {
            var invoiceDetails = await _context.InvoiceDetails
                .Where(d => d.InvoiceId == invoiceId)
                .ToListAsync();

            if (invoiceDetails == null || !invoiceDetails.Any())
            {
                return NotFound("Không tìm thấy chi tiết hóa đơn.");
            }

            return Ok(invoiceDetails);
        }

        // POST: api/InvoiceDetail
        // Thêm mới chi tiết hóa đơn
        [HttpPost]
        public async Task<ActionResult<InvoiceDetail>> CreateInvoiceDetail([FromBody] InvoiceDetail invoiceDetail)
        {
            if (invoiceDetail == null)
            {
                return BadRequest("Dữ liệu chi tiết hóa đơn không hợp lệ.");
            }

            _context.InvoiceDetails.Add(invoiceDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvoiceDetails), new { invoiceId = invoiceDetail.InvoiceId }, invoiceDetail);
        }

        // DELETE: api/InvoiceDetail/{id}
        // Xóa chi tiết hóa đơn
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceDetail(int id)
        {
            var invoiceDetail = await _context.InvoiceDetails.FindAsync(id);
            if (invoiceDetail == null)
            {
                return NotFound("Chi tiết hóa đơn không tồn tại.");
            }

            _context.InvoiceDetails.Remove(invoiceDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
