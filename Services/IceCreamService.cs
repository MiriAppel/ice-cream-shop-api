using MyWebApi.Models;
using MyWebApi.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MyWebApi.Services
{
    public class IceCreamService : IIceCreamService
    {
        List<IceCream> IceCreams { get; }
        private string fileName = "IceCreams.json";
        int nextId = 3;
        public IceCreamService()
        {
            this.fileName = Path.Combine("Data", "iceCreams.json");
            using (var jsonFile = File.OpenText(fileName))
            {
                IceCreams = JsonSerializer.Deserialize<List<IceCream>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private void saveToFile()
        {
            File.WriteAllText(fileName, JsonSerializer.Serialize(IceCreams));
        }

        public List<IceCream> GetAll(int userId)
        {
            return IceCreams.FindAll(ice => ice.userId == userId);
        }

        public IceCream GetById(int id) 
        {
            return IceCreams.FirstOrDefault(p => p.id == id);
        } 

        public int Add(IceCream newIceCream, int userId)
        {
            newIceCream.userId = userId;
            if (IceCreams.Count() == 0)
                newIceCream.id = 1;
            else
                newIceCream.id = IceCreams.Max(p => p.id) + 1;

            IceCreams.Add(newIceCream);
            saveToFile();

            return newIceCream.id;
        }

        public bool Delete(int id)
        {
            var iceCream = GetById(id);
            if(iceCream is null)
                return false;

            var index = IceCreams.IndexOf(iceCream);
            if (index == -1)
                return false;

            IceCreams.RemoveAt(index);
            saveToFile();

            return true;
        }

        public bool Update(int id, IceCream newIceCream, int userId)
        {
            if (userId != newIceCream.userId)
                return false;

            var existingIce = GetById(id);
            if (existingIce == null)
                return false;

            newIceCream.userId=userId;

            var index = IceCreams.IndexOf(existingIce);
            if (index == -1)
                return false;

            IceCreams[index] = newIceCream;
            saveToFile();
            
            return true;
        }

        public void DeleteIceCreamsOfUser(int userId)
        {
            for (int i = 0; i < IceCreams.Count(); i++)
            {
                if(IceCreams[i].userId == userId)
                {
                    Delete(IceCreams[i].id);
                }
            }
        }

        public int Count { get => IceCreams.Count(); }
    }
}
