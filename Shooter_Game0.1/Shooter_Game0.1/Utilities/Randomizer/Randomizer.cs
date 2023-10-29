using Shooter_Game0._1.Models.Enemies.Models;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Models.Maps;
using Shooter_Game0._1.Models.Weapons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Utilities.Randomizer
{
    //class for random input data
    public static class Randomizer
    {
        public static int RifleHeadshotRandomizer()
        {
            Random random = new Random();
           int result =  random.Next(0, 5);
            return result;
        }
        public static int ShotgunHeadshotRandomizer()
        {
            Random random = new Random();
            int result = random.Next(0, 3);
            return result;
        }
        public static int SniperHeadshotRandomizer()
        {
            Random random = new Random();
            int result = random.Next(0, 10);
            return result;
        }
        public static int XRandomizer()
        {
            Random random = new Random();
            int result = random.Next(0, 10);
            return result;
        }
        public static int YRandomizer()
        {
            Random random = new Random();
            int result = random.Next(0, 10);
            return result;
        }
        public static string EnemiesRandomizer()
        {
            List<string> enemies = new List<string> {nameof(Orc),nameof(Tank),nameof(Warrior),nameof(Wizard)};
            Random random = new Random();
            int index = random.Next(0, enemies.Count);
            return enemies[index];
        }
        public static string WeaponsRandomizer()
        {
            List<string> weapons = new List<string> {nameof(Rifle),nameof(Shotgun),nameof(Sniper)};
            Random random = new Random();
           int index =  random.Next(0,weapons.Count);
            return weapons[index];
        }  
        public static string MapRandomizer()
        {
            List<string> maps = new List<string> {nameof(DefaultMap),nameof(CustomMap)};
            Random random = new Random();
            int index = random.Next(0, maps.Count);
            return maps[index];
        }
        public static Dictionary<int,int> EnemiesGenerationRandomizer(IMap map)
        {
            Dictionary<int,int> coordinates = new Dictionary<int, int>();
            Random random = new Random();
            string[,] terrain = map.GenerateTerrain();
            int X = random.Next(0, map.X-1);
            int Y = random.Next(0, map.Y-1);
            coordinates.Add(X, Y);
            return coordinates;
        }
    }
}
