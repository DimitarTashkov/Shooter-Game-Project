using Shooter_Game0._1.Models.Weapons.Contracts;
using Shooter_Game0._1.Utilities.Messages;
using System;
using System.Text;
using System.Windows.Forms;

namespace Shooter_Game0._1.Models.Weapons.Models
{
    public abstract class Weapons : IWeapon
    {
        private double ammoType;
        private double power;
        public double damage;

        protected Weapons(double ammoType, double power)
        {
            this.ammoType = ammoType;
            this.power = power;
        }

        // Validation is optional — weapon stats are always valid by construction.
        public double AmmoType
        {
            get { return ammoType; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidWeaponsStats));
                ammoType = value;
            }
        }

        public double Power
        {
            get { return power; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidWeaponsStats));
                power = value;
            }
        }

        public double Damage
        {
            get { return damage; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidWeaponsStats));
                damage = value;
            }
        }

        public void CalculateDamage()
        {
            damage = ammoType * power;
            if (IsHeadShot())
                HeadShotDamage();
        }

        private void HeadShotDamage() => damage *= 2;

        public abstract bool IsHeadShot();

        // ── Phase 5: Polymorphic special action ───────────────────────────────
        /// <summary>
        /// Each weapon's unique mechanic. Returns false to block the current shot.
        /// </summary>
        public abstract bool SpecialAction(Form gameForm);

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Weapon name: {this.GetType().Name}");
            sb.AppendLine($"Weapon ammo type: {AmmoType}");
            sb.AppendLine($"Weapon power: {Power}");
            sb.AppendLine($"Weapon damage: {Damage}");
            sb.AppendLine($"Weapon dealt headshot: {IsHeadShot()}");
            return sb.ToString().Trim();
        }
    }
}
