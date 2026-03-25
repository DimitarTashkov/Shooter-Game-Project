// File: Models/Enemies/Models/Warrior.cs
using Shooter_Game0._1.Forms.MiniGames;
using Shooter_Game0._1.Utilities;
using Shooter_Game0._1.Utilities.Messages;
using System.Text;

namespace Shooter_Game0._1.Models.Enemies.Models
{
    /// <summary>
    /// Warrior – the Berserker. Mini-game: click the fast-moving target within 3 seconds.
    /// Phase 4 override: Warrior can only rebirth on HARD difficulty.
    /// </summary>
    public class Warrior : Enemy
    {
        private const int EnemySizeInfo = 30;
        private const double EnemyHealthInfo = 30;
        private const double retainFixedLifeForRegeneration = EnemySizeInfo * EnemyHealthInfo;

        public Warrior() : base(EnemySizeInfo, EnemyHealthInfo) { }

        // ── RegenHealth ────────────────────────────────────────────────────────

        public override string RegenHealth()
        {
            var sb = new StringBuilder();
            if (!IsAlreadyGenerated)
            {
                double lifeToRegen = retainFixedLifeForRegeneration / 10;
                Life += lifeToRegen;
                IsAlreadyGenerated = true;
                sb.AppendLine($"{this.GetType().Name} has regenerated {lifeToRegen}");
            }
            else
            {
                sb.AppendLine(string.Format(ExceptionMessages.EnemyHasAlreadyBeenRegenerated, this.GetType().Name));
            }
            return sb.ToString().Trim();
        }

        // ── Phase 2: SpecialMove ───────────────────────────────────────────────

        /// <summary>
        /// Opens the Warrior mini-game (hit the fast-moving berserker).
        /// Returns true if player clicked in time.
        /// Returns false if time ran out.
        /// </summary>
        public override bool SpecialMove(Difficulty difficulty)
        {
            using var form = new WarriorMiniGameForm(difficulty);
            return form.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        // ── Phase 4: TryRebirth override ──────────────────────────────────────

        /// <summary>
        /// Warrior is too powerful — can only rebirth on Hard difficulty.
        /// </summary>
        public override bool TryRebirth(Difficulty difficulty)
        {
            if (difficulty != Difficulty.Hard)
                return false;

            return base.TryRebirth(difficulty);
        }
    }
}
