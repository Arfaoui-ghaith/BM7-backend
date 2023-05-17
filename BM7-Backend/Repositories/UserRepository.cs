using System.Diagnostics;
using BM7_Backend.Context;
using BM7_Backend.Interfaces;
using BM7_Backend.Models;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

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

    public User? GetUser(Guid id)
    {
        return _dbContext.Users.FirstOrDefault(u => u.id == id);
    }

    public User? GetUserByEmail(string email)
    {
        return _dbContext.Users.FirstOrDefault(u => u.email == email);
    }

    public bool CheckUserByEmail(string email)
    {
        return _dbContext.Users.Any(u => u.email == email);
    }

    public bool CreateUser(User user)
    {
        
        _dbContext.Add(user);
        Save();
        return sendEmail(user);
    }

    public bool UpdateUser(User user)
    {
        _dbContext.Update(user);
        return Save();
    }

    public bool DeleteUser(User user)
    {
        _dbContext.Remove(user);
        return Save();
    }

    public bool Save()
    {
        var saved = _dbContext.SaveChanges();
        return saved > 0 ? true : false;
    }
    
    public bool sendEmail(User user)
    {
        Configuration.Default.ApiKey.Add("api-key", "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");

        var apiInstance = new TransactionalEmailsApi();
        string SenderName = "Budget Management Application - BM7";
        string SenderEmail = "noreply@bm7.com";
        SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
        string ToEmail = user.email;
        string ToName = user.name;
        SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, ToName);
        List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
        To.Add(smtpEmailTo);
            
        string HtmlContent = "<html><body><h1>Hi "+user.name+", Welcome to our application</h1></body></html>";
        string TextContent = null;
        string Subject = "Welcome";
            
        try
        {
            var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, HtmlContent, TextContent, Subject);
            CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
            Debug.WriteLine(result.ToJson());
            Console.WriteLine(result.ToJson());
            Console.ReadLine();
            return true;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            Console.WriteLine(e.Message);
            Console.ReadLine();
            return false;
        }
    }
}