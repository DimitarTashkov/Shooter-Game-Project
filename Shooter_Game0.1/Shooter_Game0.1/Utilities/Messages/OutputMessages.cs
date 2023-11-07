using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Utilities.Messages
{
    public static class OutputMessages
    {
        public static string EnemyWasAdded = "{0} has been added successfully to our enemies!";
        public static string MapWasAdded = "{0} has been added successfully to our maps!";
        public static string WeaponWasAdded = "{0} has been added successfully to our weapons!";
        public static string EnemiesGenerated = "{0} enemies has been generated on {1}! ";
        public static string EnemyWasShotFor = "{0} has been shot for {1} damage with {2} in coordinate {3}:{4}!";
        public static string EnemySurvived = "{0} survived with {1} hp and ran!";
        public static string EnemyWasKilled = "{0} enemy was successfully killed with {1} in coordinate {2}:{3} !";
        public static string UserReport = "User: {0}, Damage dealt: {1}, Total kills: {2}, Points: {3} !";
        public static string NoEnemyInThisLocation = "There is not enemy on {0}:{1} coordinates !";
        public static string HintClosestDownEnemy = "The closest enemy is {0} rows down and {1} steps  from you";
        public static string HintClosestUpEnemy = "The closest enemy is {0} rows up and {1} steps from you";
        public static string InvalidUsername = "Invalid username! Please write a new username !";
        public static string WelcomeToOurGame = "Welcome to our game {0}! This is your generated map:";
        public static string GameInfo = "Enter one of the following command: Shoot/StatsUpdate/Report/Hint";
    }
}
