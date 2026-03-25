using Shooter_Game0._1.Forms.MiniGames;
using Shooter_Game0._1.Utilities;
using Shooter_Game0._1.Utilities.Messages;
using System.Text;

namespace Shooter_Game0._1.Models.Enemies.Models
{

    public class Orc : Enemy
    {
        private const int EnemySizeInfo = 15;
        private const double EnemyHealthInfo = 40;
        private const double retainFixedLifeForRegeneration = EnemySizeInfo * EnemyHealthInfo;

        public Orc() : base(EnemySizeInfo, EnemyHealthInfo) { }


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


        public override bool SpecialMove(Difficulty difficulty, string weaponType)
        {
            using var form = new OrcMiniGameForm(difficulty, weaponType);
            return form.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }
    }
}
