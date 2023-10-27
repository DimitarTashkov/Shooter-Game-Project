using Shooter_Game0._1.Models.Weapons.Contracts;
using Shooter_Game0._1.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Models.Weapons.Models
{
    public abstract class Weapons : IWeapon
    {
        private string name;
        private double ammoType;
        private double power;

        protected Weapons(string name, double ammoType, double power)
        {
            this.name = name;
            this.ammoType = ammoType;
            this.power = power;
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.EmptyName));
                }
                name = value;
            }
        }

        public double AmmoType
        {
            get { return ammoType; }
            set
            {
                if (value <= 0 )
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.NegativeDamageValue));
                }
                ammoType = value;
            }
        }

        public double Power
        {
            get { return power; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.NegativeDamageValue));
                }
                power = value;
            }
        }

        public double Damage => Math.Round(ammoType*power,2);
    }
}
