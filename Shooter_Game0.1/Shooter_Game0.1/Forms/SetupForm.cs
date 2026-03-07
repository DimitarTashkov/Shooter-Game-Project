namespace Shooter_Game0._1.Forms
{
    public class SetupForm : Form
    {
        private TextBox usernameTextBox = null!;
        private RadioButton rifleRadio = null!;
        private RadioButton shotgunRadio = null!;
        private RadioButton sniperRadio = null!;

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
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Text = "Game Setup";
            Size = new Size(420, 380);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(30, 30, 40);

            var usernameLabel = new Label
            {
                Text = "Enter Username:",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12),
                Location = new Point(30, 20),
                AutoSize = true
            };

            usernameTextBox = new TextBox
            {
                Location = new Point(30, 50),
                Size = new Size(340, 30),
                Font = new Font("Segoe UI", 12),
                BackColor = Color.FromArgb(50, 50, 60),
                ForeColor = Color.White
            };

            var weaponLabel = new Label
            {
                Text = "Select Weapon:",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12),
                Location = new Point(30, 100),
                AutoSize = true
            };

            rifleRadio = new RadioButton
            {
                Text = "Rifle  (20% headshot chance)",
                ForeColor = Color.LightGreen,
                Font = new Font("Segoe UI", 10),
                Location = new Point(50, 135),
                AutoSize = true,
                Checked = true
            };

            shotgunRadio = new RadioButton
            {
                Text = "Shotgun  (33% headshot chance)",
                ForeColor = Color.Orange,
                Font = new Font("Segoe UI", 10),
                Location = new Point(50, 168),
                AutoSize = true
            };

            sniperRadio = new RadioButton
            {
                Text = "Sniper  (10% headshot chance)",
                ForeColor = Color.Cyan,
                Font = new Font("Segoe UI", 10),
                Location = new Point(50, 201),
                AutoSize = true
            };

            var beginButton = new Button
            {
                Text = "BEGIN",
                Size = new Size(340, 50),
                Location = new Point(30, 260),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 150, 50),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            beginButton.FlatAppearance.BorderSize = 0;
            beginButton.Click += BeginButton_Click;

            Controls.AddRange([
                usernameLabel, usernameTextBox,
                weaponLabel, rifleRadio, shotgunRadio, sniperRadio,
                beginButton
            ]);
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
