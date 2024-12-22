using Microsoft.AspNetCore.Mvc;
using restapi.Data;
using restapi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Authorization;


namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDBcontext _context;

        public UsersController(ApplicationDBcontext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/users
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            // Kiểm tra xem người dùng đã tồn tại hay chưa
            var existingUser = await _context.User
                .FirstOrDefaultAsync(u => u.Email == user.Email || u.Phone == user.Phone);

            if (existingUser != null)
            {
                return BadRequest(new { message = "User with this email or phone already exists." });
            }

            // Không mã hóa mật khẩu, lưu trực tiếp
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User created successfully.", user });
        }


        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var existingUser = await _context.User.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;
            existingUser.Address = user.Address;

            if (!string.IsNullOrEmpty(user.Password))
            {
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] restapi.Models.LoginRequest loginRequest)
        {
            // Kiểm tra xem người dùng đã tồn tại hay chưa dựa trên email và mật khẩu
            var existingUser = await _context.User
                .FirstOrDefaultAsync(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password);

            if (existingUser == null)
            {
                return BadRequest(new { message = "Sai email hoặc mật khẩu" });
            }

            return Ok(new { message = "Đăng nhập thành công", user = existingUser });
        }
    }
}
