using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using BM7_Backend.Dto;
using BM7_Backend.Interfaces;
using BM7_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BM7_Backend.Controllers;

public class AuthController : Controller
{
    private readonly UserInterface _userInterface;
    private readonly IConfiguration _configuration;
    
    public AuthController(UserInterface userInterface, IConfiguration configuration)
    {
        _userInterface = userInterface;
        _configuration = configuration;
    }

    [HttpPost("login")]
    [ProducesResponseType(401)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateUserPassword([FromBody] LoginDto credentials)
    {
        if (credentials == null)
            return BadRequest(ModelState);

        if (!_userInterface.CheckUserByEmail(credentials.email))
            return NotFound();

        var user = _userInterface.GetUserByEmail(credentials.email);

        var hmac = new HMACSHA512(user.passwordSalt);
        var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(credentials.password));
        
        if (!user.passwordHash.SequenceEqual(passwordHash))
        {
            ModelState.AddModelError("", "Wrong Password!");
            return StatusCode(401, ModelState);
        }

        var token = CreateToken(user);

        return Ok(new TokenDto(token));

    }

    private String CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.email),
            new Claim(ClaimTypes.Name, user.name),
            new Claim(ClaimTypes.Sid, Convert.ToString(user.id)),
            new Claim(ClaimTypes.Role, user.role.ToString())
        };

        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}