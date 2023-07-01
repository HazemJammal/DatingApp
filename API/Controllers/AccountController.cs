using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTO;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _token;
        public AccountController(DataContext context, ITokenService token)
        {
            this._token= token;
            this._context = context;

        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO dto)
        {
            dto.Username = dto.Username.ToLower();
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == dto.Username);

            if (user == null) return Unauthorized("Username doesnt exist");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

            for (int i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized();

            return new UserDTO{
                Username = user.Username,
                Token = _token.CreateToken(user)
            };

        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO dto)
        {

            if (await CheckUser(dto.Username))
            {
                return BadRequest("Username Taken");
            }


            using var hmac = new HMACSHA512();
            AppUser user = new AppUser
            {
                Username = dto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDTO{
                Username = user.Username,
                Token = _token.CreateToken(user)
            };
        }

        public async Task<bool> CheckUser(string name)
        {
            return await _context.Users.AnyAsync(x => x.Username == name.ToLower());
        }

    }
}