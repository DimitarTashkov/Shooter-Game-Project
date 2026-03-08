using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Enemies.Models;

namespace Shooter_Game0._1.Factories
{
    /// <summary>
    /// Factory Method Pattern: Encapsulates enemy creation logic.
    /// Replaces scattered <c>new Orc()</c> / <c>new Tank()</c> calls
    /// with a single factory method.
    /// </summary>
    public class EnemyFactory
    {
        public IEnemy CreateEnemy(string type)
        {
            return type switch
            {
                nameof(Orc) => new Orc(),
                nameof(Tank) => new Tank(),
                nameof(Warrior) => new Warrior(),
                nameof(Wizard) => new Wizard(),
                _ => throw new ArgumentException($"Unknown enemy type: {type}")
            };
        }

        public IEnemy CreateRandomEnemy()
        {
            string[] types = [nameof(Orc), nameof(Tank), nameof(Warrior), nameof(Wizard)];
            int index = Random.Shared.Next(types.Length);
            return CreateEnemy(types[index]);
        }
    }
}
