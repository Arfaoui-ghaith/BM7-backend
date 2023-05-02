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

    public bool UserExists(Guid id)
    {
        return _dbContext.Users.Any(u => u.id == id);
    }

    public User GetUser(Guid id)
    {
        return _dbContext.Users.FirstOrDefault(u => u.id == id);
    }

    public bool CheckUserByEmail(string email)
    {
        return _dbContext.Users.Any(u => u.email == email);
    }

    public bool CreateUser(User user)
    {
        _dbContext.Add(user);
        return Save();
    }
    
    public bool Save()
    {
        var saved = _dbContext.SaveChanges();
        return saved > 0 ? true : false;
    }
}