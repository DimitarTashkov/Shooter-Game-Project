using Shooter_Game0._1.Core.Contracts;
using Shooter_Game0._1.Factories;
using Shooter_Game0._1.IO;
using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Models.Users.Contracts;
using Shooter_Game0._1.Models.Weapons.Contracts;
using Shooter_Game0._1.Repositories;
using Shooter_Game0._1.Utilities.Hinter;
using Shooter_Game0._1.Utilities.Messages;
using Shooter_Game0._1.Utilities.Randomizer;
using Shooter_Game0._1.Core.Commands;
using Shooter_Game0._1.Models.SaveData;
using System.Text;

namespace Shooter_Game0._1.Core
{
    public class Controller : IController
    {
        private double collectDealtDamage;
        private int collectKills;

        private int oldXCoordinate;
        private int oldYCoordinate;

        private string? selectedWeaponType;
        private readonly EnemyFactory enemyFactory;
        private EnemiesRepository enemies;
        private WeaponsRepository weapons;
        private MapsRepository maps;
        private UsersRepository users;
        private DataBuilder builder;
        private StringBuilder sb;
        private EnemiesCoordinatesRepository enemiesCoordinates;
        private Writer writer;
        private CommandManager commandManager;

        public Controller()
        {
            this.enemyFactory = new EnemyFactory();
            this.enemies = new EnemiesRepository();
            this.weapons = new WeaponsRepository();
            this.maps = new MapsRepository();
            this.users = new UsersRepository();
            this.builder = new DataBuilder();
            this.sb = new StringBuilder();
            this.writer = new Writer();
            enemiesCoordinates = new EnemiesCoordinatesRepository();
            commandManager = new CommandManager();
        }
        public Dictionary<Dictionary<int, int>, IEnemy> EnemiesCoordinates => enemiesCoordinates.Enemiescoordinates;
        public int EnemiesCoordinatesCount => enemiesCoordinates.Enemiescoordinates.Count; // Helper if needed
        public IMap? CurrentMap => maps.Models().FirstOrDefault();
        public void SetWeaponType(string weaponType) => selectedWeaponType = weaponType;
        public string GetReport() => sb.ToString().Trim();

        public double GetPlayerPoints(string username)
        {
            var user = users.Models().FirstOrDefault(u => u.Username == username);
            return user?.Points ?? 0;
        }

        public IUser GetOrCreateUser(string username)
        {
            var user = users.Models().FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                user = builder.CreateUser(username);
                users.AddNew(user);
            }
            return user;
        }

        public SessionState GetSessionState(string username)
        {
            IUser user = GetOrCreateUser(username);
            IMap? map = CurrentMap;

            var state = new SessionState
            {
                Username = username,
                WeaponType = selectedWeaponType ?? "Pistol",
                DamageDealt = user.DamageDealt,
                EnemiesKilled = user.EnemiesKilled,
                Points = user.Points,
                MapType = map?.GetType().Name ?? "DefaultMap",
                MapX = map?.X ?? 5,
                MapY = map?.Y ?? 5
            };

            foreach (var kvp in enemiesCoordinates.Enemiescoordinates)
            {
                var coords = kvp.Key;
                int row = coords.Keys.First();
                int col = coords[row];
                var enemy = kvp.Value;

                state.Enemies.Add(new EnemyState
                {
                    EnemyType = enemy.GetType().Name,
                    Life = enemy.Life,
                    IsAlreadyGenerated = enemy.IsAlreadyGenerated,
                    IsEnemyKilled = enemy.IsEnemyKilled,
                    Row = row,
                    Col = col
                });
            }

            return state;
        }

        public void LoadSessionState(SessionState state, IMap map)
        {
            // Reset existing collections manually or rebuild them
            // In typical scenario, Controller is fresh when calling this
            maps.AddNew(map);
            selectedWeaponType = state.WeaponType;

            IUser user = builder.CreateUser(state.Username);
            user.DamageDealt = state.DamageDealt;
            user.EnemiesKilled = state.EnemiesKilled;
            user.Points = state.Points;
            users.AddNew(user);

            foreach (var es in state.Enemies)
            {
                IEnemy enemy = enemyFactory.CreateEnemy(es.EnemyType);
                enemy.Life = es.Life;
                enemy.IsAlreadyGenerated = es.IsAlreadyGenerated;
                enemy.IsEnemyKilled = es.IsEnemyKilled;

                Dictionary<int, int> coords = new Dictionary<int, int> { { es.Row, es.Col } };
                enemies.AddNew(enemy);
                enemiesCoordinates.AddEnemy(coords, enemy);
            }

            map.GenerateTerrain();
            map.VisualizeMap(map.Terrain);
        }

