// File: Core/Controller.cs
using Shooter_Game0._1.Core.Contracts;
using Shooter_Game0._1.Factories;
using Shooter_Game0._1.IO;
using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Models.Users.Contracts;
using Shooter_Game0._1.Models.Weapons.Contracts;
using Shooter_Game0._1.Repositories;
using Shooter_Game0._1.Utilities;
using Shooter_Game0._1.Utilities.Hinter;
using Shooter_Game0._1.Utilities.Messages;
using Shooter_Game0._1.Utilities.Randomizer;
using Shooter_Game0._1.Core.Commands;
using Shooter_Game0._1.Models.SaveData;
using System;
using System.Collections.Generic;
using System.Linq;
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
            this.enemyFactory        = new EnemyFactory();
            this.enemies             = new EnemiesRepository();
            this.weapons             = new WeaponsRepository();
            this.maps                = new MapsRepository();
            this.users               = new UsersRepository();
            this.builder             = new DataBuilder();
            this.sb                  = new StringBuilder();
            this.writer              = new Writer();
            this.enemiesCoordinates  = new EnemiesCoordinatesRepository();
            this.commandManager      = new CommandManager();
        }

        public Dictionary<Dictionary<int, int>, IEnemy> EnemiesCoordinates => enemiesCoordinates.Enemiescoordinates;
        public int EnemiesCoordinatesCount => enemiesCoordinates.Enemiescoordinates.Count;
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

        // ── Session persistence ───────────────────────────────────────────────

        public SessionState GetSessionState(string username, Difficulty difficulty)
        {
            IUser user = GetOrCreateUser(username);
            IMap? map  = CurrentMap;

            var state = new SessionState
            {
                Username    = username,
                WeaponType  = selectedWeaponType ?? "Rifle",
                Difficulty  = difficulty,
                DamageDealt = user.DamageDealt,
                EnemiesKilled = user.EnemiesKilled,
                Points      = user.Points,
                MapType     = map?.GetType().Name ?? "DefaultMap",
                MapX        = map?.X ?? 5,
                MapY        = map?.Y ?? 5
            };

            state.Enemies = enemiesCoordinates.Enemiescoordinates.Select(kvp => new EnemyState
            {
                EnemyType        = kvp.Value.GetType().Name,
                Life             = kvp.Value.Life,
                IsAlreadyGenerated = kvp.Value.IsAlreadyGenerated,
                IsEnemyKilled    = kvp.Value.IsEnemyKilled,
                Row              = kvp.Key.Keys.First(),
                Col              = kvp.Key.Values.First()
            }).ToList();

            return state;
        }

        public void LoadSessionState(SessionState state, IMap map)
        {
            maps.AddNew(map);
            selectedWeaponType = state.WeaponType;

            IUser user = builder.CreateUser(state.Username);
            user.DamageDealt   = state.DamageDealt;
            user.EnemiesKilled = state.EnemiesKilled;
            user.Points        = state.Points;
            users.AddNew(user);

            foreach (var es in state.Enemies)
            {
                IEnemy enemy = enemyFactory.CreateEnemy(es.EnemyType);
                enemy.Life             = es.Life;
                enemy.IsAlreadyGenerated = es.IsAlreadyGenerated;
                enemy.IsEnemyKilled    = es.IsEnemyKilled;

                var coords = new Dictionary<int, int> { { es.Row, es.Col } };
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
                map?.VisualizeMap(map.Terrain);
                return "Action undone.";
            }
            return "No more actions to undo.";
        }

        // ── Enemy generation ──────────────────────────────────────────────────

        public string GenerateEnemies(IMap map, int countOfEnemies)
        {
            map.GenerateTerrain();
            for (int i = 0; i < countOfEnemies; i++)
            {
                IEnemy generatedEnemy = enemyFactory.CreateRandomEnemy();
                enemies.AddNew(generatedEnemy);

                Dictionary<int, int> enemyCoords = Randomizer.EnemiesGenerationRandomizer(map);
                while (map.CoordinateIsAlreadyInhabitated(enemyCoords, enemiesCoordinates.Enemiescoordinates))
                    enemyCoords = Randomizer.EnemiesGenerationRandomizer(map);

                if (!map.CoordinateIsAlreadyInhabitated(enemyCoords, enemiesCoordinates.Enemiescoordinates))
                    enemiesCoordinates.AddEnemy(enemyCoords, generatedEnemy);
            }
            maps.AddNew(map);
            map.VisualizeMap(map.Terrain);

            sb.AppendLine(string.Format(OutputMessages.MapWasAdded, map.GetType().Name));
            sb.AppendLine(string.Format(OutputMessages.EnemiesGenerated, countOfEnemies, map.GetType().Name));
            return sb.ToString().Trim();
        }

        // ── Shoot ─────────────────────────────────────────────────────────────

        /// <summary>Primary overload — includes difficulty for rebirth & special-move logic.</summary>
        public string Shoot(int xCoordinate, int yCoordinate, string username, Difficulty difficulty)
        {
            sb.Clear();

            IMap? map = maps.Models().FirstOrDefault();
            if (map == null)
                throw new InvalidOperationException(ExceptionMessages.MapHasNotBeenAdded);

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
                enemy, weapon, user, map, enemiesCoordinates,
                xCoordinate, yCoordinate, difficulty);

            commandManager.ExecuteCommand(shootCommand);

            map.VisualizeMap(map.Terrain);
            return shootCommand.ResultMessage;
        }

        /// <summary>Backward-compatible overloads.</summary>
        public string Shoot(int xCoordinate, int yCoordinate, string username)
            => Shoot(xCoordinate, yCoordinate, username, Difficulty.Easy);

        public string Shoot(int xCoordinate, int yCoordinate)
            => Shoot(xCoordinate, yCoordinate, "Default", Difficulty.Easy);

        // ── Stats ─────────────────────────────────────────────────────────────

        public void StatsUpdate(string username)
        {
            sb.Clear();
            IUser? user = users.Models().FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                user = builder.CreateUser(username);
                users.AddNew(user);
            }
            user.DamageDealt   += collectDealtDamage;
            user.EnemiesKilled += collectKills;
            user.Points = (user.EnemiesKilled * 300) + (user.DamageDealt / 3);
            sb.AppendLine(string.Format(OutputMessages.UserReport,
                username, user.DamageDealt, user.EnemiesKilled, Math.Round(user.Points, 2)));
        }

        public void Report() => writer.WriteLine(sb.ToString().Trim());

        // ── Hint ──────────────────────────────────────────────────────────────

        public string Hint(int xCoordinate, int yCoordinate, string[,] terrain,
            Dictionary<Dictionary<int, int>, IEnemy> enemiesCoords)
        {
            sb.Clear();
            sb.AppendLine(Hinter.GetHint(xCoordinate, yCoordinate, terrain, enemiesCoords));
            return sb.ToString().Trim();
        }

        // ── Private helpers ───────────────────────────────────────────────────

        private IEnemy? ReturnEnemyFromCoordinates(int x, int y,
            Dictionary<Dictionary<int, int>, IEnemy> coords)
        {
            return coords.FirstOrDefault(kvp => kvp.Key.ContainsKey(x) && kvp.Key[x] == y).Value;
        }
    }
}
