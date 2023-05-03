using AutoMapper;
using BM7_Backend.Dto;
using BM7_Backend.Interfaces;
using BM7_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public IActionResult GetTransactions(Guid userId)
    {

        var transactions = _mapper.Map<List<Transaction>>(_transactionInterface.GetTransactionsByUser(userId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(transactions);
    }
    
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(User))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(422)]
    public IActionResult CreateTransaction([FromBody] Transaction transaction)
    {
        if (transaction == null)
            return BadRequest();
        
        /*if (!_categoryInterface.CategoryExists(transaction.categoryId))
            return NotFound();

        var category = _categoryInterface.GetCategory(transaction.categoryId);

        if (!ModelState.IsValid)
            return BadRequest();*/

        var newTransaction = _mapper.Map<Transaction>(transaction);

        //newTransaction.category = category;

        if (!_transactionInterface.CreateTransaction(newTransaction))
        {
            ModelState.AddModelError("", "Something went wrong while saving!");
            return StatusCode(500, ModelState);
        }

        return Ok("Transaction created successfully!");
    }
}