using BM7_Backend.Context;
using BM7_Backend.Interfaces;
using BM7_Backend.Models;

namespace BM7_Backend.Repositories;

public class TransactionRepository : TransactionInterface
{
    private readonly MyDbContext _dbContext;

    public TransactionRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ICollection<Transaction> GetTransactionsByUser(Guid userId)
    {
        return _dbContext.Transactions.Where(t => t.category.user.id == userId).OrderBy(user => user.createdAt).ToList();
    }

    public ICollection<Transaction> GetTransactionsByCategory(Guid categoryId)
    {
        return _dbContext.Transactions.Where(t => t.category.id == categoryId).OrderBy(user => user.createdAt).ToList();
    }

    public bool TransactionExists(Guid id)
    {
        return _dbContext.Transactions.Any(t => t.id == id);
    }

    public Transaction GetTransaction(Guid id)
    {
        return _dbContext.Transactions.FirstOrDefault(t => t.id == id);
    }

    public Transaction GetTransactionByUser(Guid userId)
    {
        return _dbContext.Transactions.FirstOrDefault(t => t.category.user.id == userId);
    }

    public Transaction GetTransactionByCategory(Guid categoryId)
    {
        return _dbContext.Transactions.FirstOrDefault(t => t.category.id == categoryId);
    }

    public bool CreateTransaction(Transaction transaction)
    {
        _dbContext.Add(transaction);
        return Save();
    }

    public bool UpdateTransaction(Transaction transaction)
    {
        _dbContext.Update(transaction);
        return Save();
    }

    public bool DeleteTransaction(Transaction transaction)
    {
        _dbContext.Remove(transaction);
        return Save();
    }

    public bool Save()
    {
        var saved = _dbContext.SaveChanges();
        return saved > 0 ? true : false;
    }
}