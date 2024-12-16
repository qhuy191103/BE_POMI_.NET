using Microsoft.AspNetCore.Mvc;
using restapi.Data;
using restapi.Models;
using Microsoft.EntityFrameworkCore;

namespace restapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductImageController : ControllerBase
    {
        private readonly ApplicationDBcontext _context;

        public ProductImageController(ApplicationDBcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductImage>>> GetProductImages()
        {
            return await _context.ProductImage.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductImage>> GetProductImage(int id)
        {
            var productImage = await _context.ProductImage.FindAsync(id);

            if (productImage == null)
            {
                return NotFound();
            }

            return productImage;
        }

        [HttpPost]
        public async Task<ActionResult<ProductImage>> CreateProductImage(ProductImage productImage)
        {
            _context.ProductImage.Add(productImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductImage), new { id = productImage.Id }, productImage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductImage(int id, ProductImage productImage)
        {
            if (id != productImage.Id)
            {
                return BadRequest();
            }

            _context.Entry(productImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ProductImage.Any(e => e.Id == id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            var productImage = await _context.ProductImage.FindAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }

            _context.ProductImage.Remove(productImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
