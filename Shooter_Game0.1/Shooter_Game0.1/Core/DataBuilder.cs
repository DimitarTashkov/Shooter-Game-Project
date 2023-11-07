using Shooter_Game0._1.Core.Contracts;
using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Enemies.Models;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Models.Maps;
using Shooter_Game0._1.Models.Users;
using Shooter_Game0._1.Models.Users.Contracts;
using Shooter_Game0._1.Models.Weapons.Contracts;
using Shooter_Game0._1.Models.Weapons.Models;
using Shooter_Game0._1.Utilities.Messages;
using Shooter_Game0._1.Utilities.Randomizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Core
{
    public  class DataBuilder : IDataBuilder
    {
        public IEnemy CreateEnemy(string typeName)
        {
            IEnemy enemy;
            if (typeName == nameof(Orc))
            {
                enemy = new Orc();
            }
            else if (typeName == nameof(Tank))
            {
                enemy = new Tank();
            }
            else if (typeName == nameof(Warrior))
            {
                enemy = new Warrior();
            }
            else if (typeName == nameof(Wizard))
            {
                enemy = new Wizard();
            }
            else
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidEnemy, typeName));
            }
            return enemy;
        }

        public IMap CreateMap(string typeName)
        {
            IMap map;
            if (typeName == nameof(DefaultMap))
            {
                map = new DefaultMap();
            }
            //else if (typeName == nameof(CustomMap))
            //{
            //    map = new CustomMap(Randomizer.XRandomizer(), Randomizer.YRandomizer());
            //}
            else
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidMap, typeName));
            }
            return map;
        }

        public IUser CreateUser(string userName)
        {
            IUser user = new User(userName);
            return user;
        }

        public IWeapon CreateWeapon(string typeName)
        {
            IWeapon weapon;
            if (typeName == nameof(Rifle))
            {
                weapon = new Rifle();
            }
            else if (typeName == nameof(Shotgun))
            {
                weapon = new Shotgun();
            }
            else if (typeName == nameof(Sniper))
            {
                weapon = new Sniper();
            }
            else
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidWeapon, typeName));
            }
            return weapon;
        }
    }
}
