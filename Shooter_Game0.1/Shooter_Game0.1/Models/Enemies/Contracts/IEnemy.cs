// File: Models/Enemies/Contracts/IEnemy.cs
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Utilities;
using System.Collections.Generic;

namespace Shooter_Game0._1.Models.Enemies.Contracts
{
    public interface IEnemy
    {
        public int EnemySize { get; }
        public double EnemyHealth { get; }
        public double Life { get; set; }
        public bool IsAlreadyGenerated { get; set; }
        public bool IsEnemyKilled { get; set; }

        public abstract string RegenHealth();
        public void RunCoordinates(IMap map, IEnemy enemy, Dictionary<Dictionary<int, int>, IEnemy> enemiesCoordinates);

        /// <summary>
        /// Polymorphic mini-game triggered on a successful hit (30% chance).
        /// Returns true if the player wins the mini-game (damage is applied),
        /// false if the player loses (penalty applied, no damage).
        /// </summary>
        public abstract bool SpecialMove(Difficulty difficulty);

        /// <summary>
        /// Attempts to revive the enemy after death.
        /// Chance depends on difficulty: Easy=10%, Medium=25%, Hard=40%.
        /// On success, life is restored to 25% of initial value and true is returned.
        /// </summary>
        public abstract bool TryRebirth(Difficulty difficulty);
    }
}
