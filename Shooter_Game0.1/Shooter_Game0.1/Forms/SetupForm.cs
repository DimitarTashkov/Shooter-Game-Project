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
                if (sniperRadio.Checked) return "Sniper";
                return "Rifle";
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
