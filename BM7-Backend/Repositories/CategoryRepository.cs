using BM7_Backend.Context;
using BM7_Backend.Interfaces;
using BM7_Backend.Models;

namespace BM7_Backend.Repositories;

public class CategoryRepository : CategoryInterface
{
    private readonly MyDbContext _dbContext;
    public CategoryRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ICollection<Category> getCategoriesByUser(Guid userId)
    {
        return _dbContext.Categories.Where(c => c.user.id == userId).OrderBy(user => user.createdAt).ToList();
    }

    public bool CategoryExists(Guid id)
    {
        return _dbContext.Categories.Any(c => c.id == id);
    }
}