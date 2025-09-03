using MyWebApi.Models;
using MyWebApi.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MyWebApi.Services;

public class UserService : IUserService
{
    List<User> Users { get; }
    private string fileName = "Users.json";
    int nextId = 3;
    public UserService()
    {
        this.fileName = Path.Combine("Data", "users.json");
        using (var jsonFile = File.OpenText(fileName))
        {
            Users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
        
    private void saveToFile()
    {
        File.WriteAllText(fileName, JsonSerializer.Serialize(Users));
    }

    public List<User> GetAll() => Users;

    public User GetById(int id) => Users.FirstOrDefault(p => p.userId == id);

    public int Add(User user)
    {
        if (Users.Count == 0)
            user.userId = 100;
        else
            user.userId = Users.Max(p => p.userId) + 1;
    
        Users.Add(user);
        saveToFile();

        return user.userId;      
    }

    public bool Delete(int id)
    {
        var existingUser = GetById(id);
        if (existingUser == null)
            return false;

        var index = Users.IndexOf(existingUser);
        if (index == -1)
            return false;

        Users.RemoveAt(index);
        saveToFile();

        return true;
    }

    public bool Update(int userId, User newUser)
    {
        if (userId != newUser.userId)
            return false;
        
        var existingUser = GetById(userId);
        if (existingUser == null)
            return false;

        var index = Users.IndexOf(existingUser);
        if (index == -1)
            return false;

        Users[index] = newUser;
        saveToFile();

        return true;
    }

    public int Count { get => Users.Count(); }

    public int ExistUser(string name, string password)
    {
        User existUser = Users.FirstOrDefault(u => u.userName.Equals(name) && u.password.Equals(password));
        Console.WriteLine(existUser);
        if (existUser != null)
            return existUser.userId;

        return -1;
    }
}
