using BM7_Backend.Models;

namespace BM7_Backend.Interfaces;

public interface CategoryInterface
{
    
    ICollection<Category> GetCategoriesByUser(Guid userId);
    bool CategoryExists(Guid id);

    Category GetCategory(Guid id);
    
    bool CreateCategory(Category category);

    bool UpdateCategory(Category category);
    
    bool DeleteCategory(Category category);

    bool Save();
}