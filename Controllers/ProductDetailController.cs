using Microsoft.AspNetCore.Mvc;
using restapi.Data;
using restapi.Models;
using Microsoft.EntityFrameworkCore;

namespace restapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductDetailController : ControllerBase
    {
        private readonly ApplicationDBcontext _context;

        public ProductDetailController(ApplicationDBcontext context)
        {
            _context = context;
        }

        // GET: api/ProductDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDetail>>> GetProductDetails()
        {
            return await _context.ProductDetail.ToListAsync();
        }

        // GET: api/ProductDetail/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetail>> GetProductDetail(int id)
        {
            var productDetail = await _context.ProductDetail
                .Include(pd => pd.Product)
                .FirstOrDefaultAsync(pd => pd.ProductId == id);

            if (productDetail == null)
            {
                return NotFound();
            }

            return productDetail;
        }

        // POST: api/ProductDetail
        [HttpPost]
        public async Task<ActionResult<ProductDetail>> CreateProductDetail(ProductDetail productDetail)
        {
            _context.ProductDetail.Add(productDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductDetail), new { id = productDetail.ProductId }, productDetail);
        }

        // PUT: api/ProductDetail/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductDetail(int id, ProductDetail productDetail)
        {
            if (id != productDetail.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(productDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ProductDetail.Any(e => e.ProductId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ProductDetail/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductDetail(int id)
        {
            var productDetail = await _context.ProductDetail
                .Include(pd => pd.Product)
                .FirstOrDefaultAsync(pd => pd.ProductId == id);

            if (productDetail == null)
            {
                return NotFound();
            }

            _context.ProductDetail.Remove(productDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

