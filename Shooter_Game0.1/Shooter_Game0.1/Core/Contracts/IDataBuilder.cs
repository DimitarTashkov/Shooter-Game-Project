using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Models.Users.Contracts;
using Shooter_Game0._1.Models.Weapons.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Core.Contracts
{
    public  interface IDataBuilder
    {
        public IEnemy CreateEnemy(string typeName);
        public IWeapon CreateWeapon(string typeName);
        public IMap CreateMap(string typeName);
        public IUser CreateUser(string userName);
    }
}
