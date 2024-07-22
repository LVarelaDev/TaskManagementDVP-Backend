using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Business.IRepository.Authentication;
using TaskManagement.DataAccess.Context;
using TaskManagement.DataAccess.Entities;
using TaskManagement.Models.Models.DTO;
using TaskManagement.Models.Models.Responses;

namespace TaskManagement.DataAccess.Repository.AuthenticationRepository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        
        public AuthenticationRepository(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }


        public string GenerateToken()
        {
            string SecretKey = _configuration["Jwt:Key"] ?? "";

            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey ?? ""));

            var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"] ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResponse> Login(LoginDTO payload)
        {
            User? user = await _context.Users.Where(x => x.Username == payload.Username && x.Password == payload.Password).FirstOrDefaultAsync();

            if(user == null)
            {
                return new LoginResponse
                {
                    Token = "",
                    User = null
                };
            }

            string token = GenerateToken();

            return new LoginResponse
            {
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email,
                    RolId = user.RolId,
                }
            };
        }
    }
}
