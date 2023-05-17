using AutoMapper;
using BM7_Backend.Dto;
using BM7_Backend.Interfaces;
using BM7_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BM7_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles="Client")]
public class CategoryController : Controller
{
    private readonly CategoryInterface _categoryInterface;
    private readonly UserInterface _userInterface;
    private readonly IMapper _mapper;


    public CategoryController(CategoryInterface categoryInterface, UserInterface userInterface, IMapper mapper)
    {
        _categoryInterface = categoryInterface;
        _userInterface = userInterface;
        _mapper = mapper;
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
    [ProducesResponseType(400)]
    public IActionResult GetCategoriesByUser(Guid userId)
    {
        if (!_userInterface.UserExists(userId))
            return NotFound();
        
        var categories = _mapper.Map<List<CategoryDto>>(_categoryInterface.GetCategoriesByUser(userId));
        
        Console.WriteLine(categories.ToString());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(categories);
    }
    
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(User))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(422)]
    public IActionResult CreateCategory([FromBody] NewCategoryDto category)
    {
        if (category == null)
            return BadRequest();
        
        if (!_userInterface.UserExists(category.userId))
            return NotFound();

        var user = _userInterface.GetUser(category.userId);

        if (!ModelState.IsValid)
            return BadRequest();

        var newCategory = _mapper.Map<Category>(category);

        newCategory.user = user;

        if (!_categoryInterface.CreateCategory(newCategory))
        {
            ModelState.AddModelError("", "Something went wrong while saving!");
            return StatusCode(500, ModelState);
        }

        return Ok("Category created successfully!");
    }

    [HttpPut("{id}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(403)]
    public IActionResult UpdateCategory(Guid id, [FromBody] UpdatedCategoryDto updatedCategory)
    {
        if (updatedCategory == null)
            return BadRequest(ModelState);

        if (!_categoryInterface.CategoryExists(id))
            return NotFound();
        
        var category = _categoryInterface.GetCategory(id);

        /*if (!_categoryInterface.CategoryUniqueTitleByUser(category.user.id, category.id, updatedCategory.title))
        {
            ModelState.AddModelError("", "Title already used !");
            return StatusCode(403, ModelState);
        }*/

        category.title = updatedCategory.title;
        category.image = updatedCategory.image;
        category.updatedAt = DateTime.UtcNow;

        if (!ModelState.IsValid)
            return BadRequest();

        if (!_categoryInterface.UpdateCategory(category))
        {
            ModelState.AddModelError("", "Something went wrong updating category");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteCategory(Guid id)
    {
        if (!_categoryInterface.CategoryExists(id))
        {
            return NotFound();
        }

        var category = _categoryInterface.GetCategory(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_categoryInterface.DeleteCategory(category))
        {
            ModelState.AddModelError("", "Something went wrong deleting category");
        }

        return NoContent();
    }
}