using Microsoft.AspNetCore.Mvc;
using restapi.Data;
using restapi.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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

        // GET: api/invoicedetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceDetail>>> GetInvoiceDetails()
        {
            return await _context.InvoiceDetails.ToListAsync();
        }

        // GET: api/invoicedetail/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDetail>> GetInvoiceDetail(int id)
        {
            var invoiceDetail = await _context.InvoiceDetails.FindAsync(id);

            if (invoiceDetail == null)
            {
                return NotFound();
            }

            return invoiceDetail;
        }

        // POST: api/invoicedetail
        [HttpPost]
        public async Task<ActionResult<InvoiceDetail>> PostInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            _context.InvoiceDetails.Add(invoiceDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvoiceDetail), new { id = invoiceDetail.Id }, invoiceDetail);
        }
    }
}