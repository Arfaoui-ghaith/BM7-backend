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

    public ICollection<Category> GetCategoriesByUser(Guid userId)
    {
        return _dbContext.Categories.Where(c => c.user.id == userId).OrderBy(user => user.createdAt).ToList();
    }

    public Category? GetCategory(Guid categoryId)
    {
        return _dbContext.Categories.FirstOrDefault(c => c.id == categoryId);
    }

    public bool CategoryExists(Guid id)
    {
        return _dbContext.Categories.Any(c => c.id == id);
    }

    public bool CreateCategory(Category category)
    {
        _dbContext.Add(category);
        return Save();
    }

    public bool UpdateCategory(Category category)
    {
        _dbContext.Update(category);
        return Save();
    }

    public bool DeleteCategory(Category category)
    {
        _dbContext.Remove(category);
        return Save();
    }

    public bool Save()
    {
        var saved = _dbContext.SaveChanges();
        return saved > 0 ? true : false;
    }
}