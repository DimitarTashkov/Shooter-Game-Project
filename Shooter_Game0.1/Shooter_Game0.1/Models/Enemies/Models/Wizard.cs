// File: Models/Enemies/Models/Wizard.cs
using Shooter_Game0._1.Forms.MiniGames;
using Shooter_Game0._1.Utilities;
using Shooter_Game0._1.Utilities.Messages;
using System.Text;

namespace Shooter_Game0._1.Models.Enemies.Models
{
    /// <summary>
    /// Wizard – the Illusionist. Mini-game: choose the real Wizard among 3 targets.
    /// Rebirth uses base logic (all difficulties eligible).
    /// </summary>
    public class Wizard : Enemy
    {
        private const int EnemySizeInfo = 25;
        private const double EnemyHealthInfo = 50;
        private const double retainFixedLifeForRegeneration = EnemySizeInfo * EnemyHealthInfo;

        public Wizard() : base(EnemySizeInfo, EnemyHealthInfo) { }

        // ── RegenHealth ────────────────────────────────────────────────────────

        public override string RegenHealth()
        {
            var sb = new StringBuilder();
            if (!IsAlreadyGenerated)
            {
                double lifeToRegen = (retainFixedLifeForRegeneration / 10) * 2;
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
        /// Opens the Wizard mini-game (find the real wizard).
        /// Returns true if correct target chosen (damage applied).
        /// Returns false if wrong target or time expired (penalty applied).
        /// </summary>
        public override bool SpecialMove(Difficulty difficulty)
        {
            using var form = new WizardMiniGameForm(difficulty);
            return form.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }
    }
}
