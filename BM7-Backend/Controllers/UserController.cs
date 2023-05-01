using BM7_Backend.Interfaces;
using BM7_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace BM7_Backend.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly UserInterface _userInterface;


    public UserController(UserInterface userInterface)
    {
        _userInterface = userInterface;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    public IActionResult GetUsers()
    {
        var users = _userInterface.GetUsers();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(users);
    }
}