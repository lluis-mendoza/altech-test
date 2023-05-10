using System.Text;
using AltechTest.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace AltechTest.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    [HttpPost, Route("login")]
    public IActionResult Login(LoginDto user)
    {
        if (user == null)
        {
            return BadRequest("Invalid client request");
        }
        if (user.UserName == "admin" && user.Password == "admin")
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nn5Dds7TYzsGSnCgSTenEUXnBZKIOxjA"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "Altech",
                audience: "https://localhost:5213",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { Token = tokenString });
        }
        else
        {
            return Unauthorized();
        }
    }
}