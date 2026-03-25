// File: Forms/SetupForm.cs
using Shooter_Game0._1.Utilities;

namespace Shooter_Game0._1.Forms
{
    public partial class SetupForm : Form
    {
        public string Username => usernameTextBox.Text.Trim();

        public string SelectedWeapon
        {
            get
            {
                if (shotgunRadio.Checked) return "Shotgun";
                if (sniperRadio.Checked)  return "Sniper";
                return "Rifle";
            }
        }

        public Difficulty SelectedDifficulty
        {
            get
            {
                if (hardRadio.Checked)   return Difficulty.Hard;
                if (mediumRadio.Checked) return Difficulty.Medium;
                return Difficulty.Easy;
            }
        }

        public SetupForm()
        {
            InitializeComponent();
        }

        private void BeginButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text))
            {
                MessageBox.Show("Please enter a valid username!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
