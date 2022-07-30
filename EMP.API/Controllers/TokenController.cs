using EMP.LIB.Infrastructure;
using EMP.LIB.Models;
using EMP.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EMP.API.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private IRepository<Users> _users;

        private IConfiguration _configuration;

        public TokenController(IRepository<Users> users, IConfiguration configuration)
        {
            _users = users;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _users.Get(x => x.UserName == model.UserName && x.Password == model.Password);

                if (user != null && user.Count() == 1)
                {
                    var user_obj = user.FirstOrDefault();
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user_obj.Id.ToString()),
                        new Claim("DisplayName", user_obj.UserName),
                        new Claim("UserName", user_obj.UserName),
                        new Claim("Email", user_obj.UserName)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(5),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
