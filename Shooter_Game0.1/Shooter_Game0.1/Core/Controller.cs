using Shooter_Game0._1.Core.Contracts;
using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Models.Weapons.Contracts;
using Shooter_Game0._1.Repositories;
using Shooter_Game0._1.Utilities.Messages;
using Shooter_Game0._1.Utilities.Randomizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Core
{
    public class Controller : IController
    {
        private EnemiesRepository enemies;
        private WeaponsRepository weapons;
        private MapsRepository maps;
        private UsersRepository users;
        private DataBuilder builder;
        private StringBuilder sb;
        private Dictionary<Dictionary<int, int>,IEnemy> enemiesCoordinates;
        public Controller()
        {
            this.enemies = new EnemiesRepository();
            this.weapons = new WeaponsRepository();
            this.maps = new MapsRepository();
            this.users = new UsersRepository();
            this.builder = new DataBuilder();
            this.sb = new StringBuilder();
            enemiesCoordinates = new Dictionary<Dictionary<int, int>, IEnemy>();
        }
        public void GenerateEnemies(string mapName, int countOfEnemies)
        {
            IMap map = builder.CreateMap(mapName);
            string[,] terrain = map.GenerateTerrain();
            maps.AddNew(map);
            for (int i = 0; i < countOfEnemies; i++)
            {
                IEnemy generatedEnemy = builder.CreateEnemy(Randomizer.EnemiesRandomizer());
                enemies.AddNew(generatedEnemy);
                Dictionary<int, int> enemyCoordinates = Randomizer.EnemiesGenerationRandomizer(map);
                 while(enemiesCoordinates.ContainsKey(enemyCoordinates)) 
                {
                    enemyCoordinates = Randomizer.EnemiesGenerationRandomizer(map);
                }
                 if(!enemiesCoordinates.ContainsKey(enemyCoordinates))
                {
                    for (int rows = 0; rows < map.MapTerrain.GetLength(0); rows++)
                    {
                        for (int cols = 0; cols < map.MapTerrain.GetLength(1); cols++)
                        {
                            foreach (var kvp in enemyCoordinates)
                            {
                                if (terrain[rows, cols] == terrain[kvp.Key,kvp.Value])
                                {
                                    terrain[rows, cols] = "*";
                                }
                            }
                        }
                    }
                    enemiesCoordinates.Add(enemyCoordinates, generatedEnemy);
                }
                map.VisualizeMap(terrain);
               
            }
            sb.AppendLine(string.Format(OutputMessages.MapWasAdded, mapName));
            sb.AppendLine(string.Format(OutputMessages.EnemiesGenerated, countOfEnemies, mapName));
        }

        public void Shoot(string weaponName, string enemyName,string mapName)
        {
            IWeapon weapon = builder.CreateWeapon(weaponName);
            IEnemy enemy = builder.CreateEnemy(enemyName);
            IMap map = builder.CreateMap(mapName);
            weapons.AddNew(weapon);
            if(maps.Models().Contains(map))
            {
                weapon.CalculateDamage();
                enemy.CalculateLife();
                double remain = weapon.Damage - enemy.Life;
                if (remain <= 0)
                {
                    sb.AppendLine(string.Format(OutputMessages.EnemyWasShotFor, enemyName, weapon.Damage, weaponName));
                    sb.AppendLine(enemy.RegenHealth());
                    enemy.RunCoordinates(mapName, enemyName, enemiesCoordinates);                   

                }
                else
                {
                    sb.AppendLine(string.Format(OutputMessages.EnemyWasKilled, enemyName, weaponName));
                    enemy.IsEnemyKilled = true;
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.MapHasNotBeenAdded, mapName));
            }           
        }
    }
}
