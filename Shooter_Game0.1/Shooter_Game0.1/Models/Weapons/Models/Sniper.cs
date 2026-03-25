// File: Models/Weapons/Models/Sniper.cs
using Shooter_Game0._1.Utilities.Randomizer;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shooter_Game0._1.Models.Weapons.Models
{
    public class Sniper : Weapons
    {
        private const double AmmoTypeInfo = 1;
        private const double PowerInfo    = 800;
        private const int HeadShotNumber  = 1;

        public Sniper() : base(AmmoTypeInfo, PowerInfo) { }

        public override bool IsHeadShot()
        {
            // 10% chance for headshot
            return Randomizer.SniperHeadshotRandomizer() == HeadShotNumber;
        }

        // ── Phase 5: Hold Breath ──────────────────────────────────────────────

        /// <summary>
        /// Sniper hold-breath: displays a "HOLDING BREATH – AIM STEADY" overlay
        /// on the game form for 2 seconds, then signals that the next shot has a
        /// +50% accuracy bonus (communicated via a label in the game form UI).
        /// Returns true (shooting is not blocked; hold-breath enhances the next shot).
        /// </summary>
        public override bool SpecialAction(Form gameForm)
        {
            // Remove any existing overlay first
            foreach (Control c in gameForm.Controls)
            {
                if (c.Tag is string t && t == "SniperBreath")
                {
                    gameForm.Controls.Remove(c);
                    c.Dispose();
                    break;
                }
            }

            int w = 380;
            int h = 44;
            var overlay = new Label
            {
                Text = "★ HOLDING BREATH — AIM STEADY (+50% accuracy) ★",
                ForeColor = Color.Cyan,
                BackColor = Color.FromArgb(160, 0, 0, 0),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(w, h),
                Location = new Point(
                    (gameForm.ClientSize.Width - w) / 2,
                    gameForm.ClientSize.Height / 2 - h / 2),
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = "SniperBreath"
            };
            gameForm.Controls.Add(overlay);
            overlay.BringToFront();

            var removeTimer = new System.Windows.Forms.Timer { Interval = 2000 };
            removeTimer.Tick += (s, e) =>
            {
                removeTimer.Stop();
                removeTimer.Dispose();
                if (!gameForm.IsDisposed && gameForm.Controls.Contains(overlay))
                    gameForm.Controls.Remove(overlay);
                overlay.Dispose();
            };
            removeTimer.Start();

            return true;
        }
    }
}
