using BM7_Backend.Models;

namespace BM7_Backend.Interfaces;

public interface UserInterface
{
    ICollection<User> GetUsers();
}