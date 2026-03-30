using Shooter_Game0._1.Core.Contracts;
using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Models.Users.Contracts;
using Shooter_Game0._1.Models.Weapons.Contracts;
using Shooter_Game0._1.Repositories;
using Shooter_Game0._1.Utilities;
using Shooter_Game0._1.Utilities.Messages;
using System.Collections.Generic;
using System.Linq;

namespace Shooter_Game0._1.Core.Commands
{
    public class ShootCommand : ICommand
    {
        private IEnemy? enemy;
        private IWeapon weapon;
        private IUser user;
        private IMap map;
        private EnemiesCoordinatesRepository enemiesCoordinates;
        private readonly Difficulty difficulty;

        private int xCoordinate;
        private int yCoordinate;

        // State snapshots for undo
        private double previousEnemyLife;
        private bool previousEnemyGenerated;
        private bool previousEnemyKilled;
        private Dictionary<int, int>? previousEnemyCoordinates;

        private double previousUserDamage;
        private int previousUserKills;
        private double previousUserPoints;

        public string ResultMessage { get; private set; } = string.Empty;

        public ShootCommand(
            IEnemy? enemy,
            IWeapon weapon,
            IUser user,
            IMap map,
            EnemiesCoordinatesRepository enemiesCoordinates,
            int xCoordinate,
            int yCoordinate,
            Difficulty difficulty)
        {
            this.enemy = enemy;
            this.weapon = weapon;
            this.user = user;
            this.map = map;
            this.enemiesCoordinates = enemiesCoordinates;
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
            this.difficulty = difficulty;
        }

        public void Execute()
        {
            // Snapshot user state for undo
            previousUserDamage = user.DamageDealt;
            previousUserKills  = user.EnemiesKilled;
            previousUserPoints = user.Points;

            if (enemy == null)
            {
                ResultMessage = string.Format(OutputMessages.NoEnemyInThisLocation, xCoordinate, yCoordinate);
                return;
            }

            // Snapshot enemy state for undo
            previousEnemyLife      = enemy.Life;
            previousEnemyGenerated = enemy.IsAlreadyGenerated;
            previousEnemyKilled    = enemy.IsEnemyKilled;
            previousEnemyCoordinates = new Dictionary<int, int> { { xCoordinate, yCoordinate } };

            var aimCoordinates = new Dictionary<int, int> { { xCoordinate, yCoordinate } };

            weapon.CalculateDamage();
            double remainingLife = enemy.Life - weapon.Damage;

            var sb = new System.Text.StringBuilder();

            if (remainingLife > 0)
            {
                // ── Enemy survives the hit ────────────────────────────────────
                user.DamageDealt += weapon.Damage;
                enemy.Life       -= weapon.Damage;
                sb.AppendLine(string.Format(OutputMessages.EnemyWasShotFor,
                    enemy.GetType().Name, weapon.Damage, weapon.GetType().Name, xCoordinate, yCoordinate));
                sb.AppendLine(enemy.RegenHealth());

                enemiesCoordinates.RemoveEnemy(aimCoordinates);
                enemy.RunCoordinates(map, enemy, enemiesCoordinates.Enemiescoordinates);
            }
            else
            {
=                user.DamageDealt += weapon.Damage;

                bool reborn = enemy.TryRebirth(difficulty);
                if (reborn)
                {
                    // Enemy survived through rebirth — reposition on the map
                    sb.AppendLine($"☠ {enemy.GetType().Name} was nearly killed but REBORN with {System.Math.Round(enemy.Life)} HP!");
                    enemiesCoordinates.RemoveEnemy(aimCoordinates);
                    enemy.RunCoordinates(map, enemy, enemiesCoordinates.Enemiescoordinates);
                }
                else
                {
                    // Normal kill
                    enemiesCoordinates.RemoveEnemy(aimCoordinates);
                    user.EnemiesKilled++;
                    enemy.IsEnemyKilled = true;
                    sb.AppendLine(string.Format(OutputMessages.EnemyWasKilled,
                        enemy.GetType().Name, weapon.GetType().Name, xCoordinate, yCoordinate,
                        enemiesCoordinates.Enemiescoordinates.Count));
                }
            }

            // Recalculate points
            user.Points = (user.EnemiesKilled * 300) + (user.DamageDealt / 3);
            ResultMessage = sb.ToString().Trim();
        }

        public void Undo()
        {
            // Restore user state
            user.DamageDealt   = previousUserDamage;
            user.EnemiesKilled = previousUserKills;
            user.Points        = previousUserPoints;

            if (enemy == null || previousEnemyCoordinates == null) return;

            // Restore enemy state
            enemy.Life             = previousEnemyLife;
            enemy.IsAlreadyGenerated = previousEnemyGenerated;
            enemy.IsEnemyKilled    = previousEnemyKilled;

            // Find enemy's current position (might have moved or been removed)
            Dictionary<int, int>? currentCoords = enemiesCoordinates.Enemiescoordinates
                .FirstOrDefault(kvp => kvp.Value == enemy).Key;

            if (currentCoords != null)
                enemiesCoordinates.RemoveEnemy(currentCoords);

            // Restore enemy to original coordinates
            enemiesCoordinates.AddEnemy(previousEnemyCoordinates, enemy);
        }
    }
}
