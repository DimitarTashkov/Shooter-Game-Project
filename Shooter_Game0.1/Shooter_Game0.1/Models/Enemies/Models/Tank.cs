// File: Models/Enemies/Models/Tank.cs
using Shooter_Game0._1.Forms.MiniGames;
using Shooter_Game0._1.Utilities;
using Shooter_Game0._1.Utilities.Messages;
using System.Text;

namespace Shooter_Game0._1.Models.Enemies.Models
{
    /// <summary>
    /// Tank – the Armored. Mini-game: click shields in order (small → medium → large).
    /// Phase 4 override: Tank can only rebirth on HARD difficulty.
    /// </summary>
    public class Tank : Enemy
    {
        private const int EnemySizeInfo = 50;
        private const double EnemyHealthInfo = 80;
        private const double retainFixedLifeForRegeneration = EnemySizeInfo * EnemyHealthInfo;

        public Tank() : base(EnemySizeInfo, EnemyHealthInfo) { }

        // ── RegenHealth ────────────────────────────────────────────────────────

        public override string RegenHealth()
        {
            var sb = new StringBuilder();
            if (!IsAlreadyGenerated)
            {
                double lifeToRegen = (retainFixedLifeForRegeneration / 10) * 4;
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
        /// Opens the Tank mini-game (click shields in order).
        /// Returns true if all three shields are clicked in the correct sequence.
        /// Returns false on wrong order or timeout.
        /// </summary>
        public override bool SpecialMove(Difficulty difficulty)
        {
            using var form = new TankMiniGameForm(difficulty);
            return form.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        // ── Phase 4: TryRebirth override ──────────────────────────────────────

        /// <summary>
        /// Tank is too powerful — can only rebirth on Hard difficulty.
        /// </summary>
        public override bool TryRebirth(Difficulty difficulty)
        {
            if (difficulty != Difficulty.Hard)
                return false;

            return base.TryRebirth(difficulty);
        }
    }
}
