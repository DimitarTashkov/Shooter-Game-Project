using Shooter_Game0._1.Forms.MiniGames;
using Shooter_Game0._1.Utilities;
using Shooter_Game0._1.Utilities.Messages;
using System.Text;

namespace Shooter_Game0._1.Models.Enemies.Models
{

    public class Tank : Enemy
    {
        private const int EnemySizeInfo = 50;
        private const double EnemyHealthInfo = 80;
        private const double retainFixedLifeForRegeneration = EnemySizeInfo * EnemyHealthInfo;

        public Tank() : base(EnemySizeInfo, EnemyHealthInfo) { }


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

        public override bool SpecialMove(Difficulty difficulty, string weaponType)
        {
            using var form = new TankMiniGameForm(difficulty, weaponType);
            return form.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }


        public override bool TryRebirth(Difficulty difficulty)
        {
            if (difficulty != Difficulty.Hard)
                return false;

            return base.TryRebirth(difficulty);
        }
    }
}
