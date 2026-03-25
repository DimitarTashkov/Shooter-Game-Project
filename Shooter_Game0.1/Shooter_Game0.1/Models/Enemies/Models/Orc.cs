// File: Models/Enemies/Models/Orc.cs
using Shooter_Game0._1.Forms.MiniGames;
using Shooter_Game0._1.Utilities;
using Shooter_Game0._1.Utilities.Messages;
using System.Text;

namespace Shooter_Game0._1.Models.Enemies.Models
{
    /// <summary>
    /// Orc – the Thief. Mini-game: click the moving target within 3 seconds.
    /// Rebirth uses base logic (all difficulties eligible).
    /// </summary>
    public class Orc : Enemy
    {
        private const int EnemySizeInfo = 15;
        private const double EnemyHealthInfo = 40;
        private const double retainFixedLifeForRegeneration = EnemySizeInfo * EnemyHealthInfo;

        public Orc() : base(EnemySizeInfo, EnemyHealthInfo) { }

        // ── RegenHealth ────────────────────────────────────────────────────────

        public override string RegenHealth()
        {
            var sb = new StringBuilder();
            if (!IsAlreadyGenerated)
            {
                double lifeToRegen = (retainFixedLifeForRegeneration / 10) * 3;
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
        /// Opens the Orc mini-game (catch the moving thief).
        /// Returns true if the player clicked the target in time (damage is applied).
        /// Returns false if time ran out (Orc steals points).
        /// </summary>
        public override bool SpecialMove(Difficulty difficulty)
        {
            using var form = new OrcMiniGameForm(difficulty);
            return form.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }
    }
}
