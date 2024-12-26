using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restapi.Models;
using restapi.Data;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.IdentityModel.Tokens;
using restapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ApplicationDBcontext _context;

    public CartController(ApplicationDBcontext context)
    {
        _context = context;
    }

    [HttpPost("AddToCart")]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
    {
        // Tìm sản phẩm
        var product = await _context.Product.FindAsync(request.ProductId);
        if (product == null)
        {
            return NotFound(new AddToCartResponse
            {
                Value = 0,
                Message = "Sản phẩm không tồn tại.",
                CalculatedPrice = 0
            });
        }

        // Kiểm tra danh sách kích thước sản phẩm
        // Kiểm tra danh sách kích thước sản phẩm
        if (product.Sizes == null || product.Sizes.Count == 0)
        {
            return BadRequest(new AddToCartResponse
            {
                Value = 0,
                Message = "Sản phẩm không có kích thước hợp lệ.",
                CalculatedPrice = 0
            });
        }

        // Convert List<string> to array before using Array.IndexOf
        var sizes = product.Sizes.ToArray();
        int sizeIndex = Array.IndexOf(sizes, request.SelectedSize);

        // Nếu kích thước không hợp lệ, trả về lỗi
        if (sizeIndex == -1)
        {
            return BadRequest(new AddToCartResponse
            {
                Value = 0,
                Message = "Kích thước không hợp lệ.",
                CalculatedPrice = 0
            });
        }

        // Tính giá tăng thêm cho kích thước
        decimal sizePrice = sizeIndex * 5000;

        // Tính giá tổng
        decimal totalPrice = (product.Price + sizePrice) * request.Quantity;

        // Tạo đối tượng Cart mới
        var newCartItem = new Cart
        {
            UserId = request.UserId,
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            Price = totalPrice, // Giá đã tính toán
            DateAdded = DateTime.Now,
            SelectedSize = request.SelectedSize,
            SelectedSugar = request.SelectedSugar,
            SelectedIce = request.SelectedIce,
            Note = request.Note
        };

        // Lưu vào cơ sở dữ liệu
        _context.Carts.Add(newCartItem);
        await _context.SaveChangesAsync();

        // Trả về phản hồi
        return Ok(new AddToCartResponse
        {
            Value = 1,
            Message = "Sản phẩm đã được thêm vào giỏ hàng thành công.",
            CalculatedPrice = totalPrice
        });
    }

    [HttpGet("GetCart")]
    public async Task<IActionResult> GetCart(int userId)
    {
        var cartItems = await _context.Carts
            .Where(c => c.UserId == userId)
            .Include(c => c.Product)
            .ToListAsync();

        if (cartItems == null || cartItems.Count == 0)
        {
            return NotFound(new { value = 0, message = "Giỏ hàng trống." });
        }

        var response = cartItems.Select(c => new
        {
            c.CartId,
            c.ProductId,
            ProductName = c.Product.Name,
            c.Quantity,
            c.Price,
            c.DateAdded,
            c.SelectedSize,
            c.SelectedSugar,
            c.SelectedIce,
            c.Note
        });

        return Ok(new { value = 1, data = response });
    }

    [HttpDelete("DeleteFromCart")]
    public async Task<IActionResult> DeleteFromCart(int userId, int CartId)
    {
        var cartItem = await _context.Carts
            .SingleOrDefaultAsync(c => c.UserId == userId && c.CartId == CartId);

        if (cartItem == null)
        {
            return NotFound(new { value = 0, message = "Sản phẩm không tồn tại trong giỏ hàng." });
        }

        _context.Carts.Remove(cartItem);
        await _context.SaveChangesAsync();

        return Ok(new { value = 1, message = "Sản phẩm đã được xóa khỏi giỏ hàng." });
    }
}
