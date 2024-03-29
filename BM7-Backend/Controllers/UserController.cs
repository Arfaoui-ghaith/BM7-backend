using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using BM7_Backend.Dto;
using BM7_Backend.Helper;
using BM7_Backend.Interfaces;
using BM7_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace BM7_Backend.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize(Roles="Client")]
public class UserController : Controller
{
    private readonly UserInterface _userInterface;
    private readonly IMapper _mapper;


    public UserController(UserInterface userInterface, IMapper mapper)
    {
        _userInterface = userInterface;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    public IActionResult GetUsers()
    {
        var users = _mapper.Map<List<UserDto>>(_userInterface.GetUsers());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(users);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(User))]
    [ProducesResponseType(400)]
    public IActionResult GetUser(Guid id)
    {
        if (!_userInterface.UserExists(id))
            return NotFound();
        
        var user = _mapper.Map<UserDto>(_userInterface.GetUser(id));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(User))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(422)]
    public IActionResult CreateUser([FromBody] NewUserDto newUserDto)
    {
        if (newUserDto == null)
            return BadRequest();

        if (_userInterface.CheckUserByEmail(newUserDto.email))
        {
            ModelState.AddModelError("", "Email Already Exists!");
            return StatusCode(422, ModelState);
        }

        if (newUserDto.password != newUserDto.confirmPassword)
        {
            ModelState.AddModelError("", "Password Not Match!");
            return StatusCode(401, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest();

        var user = _mapper.Map<User>(newUserDto);
        
        var hmac = new HMACSHA512();
        user.passwordSalt = hmac.Key;
        user.passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.password));

        if (!_userInterface.CreateUser(user))
        {
            ModelState.AddModelError("", "Something went wrong while saving!");
            return StatusCode(500, ModelState);
        }

        return Ok("User created successfully!");
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateUser(Guid id, [FromBody] UpdatedUserDto updatedUser)
    {
        if (updatedUser == null)
            return BadRequest(ModelState);

        if (!_userInterface.UserExists(id))
            return NotFound();

        var user = _userInterface.GetUser(id);

        if (user.email != updatedUser.email)
        {
            if (_userInterface.CheckUserByEmail(updatedUser.email))
            {
                ModelState.AddModelError("", "Email Already Exists!");
                return StatusCode(422, ModelState);
            }
        }

        user.email = updatedUser.email;
        user.name = updatedUser.name;
        user.updatedAt = DateTime.UtcNow;
        user.role = updatedUser.role;

        if (!ModelState.IsValid)
            return BadRequest();

        if (!_userInterface.UpdateUser(user))
        {
            ModelState.AddModelError("", "Something went wrong updating user");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
    
    [HttpPost("{id}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateUserPassword(Guid id, [FromBody] UpdatedUserPasswordDto updatedUser)
    {
        if (updatedUser == null)
            return BadRequest(ModelState);

        if (!_userInterface.UserExists(id))
            return NotFound();

        var user = _userInterface.GetUser(id);

        if (user.password != updatedUser.current_password)
        {
            ModelState.AddModelError("", "Wrong current password!");
            return StatusCode(401, ModelState);
        }

        if (updatedUser.password != updatedUser.confirmPassword)
        {
            ModelState.AddModelError("", "Password Not Match!");
            return StatusCode(401, ModelState);
        }
        
        var hmac = new HMACSHA512();
        
        user.passwordSalt = hmac.Key;
        user.passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.password));
        user.password = updatedUser.password;
        user.updatedAt = DateTime.UtcNow;

        if (!ModelState.IsValid)
            return BadRequest();

        if (!_userInterface.UpdateUser(user))
        {
            ModelState.AddModelError("", "Something went wrong updating user");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
    
    [HttpDelete("{userId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteUser(Guid userId)
    {
        if (!_userInterface.UserExists(userId))
        {
            return NotFound();
        }

        var user = _userInterface.GetUser(userId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_userInterface.DeleteUser(user))
        {
            ModelState.AddModelError("", "Something went wrong deleting user");
        }

        return NoContent();
    }

}