        public string UndoLastAction()
        {
            if (commandManager.HasHistory)
            {
                commandManager.UndoPreviousCommand();
                
                IMap? map = maps.Models().FirstOrDefault();
                if (map != null)
                {
                    // Redraw map with restored enemy positions
                    map.VisualizeMap(map.Terrain);
                }
                
                return "Action undone.";
            }
            return "No more actions to undo.";
        }     

        public string GenerateEnemies(IMap map, int countOfEnemies)
        {
            map.GenerateTerrain();
            for (int i = 0; i < countOfEnemies; i++)
            {
                IEnemy generatedEnemy = enemyFactory.CreateRandomEnemy();
                enemies.AddNew(generatedEnemy);
                Dictionary<int, int> enemyCoordinates = Randomizer.EnemiesGenerationRandomizer(map);
                while (map.CoordinateIsAlreadyInhabitated(enemyCoordinates, enemiesCoordinates.Enemiescoordinates)) 
                {
                    enemyCoordinates = Randomizer.EnemiesGenerationRandomizer(map);
                }

                if(!map.CoordinateIsAlreadyInhabitated(enemyCoordinates, enemiesCoordinates.Enemiescoordinates))
                {                                                             
                    enemiesCoordinates.AddEnemy(enemyCoordinates, generatedEnemy);
                }                
               
            }
            maps.AddNew(map);
            map.VisualizeMap(map.Terrain);

            sb.AppendLine(string.Format(OutputMessages.MapWasAdded, map.GetType().Name));
            sb.AppendLine(string.Format(OutputMessages.EnemiesGenerated, countOfEnemies, map.GetType().Name));
            return sb.ToString().Trim();
        }

        public string Shoot(int xCoordinate, int yCoordinate, string username)
        {
            sb.Clear();

            IMap? map = maps.Models().FirstOrDefault();
            if (map == null)
            {
                throw new InvalidOperationException(ExceptionMessages.MapHasNotBeenAdded);
            }

            IWeapon weapon = selectedWeaponType != null
                ? builder.CreateWeapon(selectedWeaponType)
                : Randomizer.WeaponsRandomizer();
            
            IEnemy? enemy = ReturnEnemyFromCoordinates(xCoordinate, yCoordinate, enemiesCoordinates.Enemiescoordinates);
            weapons.AddNew(weapon);
            IUser user = GetOrCreateUser(username);

            map.Terrain[oldXCoordinate, oldYCoordinate] = "-";
            oldXCoordinate = xCoordinate;
            oldYCoordinate = yCoordinate;
            map.Terrain[xCoordinate, yCoordinate] = "+";
            
            var shootCommand = new ShootCommand(
                enemy, weapon, user, map, enemiesCoordinates, xCoordinate, yCoordinate);
            
            commandManager.ExecuteCommand(shootCommand);

            map.VisualizeMap(map.Terrain);
            return shootCommand.ResultMessage;
        }

        // Keep the old signature for compatibility, if called elsewhere without username
        public string Shoot(int xCoordinate, int yCoordinate)
        {
            return Shoot(xCoordinate, yCoordinate, "Default");
        }

        public void StatsUpdate(string username)
        {
            sb.Clear();
            IUser? user = users.Models().FirstOrDefault(u => u.Username == username);
            if (user == null)
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
        private IEnemy? ReturnEnemyFromCoordinates(int x, int y, Dictionary<Dictionary<int, int>, IEnemy> enemiesCoordinates)
        {
            IEnemy? enemy = null;
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

        public string Hint(int xCoordinate, int yCoordinate, string[,] terrain, Dictionary<Dictionary<int, int>, IEnemy> enemiesCoordinates)
        {
            sb.Clear();
            string closestEnemy = Hinter.GetHint(xCoordinate, yCoordinate, terrain, enemiesCoordinates);
            sb.AppendLine(closestEnemy);
            return sb.ToString().Trim();
        }
    }
}
