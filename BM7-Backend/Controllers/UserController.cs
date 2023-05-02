using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using BM7_Backend.Dto;
using BM7_Backend.Helper;
using BM7_Backend.Interfaces;
using BM7_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace BM7_Backend.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly UserInterface _userInterface;
    private readonly IMapper _mapper;
    private readonly HashPassword _hashPassword;


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
        
        var user = _mapper.Map<List<UserDto>>(_userInterface.GetUser(id));

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
        else
        {
            newUserDto.hash();
        }

        if (!ModelState.IsValid)
            return BadRequest();

        var user = _mapper.Map<User>(newUserDto);

        if (!_userInterface.CreateUser(user))
        {
            ModelState.AddModelError("", "Something went wrong while saving!");
            return StatusCode(500, ModelState);
        }

        return Ok("User created successfully!");
    }
    

}