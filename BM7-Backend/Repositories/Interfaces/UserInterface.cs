using BM7_Backend.Models;

namespace BM7_Backend.Interfaces;

public interface UserInterface
{
    ICollection<User> GetUsers();
    
    bool UserExists(Guid id);

    User GetUser(Guid id);

    bool CheckUserByEmail(String email);

    bool CreateUser(User user);

    bool Save();
}