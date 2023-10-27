using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Models.Weapons.Contracts
{
    public interface IWeapon
    {
       public string Name { get; }
       public double AmmoType { get; }
        public double Power { get; }
        public double Damage { get; }
    }
}
