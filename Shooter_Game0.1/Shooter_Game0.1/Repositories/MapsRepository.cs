using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Repositories
{
    public class MapsRepository : IRepository<IMap>
    {
        private List<IMap> maps;
        public MapsRepository()
        {
            maps = new List<IMap>();
        }
        public void AddNew(IMap name)
        {
            maps.Add(name);
        }

        public IReadOnlyCollection<IMap> Models() => maps.AsReadOnly();


        public bool RemoveByName(string typeName) => maps.Remove(maps.FirstOrDefault(m => m.GetType().Name == typeName));

    }
}
