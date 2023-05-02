using BM7_Backend.Interfaces;
using BM7_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace BM7_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : Controller
{
    private readonly CategoryInterface _categoryInterface;
    private readonly UserInterface _userInterface;


    public CategoryController(CategoryInterface categoryInterface, UserInterface userInterface)
    {
        _categoryInterface = categoryInterface;
        _userInterface = userInterface;
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
    [ProducesResponseType(400)]
    public IActionResult GetCategoriesByUser(Guid userId)
    {
        if (!_userInterface.UserExists(userId))
            return NotFound();
        
        var categories = _categoryInterface.getCategoriesByUser(userId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(categories);
    }
}