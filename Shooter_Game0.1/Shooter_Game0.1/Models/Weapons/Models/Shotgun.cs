using Shooter_Game0._1.Utilities.Randomizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Models.Weapons.Models
{
    public class Shotgun : Weapons
    {
        private const double AmmoTypeInfo = 9;
        private const double PowerInfo = 33;
        private const int HeadShotNumber = 1;
        public Shotgun() : base(AmmoTypeInfo, PowerInfo)
        {
        }

        public override bool IsHeadShot()
        {
            //20% chance for headshot(If you change it update the comment)
            int weaponHeadshotRandomNumber = Randomizer.ShotgunHeadshotRandomizer();
            if (weaponHeadshotRandomNumber == HeadShotNumber)
            {
                return true;
            }
            return false;
        }

    }
}
