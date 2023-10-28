using Shooter_Game0._1.Core.Contracts;
using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Models.Weapons.Contracts;
using Shooter_Game0._1.Repositories;
using Shooter_Game0._1.Utilities.Randomizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Core
{
    public class Controller : IController
    {
        private EnemiesRepository enemies;
        private WeaponsRepository weapons;
        private MapsRepository maps;
        private DataBuilder builder;
        private Dictionary<IEnemy, Dictionary<int, int>> enemiesCoordinates;
        public Controller()
        {
            this.enemies = new EnemiesRepository();
            this.weapons = new WeaponsRepository();
            this.maps = new MapsRepository();
            this.builder = new DataBuilder();
            enemiesCoordinates = new Dictionary<IEnemy, Dictionary<int, int>>();
        }
        public void GenerateEnemies(string mapName, int countOfEnemies)
        {
            IMap map = builder.CreateMap(mapName);
            int[,] navigator = new int[map.X, map.Y];
            maps.AddNew(map);
            for (int i = 0; i < countOfEnemies; i++)
            {
                IEnemy generatedEnemy = builder.CreateEnemy(Randomizer.EnemiesRandomizer());
                enemiesCoordinates[generatedEnemy] = Randomizer.EnemiesGenerationRandomizer(map);
                for (int rows = 0; rows < map.X; rows++)
                {
                    for (int cols = 0; cols < map.Y; cols++)
                    {
                        foreach (var values in enemiesCoordinates.Values)
                        {
                            foreach (var value in values)
                            {
                                if (navigator[rows, cols] == navigator[value.Key,value.Value] )
                                {
                                    //ik it looks ugly but I will try my best to improve it
                                }
                            }
                        }
                    }
                }

            }
        }

        public void Shoot(string weaponName, string enemyName)
        {
            throw new NotImplementedException();
        }
    }
}
