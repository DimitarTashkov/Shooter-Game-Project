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
            return Randomizer.ShotgunHeadshotRandomizer() == HeadShotNumber;
        }


        public override bool SpecialAction(Form gameForm)
        {
            using var jamForm = new ShotgunJamForm();
            return jamForm.ShowDialog(gameForm) == DialogResult.OK;
        }
    }
}
