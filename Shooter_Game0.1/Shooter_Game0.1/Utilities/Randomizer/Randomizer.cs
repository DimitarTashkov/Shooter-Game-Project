using Shooter_Game0._1.Core;
using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Enemies.Models;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Models.Maps;
using Shooter_Game0._1.Models.Weapons.Contracts;
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
        private static DataBuilder builder;
         static Randomizer()
        {
            builder = new DataBuilder();
        }
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
            int result = random.Next(2, 10);
            return result;
        }
        public static int YRandomizer()
        {
            Random random = new Random();
            int result = random.Next(2, 10);
            return result;
        }
        public static IEnemy EnemiesRandomizer()
        {
            List<string> enemies = new List<string> {nameof(Orc),nameof(Tank),nameof(Warrior),nameof(Wizard)};
            Random random = new Random();
            int index = random.Next(0, enemies.Count);
            IEnemy enemy = builder.CreateEnemy(enemies[index]);
            return enemy;
        }
        public static IWeapon WeaponsRandomizer()
        {
            List<string> weapons = new List<string> {nameof(Rifle),nameof(Shotgun),nameof(Sniper)};
            Random random = new Random();
           int index =  random.Next(0,weapons.Count);
            IWeapon weapon = builder.CreateWeapon(weapons[index]);
            return weapon;
        }  
        public static IMap MapRandomizer()
        {
            List<string> maps = new List<string> { nameof(DefaultMap), };//nameof(CustomMap)};
            Random random = new Random();
            int index = random.Next(0, maps.Count);
            IMap map = builder.CreateMap(maps[index]);
            return map;
        }
        public static Dictionary<int,int> EnemiesGenerationRandomizer(IMap map)
        {
            Dictionary<int,int> coordinates = new Dictionary<int, int>();
            Random random = new Random();
             map.GenerateTerrain();
            int X = random.Next(0, map.X);
            int Y = random.Next(0, map.Y);
            coordinates.Add(X, Y);
            return coordinates;
        }

    }
}
