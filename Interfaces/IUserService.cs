using MyWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyWebApi.Interfaces
{
    public interface IUserService
    {
        List<User> GetAll();

        User GetById(int id);

        int Add(User user);

        bool Delete(int id);

        bool Update(int userId, User newUser);

        int Count { get;}

        int ExistUser(string name, string password);
    }
}
