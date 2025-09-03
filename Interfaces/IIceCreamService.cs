using MyWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyWebApi.Interfaces
{
    public interface IIceCreamService
    {
        List<IceCream> GetAll(int userId);

        IceCream GetById(int id);

        int Add(IceCream iceCream, int userId);

        bool Delete(int id);

        bool Update(int id, IceCream newIceCream, int userId);

        public void DeleteIceCreamsOfUser(int userId);

        int Count { get;}
    }
}
