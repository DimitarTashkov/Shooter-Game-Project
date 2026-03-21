using Shooter_Game0._1.Core.Contracts;
using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Models.Users.Contracts;
using Shooter_Game0._1.Models.Weapons.Contracts;
using Shooter_Game0._1.Repositories;
using Shooter_Game0._1.Utilities.Messages;

namespace Shooter_Game0._1.Core.Commands
{
    public class ShootCommand : ICommand
    {
        private IEnemy? enemy;
        private IWeapon weapon;
        private IUser user;
        private IMap map;
        private EnemiesCoordinatesRepository enemiesCoordinates;

        private int xCoordinate;
        private int yCoordinate;

        // State variables for undo
        private double previousEnemyLife;
        private bool previousEnemyGenerated;
        private bool previousEnemyKilled;
        private Dictionary<int, int>? previousEnemyCoordinates;

        private double previousUserDamage;
        private int previousUserKills;

        public string ResultMessage { get; private set; } = string.Empty;

        public ShootCommand(IEnemy? enemy, IWeapon weapon, IUser user, IMap map, EnemiesCoordinatesRepository enemiesCoordinates, int xCoordinate, int yCoordinate)
        {
            this.enemy = enemy;
            this.weapon = weapon;
            this.user = user;
            this.map = map;
            this.enemiesCoordinates = enemiesCoordinates;
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
        }

        public void Execute()
        {
            // Save user state
            previousUserDamage = user.DamageDealt;
            previousUserKills = user.EnemiesKilled;

            if (enemy == null)
            {
                ResultMessage = string.Format(OutputMessages.NoEnemyInThisLocation, xCoordinate, yCoordinate);
                return;
            }

            // Save enemy state
            previousEnemyLife = enemy.Life;
            previousEnemyGenerated = enemy.IsAlreadyGenerated;
            previousEnemyKilled = enemy.IsEnemyKilled;

            // Reconstruct current coordinates for backup
            previousEnemyCoordinates = new Dictionary<int, int> { { xCoordinate, yCoordinate } };

            Dictionary<int, int> aimCoordinates = new Dictionary<int, int> { { xCoordinate, yCoordinate } };

            weapon.CalculateDamage();
            double remain = weapon.Damage - enemy.Life;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (remain <= 0)
            {
                user.DamageDealt += weapon.Damage;
                enemy.Life -= weapon.Damage;
                sb.AppendLine(string.Format(OutputMessages.EnemyWasShotFor, enemy.GetType().Name, weapon.Damage, weapon.GetType().Name, xCoordinate, yCoordinate));
                sb.AppendLine(enemy.RegenHealth());

                enemiesCoordinates.RemoveEnemy(aimCoordinates);
                enemy.RunCoordinates(map, enemy, enemiesCoordinates.Enemiescoordinates);                   
            }
            else
            {
                user.DamageDealt += weapon.Damage; // Assuming we add full damage, or maybe life
                enemiesCoordinates.RemoveEnemy(aimCoordinates);

                user.EnemiesKilled++;
                enemy.IsEnemyKilled = true;

                sb.AppendLine(string.Format(OutputMessages.EnemyWasKilled, enemy.GetType().Name, weapon.GetType().Name, xCoordinate, yCoordinate, enemiesCoordinates.Enemiescoordinates.Count));
            }

            // Recalculate points
            user.Points = (user.EnemiesKilled * 300) + (user.DamageDealt / 3);
            ResultMessage = sb.ToString().Trim();
        }

        public void Undo()
        {
            // Revert User state
            user.DamageDealt = previousUserDamage;
            user.EnemiesKilled = previousUserKills;
            user.Points = (user.EnemiesKilled * 300) + (user.DamageDealt / 3);

            if (enemy == null || previousEnemyCoordinates == null) return;

            // Revert Enemy state
            enemy.Life = previousEnemyLife;
            enemy.IsAlreadyGenerated = previousEnemyGenerated;
            enemy.IsEnemyKilled = previousEnemyKilled;

            // Find where enemy is now, and remove it from there (if it wasn't killed)
            // If it was killed, it's not in the dictionary.

            // To properly remove current coordinates, we need to find it:
            Dictionary<int, int>? currentEnemyCoords = null;
            foreach (var kvp in enemiesCoordinates.Enemiescoordinates)
            {
                if (kvp.Value == enemy)
                {
                    currentEnemyCoords = kvp.Key;
                    break;
                }
            }

            if (currentEnemyCoords != null)
            {
                enemiesCoordinates.RemoveEnemy(currentEnemyCoords);
            }

            // Put it back to old coordinates
            enemiesCoordinates.AddEnemy(previousEnemyCoordinates, enemy);

            // Revert the terrain cell if needed, though MapPanel renders from coords anyway.
        }
    }
}
