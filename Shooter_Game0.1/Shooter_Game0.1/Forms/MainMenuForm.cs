namespace Shooter_Game0._1.Forms
{
    public class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Text = "Shooter Game";
            Size = new Size(500, 420);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.FromArgb(30, 30, 40);

            var titleLabel = new Label
            {
                Text = "SHOOTER GAME",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 180, 255),
                AutoSize = true,
                Location = new Point(90, 40)
            };

            var subtitleLabel = new Label
            {
                Text = "Console-to-WinForms Edition",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(140, 90)
            };

            var startButton = new Button
            {
                Text = "START GAME",
                Size = new Size(220, 50),
                Location = new Point(140, 160),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 120, 200),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            startButton.FlatAppearance.BorderSize = 0;
            startButton.Click += StartButton_Click;

            var exitButton = new Button
            {
                Text = "EXIT",
                Size = new Size(220, 50),
                Location = new Point(140, 250),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(180, 40, 40),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.Click += (s, e) => Application.Exit();

            Controls.AddRange([titleLabel, subtitleLabel, startButton, exitButton]);
        }

        private void StartButton_Click(object? sender, EventArgs e)
        {
            using var setupForm = new SetupForm();
            if (setupForm.ShowDialog() == DialogResult.OK)
            {
                Hide();
                var gameForm = new GameForm(setupForm.Username, setupForm.SelectedWeapon);
                gameForm.FormClosed += (s, args) => Show();
                gameForm.Show();
            }
        }
    }
}
