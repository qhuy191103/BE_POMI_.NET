using Microsoft.AspNetCore.Mvc;
using restapi.Data;
using restapi.Models;
using Microsoft.EntityFrameworkCore;    

namespace restapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDBcontext _context;

        public ProductController(ApplicationDBcontext context)
        {
            _context = context;
        }

        // GET: api/Product

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Product
                                 .Where(p => !p.IsDeleted) // Lọc sản phẩm chưa bị xóa
                                 .Include(p => p.Category)
                                 .ToListAsync();
        }


        // GET: api/Product/{id}
        [HttpGet("{id}")]     
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product
                                        .Include(p => p.Category)
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            var savedProduct = await _context.Product
                                             .Include(p => p.Category)  
                                             .FirstOrDefaultAsync(p => p.Id == product.Id);

            return CreatedAtAction(nameof(GetProduct), new { id = savedProduct.Id }, savedProduct);
        }


        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Product.Any(e => e.Id == id))
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

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            product.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
