using Shooter_Game0._1.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Models.Enemies.Models
{
    public class Warrior : Enemies
    {
        private const int EnemySizeInfo = 30;
        private const double EnemyHealthInfo = 30;
        private const double retainFixedLifeForRegeneration = EnemySizeInfo * EnemyHealthInfo;
        public Warrior() : base(EnemySizeInfo, EnemyHealthInfo)
        {
        }

        public override string RegenHealth()
        {
            StringBuilder sb = new StringBuilder();
            if (!IsAlreadyGenerated)
            {
                double lifetoRegenerate = (retainFixedLifeForRegeneration / 10);
                Life += lifetoRegenerate;
                IsAlreadyGenerated = true;
                sb.AppendLine($"{this.GetType().Name} has regenerated {lifetoRegenerate}");
            }
            else
            {
                sb.AppendLine(string.Format(ExceptionMessages.EnemyHasAlreadyBeenRegenerated, this.GetType().Name));
            }
            return sb.ToString().Trim();
        }
    }
}
