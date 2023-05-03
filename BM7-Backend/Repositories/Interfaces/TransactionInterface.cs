using BM7_Backend.Models;

namespace BM7_Backend.Interfaces;

public interface TransactionInterface
{
    ICollection<Transaction> GetTransactionsByUser(Guid userId);
    ICollection<Transaction> GetTransactionsByCategory(Guid categoryId);
    bool TransactionExists(Guid id);
    Transaction GetTransaction(Guid id);
    Transaction GetTransactionByUser(Guid userId);
    Transaction GetTransactionByCategory(Guid categoryId);
    bool CreateTransaction(Transaction transaction);
    
    bool UpdateTransaction(Transaction transaction);
    
    bool DeleteTransaction(Transaction transaction);
    bool Save();
    
}