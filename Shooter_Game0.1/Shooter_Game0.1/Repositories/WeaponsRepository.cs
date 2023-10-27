using Shooter_Game0._1.Models.Weapons.Contracts;
using Shooter_Game0._1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Repositories
{
    public class WeaponsRepository : IRepository<IWeapon>
    {
        private List<IWeapon> weapons;
        public WeaponsRepository()
        {
            weapons = new List<IWeapon>();
        }
        public void AddNew(IWeapon name)
        {
            weapons.Add(name);
        }

        public IReadOnlyCollection<IWeapon> Models() => weapons.AsReadOnly();


        public bool RemoveByName(string typeName)
            => weapons.Remove(weapons.FirstOrDefault(w => w.Name == typeName));

    }
}
