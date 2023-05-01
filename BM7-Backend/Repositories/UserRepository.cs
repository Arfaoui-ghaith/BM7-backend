using BM7_Backend.Context;
using BM7_Backend.Interfaces;
using BM7_Backend.Models;

namespace BM7_Backend.Repositories;

public class UserRepository : UserInterface
{
    private readonly MyDbContext _dbContext;
    public UserRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ICollection<User> GetUsers()
    {
        return _dbContext.Users.OrderBy(user => user.createdAt).ToList();
    } 
}