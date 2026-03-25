// File: Models/Weapons/Models/Shotgun.cs
using Shooter_Game0._1.Forms.MiniGames;
using Shooter_Game0._1.Utilities.Randomizer;
using System.Windows.Forms;

namespace Shooter_Game0._1.Models.Weapons.Models
{
    public class Shotgun : Weapons
    {
        private const double AmmoTypeInfo = 9;
        private const double PowerInfo    = 33;
        private const int HeadShotNumber  = 1;

        public Shotgun() : base(AmmoTypeInfo, PowerInfo) { }

        public override bool IsHeadShot()
        {
            // 33% chance for headshot
            return Randomizer.ShotgunHeadshotRandomizer() == HeadShotNumber;
        }

        // ── Phase 5: Jam Mechanic ─────────────────────────────────────────────

        /// <summary>
        /// Shotgun jam: opens the ShotgunJamForm where the player must press SPACE 5×.
        /// Returns true  → jam cleared, shot proceeds.
        /// Returns false → jam not cleared, shot is blocked this turn.
        /// </summary>
        public override bool SpecialAction(Form gameForm)
        {
            using var jamForm = new ShotgunJamForm();
            return jamForm.ShowDialog(gameForm) == DialogResult.OK;
        }
    }
}
