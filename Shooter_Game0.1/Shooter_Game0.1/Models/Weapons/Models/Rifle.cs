using Shooter_Game0._1.Utilities.Randomizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Models.Weapons.Models
{
    public class Rifle : Weapons
    {
        private const double AmmoTypeInfo = 22;
        private const double PowerInfo = 20;
        private const int HeadShotNumber = 1;
        public Rifle() : base( AmmoTypeInfo, PowerInfo)
        {
        }

        public override bool IsHeadShot()
        {
            //20% chance for headshot(If you change it update the comment)
            int weaponHeadshotRandomNumber = Randomizer.RifleRandomizer();
            if(weaponHeadshotRandomNumber == HeadShotNumber)
            {
                return true;
            }
            return false;
        }

    }
}
