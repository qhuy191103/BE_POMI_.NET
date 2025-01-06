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

        // Kiểm tra kích thước hợp lệ
        var sizes = product.Sizes.ToArray();
        int sizeIndex = Array.IndexOf(sizes, request.SelectedSize);
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
        decimal totalPrice = (product.Price + sizePrice) * request.Quantity;

        // Tạo đối tượng Cart mới và lưu thông tin vị trí
        var newCartItem = new Cart
        {
            UserId = request.UserId,
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            Price = totalPrice,
            DateAdded = DateTime.Now,
            SelectedSize = request.SelectedSize,
            SelectedSugar = request.SelectedSugar,
            SelectedIce = request.SelectedIce,
            Note = request.Note,
           
        };

        // Lưu vào cơ sở dữ liệu
        _context.Carts.Add(newCartItem);
        await _context.SaveChangesAsync();

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
            ProductImage = c.Product.ImageUrl,
            c.Quantity,
            c.Price,
            c.DateAdded,
            c.SelectedSize,
            c.SelectedSugar,
            c.SelectedIce,
            c.Note,
          
        });

        return Ok(new { value = 1, data = response });
    }

    [HttpGet("GetCartTotalPrice")]
    public async Task<IActionResult> GetCartTotalPrice(int userId)
    {
        // Lấy tất cả sản phẩm trong giỏ hàng của người dùng
        var cartItems = await _context.Carts
            .Where(c => c.UserId == userId)
            .ToListAsync();

        // Kiểm tra nếu giỏ hàng rỗng
        if (cartItems == null || cartItems.Count == 0)
        {
            return Ok(new { value = 0, message = "Giỏ hàng trống.", totalPrice = 0 });
        }

        // Tính tổng giá tiền
        decimal totalPrice = cartItems.Sum(c => c.Price);

        // Trả về tổng giá tiền
        return Ok(new { value = 1, message = "Tính tổng giá thành công.", totalPrice = totalPrice });
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
