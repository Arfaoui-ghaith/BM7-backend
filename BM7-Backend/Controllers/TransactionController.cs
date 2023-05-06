using AutoMapper;
using BM7_Backend.Dto;
using BM7_Backend.Interfaces;
using BM7_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace BM7_Backend.Controllers;



[Route("api/[controller]")]
[ApiController]
public class TransactionController : Controller
{
    private readonly TransactionInterface _transactionInterface;
    private readonly CategoryInterface _categoryInterface;
    private readonly IMapper _mapper;

    public TransactionController(TransactionInterface transactionInterface, CategoryInterface categoryInterface, IMapper mapper)
    {
        _categoryInterface = categoryInterface;
        _transactionInterface = transactionInterface;
        _mapper = mapper;
    }
    
    [HttpGet("{userId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Transaction>))]
    public IActionResult GetTransactionsByUser(Guid userId)
    {

        var transactions = _mapper.Map<List<TransactionDto>>(_transactionInterface.GetTransactionsByUser(userId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(transactions);
    }
    
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(User))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(422)]
    public IActionResult CreateTransaction([FromBody] NewTransactionDto newTransaction)
    {
        if (newTransaction == null)
            return BadRequest();
        
        if (!_categoryInterface.CategoryExists(newTransaction.categoryId))
            return NotFound();
        
        var category = _categoryInterface.GetCategory(newTransaction.categoryId);
        
        if (!ModelState.IsValid)
            return BadRequest();

        var transaction = _mapper.Map<Transaction>(newTransaction);
        
        transaction.category = category;

        if (!_transactionInterface.CreateTransaction(transaction))
        {
            ModelState.AddModelError("", "Something went wrong while saving!");
            return StatusCode(500, ModelState);
        }

        return Ok("Transaction created successfully!");
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(403)]
    public IActionResult UpdateTransaction(Guid id, [FromBody] UpdatedTransactionDto updatedTransaction)
    {
        if (updatedTransaction == null)
            return BadRequest(ModelState);

        if (!_transactionInterface.TransactionExists(id))
            return NotFound();
        
        var transaction = _transactionInterface.GetTransaction(id);


        transaction.amount = updatedTransaction.amount;
        transaction.title = updatedTransaction.title;
        transaction.status = updatedTransaction.status;
        transaction.updatedAt = DateTime.UtcNow;

        if (!ModelState.IsValid)
            return BadRequest();

        if (!_transactionInterface.UpdateTransaction(transaction))
        {
            ModelState.AddModelError("", "Something went wrong updating transaction");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteTransaction(Guid id)
    {
        if (!_transactionInterface.TransactionExists(id))
        {
            return NotFound();
        }

        var transaction = _transactionInterface.GetTransaction(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_transactionInterface.DeleteTransaction(transaction))
        {
            ModelState.AddModelError("", "Something went wrong deleting transaction");
        }

        return NoContent();
    }
}