// File: Models/Weapons/Contracts/IWeapon.cs
using System.Windows.Forms;

namespace Shooter_Game0._1.Models.Weapons.Contracts
{
    public interface IWeapon
    {
        public double AmmoType { get; }
        public double Power    { get; }
        public double Damage   { get; }

        public void CalculateDamage();
        public abstract bool IsHeadShot();

        // ── Phase 5: Polymorphic weapon special action ─────────────────────────
        /// <summary>
        /// Triggers the weapon's unique special mechanic.
        /// Returns true  → the shot can proceed normally.
        /// Returns false → the shot is blocked this turn (e.g. Shotgun jam not cleared).
        /// NOTE: deviates from plan's 'void' to allow the Shotgun to block shots.
        /// </summary>
        public bool SpecialAction(Form gameForm);
    }
}
