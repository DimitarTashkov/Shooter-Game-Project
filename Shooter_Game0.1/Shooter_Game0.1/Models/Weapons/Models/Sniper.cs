using Shooter_Game0._1.Utilities.Randomizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Models.Weapons.Models
{
    public class Sniper : Weapons
    {
        private const double AmmoTypeInfo = 1;
        private const double PowerInfo = 800;
        private const int HeadShotNumber = 1;
        public Sniper() : base(AmmoTypeInfo, PowerInfo)
        {
        }

        public override bool IsHeadShot()
        {
            //20% chance for headshot(If you change it update the comment)
            int weaponHeadshotRandomNumber = Randomizer.SniperRandomizer();
            if (weaponHeadshotRandomNumber == HeadShotNumber)
            {
                return true;
            }
            return false;
        }
    }
}
