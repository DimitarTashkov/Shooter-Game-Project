using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Utilities.Messages
{
    public static class ExceptionMessages
    {
        public static string InvalidWeaponsStats = "Our weapons must have ammo and power values!";
        public static string IvalidHealthValue = "size and health must be bigger than 0!";
        public static string InvalidCoordinates = "map/pointer coordinates must be in range of 1 and 10!";
        public static string EnemyHasAlreadyBeenRegenerated = "{0} has already been regenerated!";
        public static string MapRowsOrColsShouldNotBeEmpty = "Map should have valid rows and cols!";
        public static string InvalidEnemy = "{0} enemy is not supported!";
        public static string InvalidMap = "{0} map is not supported!";
        public static string InvalidWeapon = "{0} weapon is not supported!";
        public static string MapHasNotBeenAdded = "{0} has not been already created!";
        public static string EmptyOrInvalidUsername = "You must enter valid username!";
        public static string ShootInbound = "You must shoot within this range: {0}:{1}! Enter new values!";
    }
}
