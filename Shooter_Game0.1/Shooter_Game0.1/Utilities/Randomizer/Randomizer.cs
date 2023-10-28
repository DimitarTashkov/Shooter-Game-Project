using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Utilities.Randomizer
{
    public static class Randomizer
    {
        public static int RifleRandomizer()
        {
            Random random = new Random();
           int result =  random.Next(0, 5);
            return result;
        }
        public static int ShotgunRandomizer()
        {
            Random random = new Random();
            int result = random.Next(0, 3);
            return result;
        }
        public static int SniperRandomizer()
        {
            Random random = new Random();
            int result = random.Next(0, 10);
            return result;
        }
    }
}
