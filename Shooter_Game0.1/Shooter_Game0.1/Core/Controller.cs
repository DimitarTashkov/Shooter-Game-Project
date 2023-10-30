using Shooter_Game0._1.Core.Contracts;
using Shooter_Game0._1.IO;
using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Models.Users.Contracts;
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
        private double collectDealtDamage;
        private int collectKills;

        private EnemiesRepository enemies;
        private WeaponsRepository weapons;
        private MapsRepository maps;
        private UsersRepository users;
        private DataBuilder builder;
        private StringBuilder sb;
        private Dictionary<Dictionary<int, int>,IEnemy> enemiesCoordinates;
        private Writer writer;
        public Controller()
        {
            this.enemies = new EnemiesRepository();
            this.weapons = new WeaponsRepository();
            this.maps = new MapsRepository();
            this.users = new UsersRepository();
            this.builder = new DataBuilder();
            this.sb = new StringBuilder();
            this.writer = new Writer();
            enemiesCoordinates = new Dictionary<Dictionary<int, int>, IEnemy>();
        }
        public void GenerateEnemies(IMap map, int countOfEnemies)
        {
            map.GenerateTerrain();
            for (int i = 0; i < countOfEnemies; i++)
            {
                IEnemy generatedEnemy = Randomizer.EnemiesRandomizer();
                enemies.AddNew(generatedEnemy);
                Dictionary<int, int> enemyCoordinates = Randomizer.EnemiesGenerationRandomizer(map);
                 while(enemiesCoordinates.ContainsKey(enemyCoordinates)) 
                {
                    enemyCoordinates = Randomizer.EnemiesGenerationRandomizer(map);
                }
                 if(!enemiesCoordinates.ContainsKey(enemyCoordinates))
                {
                    for (int rows = 0; rows < map.X; rows++)
                    {
                        for (int cols = 0; cols < map.Y; cols++)
                        {
                            foreach (var kvp in enemyCoordinates)
                            {
                                if (rows == kvp.Key && cols == kvp.Value)
                                {
                                    map.Terrain[rows, cols] = "*";
                                }
                            }
                        }
                    }
                    enemiesCoordinates.Add(enemyCoordinates, generatedEnemy);
                }                
               
            }
            maps.AddNew(map);
            map.VisualizeMap(map.Terrain);

            sb.AppendLine(string.Format(OutputMessages.MapWasAdded, map.GetType().Name));
            sb.AppendLine(string.Format(OutputMessages.EnemiesGenerated, countOfEnemies, map.GetType().Name));
        }

        public void Shoot(int xCoordinate, int yCoordinate)
        {
            Dictionary<int, int> aimCoordinates = new Dictionary<int, int>();
            aimCoordinates.Add(xCoordinate, yCoordinate);

            IWeapon weapon = Randomizer.WeaponsRandomizer();
            IMap map = maps.Models().FirstOrDefault();
            IEnemy enemy = ReturnEnemyFromCoordinates(xCoordinate,yCoordinate,enemiesCoordinates);
            weapons.AddNew(weapon);
            if(map == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.MapHasNotBeenAdded, map.GetType().Name));
            }            
            if (enemy == null)
            {
                writer.WriteLine(Environment.NewLine);
                map.Terrain[xCoordinate, yCoordinate] = "+";
                map.VisualizeMap(map.Terrain);
                sb.AppendLine(string.Format(OutputMessages.NoEnemyInThisLocation, xCoordinate, yCoordinate));
                return;
            }
                weapon.CalculateDamage();
                enemy.CalculateLife();
                double remain = weapon.Damage - enemy.Life;
                if (remain <= 0)
                {
                    collectDealtDamage += weapon.Damage;
                    enemy.Life -= weapon.Damage;
                    sb.AppendLine(string.Format(OutputMessages.EnemyWasShotFor, enemy.GetType().Name, weapon.Damage, weapon.GetType().Name,xCoordinate,yCoordinate));
                    sb.AppendLine(enemy.RegenHealth());
                    enemy.RunCoordinates(map, enemy, enemiesCoordinates);                   

                }
                else
                {
                    collectDealtDamage += weapon.Damage;
                    collectKills++;
                    sb.AppendLine(string.Format(OutputMessages.EnemyWasKilled, enemy.GetType().Name, weapon.GetType().Name,xCoordinate,yCoordinate));
                    enemy.IsEnemyKilled = true;
                    
                }
            map.Terrain[xCoordinate, yCoordinate] = "+";
            map.VisualizeMap(map.Terrain);
        }

        public void StatsUpdate(string username)
        {
            IUser user = null;
            if(users.Models().Any(u => u.Username == username))
            {
                 user = users.Models().FirstOrDefault(u => u.Username == username);
            }
            else
            {
                user = builder.CreateUser(username);
                users.AddNew(user);
            }
            user.DamageDealt += collectDealtDamage;
            user.EnemiesKilled += collectKills;
            user.Points = (user.EnemiesKilled * 300) + (user.DamageDealt / 3);
            sb.AppendLine(string.Format(OutputMessages.UserReport, username, user.DamageDealt, user.EnemiesKilled, Math.Round(user.Points,2)));
        }
        public void Report()
        {
            writer.WriteLine(sb.ToString().Trim());
        }
        private IEnemy ReturnEnemyFromCoordinates(int x,int y, Dictionary<Dictionary<int, int>, IEnemy> enemiesCoordinates)
        {
            Dictionary<int, int> aimCoordinates = new Dictionary<int, int>();
            aimCoordinates.Add(x, y);

            IEnemy enemy = null;
            foreach (var kvp in enemiesCoordinates)
            {
                Dictionary<int, int> coordinate = kvp.Key;

                // Check if the current dictionary entry's coordinates match the target coordinates
                if (coordinate.ContainsKey(x) && coordinate[x] == y)
                {
                    enemy = kvp.Value; // Assign the matching IEnemy to the 'enemy' variable
                    break; // Exit the loop since we found the enemy
                }
            }
            return enemy;
        }
        
    }
}
