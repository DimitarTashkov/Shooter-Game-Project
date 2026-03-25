using Shooter_Game0._1.Utilities.Randomizer;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shooter_Game0._1.Models.Weapons.Models
{
    public class Rifle : Weapons
    {
        private const double AmmoTypeInfo  = 22;
        private const double PowerInfo     = 20;
        private const int HeadShotNumber   = 1;

        private static readonly Random _rng = new Random();

        public Rifle() : base(AmmoTypeInfo, PowerInfo) { }

        public override bool IsHeadShot()
        {
            return Randomizer.RifleHeadshotRandomizer() == HeadShotNumber;
        }


        public override bool SpecialAction(Form gameForm)
        {
            var pos = Cursor.Position;
            int shakeX = _rng.Next(-10, 11);
            int shakeY = _rng.Next(-10, 11);
            Cursor.Position = new Point(pos.X + shakeX, pos.Y + shakeY);

            var recoilLabel = new Label
            {
                Text = "⚡ RECOIL!",
                ForeColor = Color.OrangeRed,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(
                    gameForm.ClientSize.Width / 2 - 50,
                    gameForm.ClientSize.Height / 2 - 20)
            };
            gameForm.Controls.Add(recoilLabel);
            recoilLabel.BringToFront();

            var fadeTimer = new System.Windows.Forms.Timer { Interval = 600 };
            fadeTimer.Tick += (s, e) =>
            {
                fadeTimer.Stop();
                fadeTimer.Dispose();
                if (gameForm.Controls.Contains(recoilLabel))
                    gameForm.Controls.Remove(recoilLabel);
                recoilLabel.Dispose();
            };
            fadeTimer.Start();

            return true;
        }
    }
}
