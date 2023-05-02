using BM7_Backend.Models;

namespace BM7_Backend.Interfaces;

public interface CategoryInterface
{
    ICollection<Category> getCategoriesByUser(Guid userId);
    bool CategoryExists(Guid id);
}