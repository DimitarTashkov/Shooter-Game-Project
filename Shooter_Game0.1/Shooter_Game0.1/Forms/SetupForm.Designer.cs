// File: Forms/SetupForm.Designer.cs
namespace Shooter_Game0._1.Forms
{
    partial class SetupForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label usernameLabel;
        private TextBox usernameTextBox;
        private Label weaponLabel;
        private RadioButton rifleRadio;
        private RadioButton shotgunRadio;
        private RadioButton sniperRadio;
        private Label difficultyLabel;
        private RadioButton easyRadio;
        private RadioButton mediumRadio;
        private RadioButton hardRadio;
        private Button beginButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            usernameLabel   = new Label();
            usernameTextBox = new TextBox();
            weaponLabel     = new Label();
            rifleRadio      = new RadioButton();
            shotgunRadio    = new RadioButton();
            sniperRadio     = new RadioButton();
            difficultyLabel = new Label();
            easyRadio       = new RadioButton();
            mediumRadio     = new RadioButton();
            hardRadio       = new RadioButton();
            beginButton     = new Button();
            SuspendLayout();

            // ── usernameLabel ──────────────────────────────────────────────────
            usernameLabel.AutoSize = true;
            usernameLabel.Font = new Font("Segoe UI", 12F);
            usernameLabel.ForeColor = Color.White;
            usernameLabel.Location = new Point(30, 20);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Text = "Enter Username:";

            // ── usernameTextBox ────────────────────────────────────────────────
            usernameTextBox.BackColor = Color.FromArgb(50, 50, 60);
            usernameTextBox.Font = new Font("Segoe UI", 12F);
            usernameTextBox.ForeColor = Color.White;
            usernameTextBox.Location = new Point(30, 48);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(340, 29);

            // ── weaponLabel ────────────────────────────────────────────────────
            weaponLabel.AutoSize = true;
            weaponLabel.Font = new Font("Segoe UI", 12F);
            weaponLabel.ForeColor = Color.White;
            weaponLabel.Location = new Point(30, 96);
            weaponLabel.Name = "weaponLabel";
            weaponLabel.Text = "Select Weapon:";

            // ── rifleRadio ─────────────────────────────────────────────────────
            rifleRadio.AutoSize = true;
            rifleRadio.Checked = true;
            rifleRadio.Font = new Font("Segoe UI", 10F);
            rifleRadio.ForeColor = Color.LightGreen;
            rifleRadio.Location = new Point(50, 128);
            rifleRadio.Name = "rifleRadio";
            rifleRadio.Text = "Rifle  (20% headshot chance)";

            // ── shotgunRadio ───────────────────────────────────────────────────
            shotgunRadio.AutoSize = true;
            shotgunRadio.Font = new Font("Segoe UI", 10F);
            shotgunRadio.ForeColor = Color.Orange;
            shotgunRadio.Location = new Point(50, 158);
            shotgunRadio.Name = "shotgunRadio";
            shotgunRadio.Text = "Shotgun  (33% headshot chance)";

            // ── sniperRadio ────────────────────────────────────────────────────
            sniperRadio.AutoSize = true;
            sniperRadio.Font = new Font("Segoe UI", 10F);
            sniperRadio.ForeColor = Color.Cyan;
            sniperRadio.Location = new Point(50, 188);
            sniperRadio.Name = "sniperRadio";
            sniperRadio.Text = "Sniper  (10% headshot chance)";

            // ── difficultyLabel ────────────────────────────────────────────────
            difficultyLabel.AutoSize = true;
            difficultyLabel.Font = new Font("Segoe UI", 12F);
            difficultyLabel.ForeColor = Color.White;
            difficultyLabel.Location = new Point(30, 228);
            difficultyLabel.Name = "difficultyLabel";
            difficultyLabel.Text = "Select Difficulty:";

            // ── easyRadio ─────────────────────────────────────────────────────
            easyRadio.AutoSize = true;
            easyRadio.Checked = true;
            easyRadio.Font = new Font("Segoe UI", 10F);
            easyRadio.ForeColor = Color.LightGreen;
            easyRadio.Location = new Point(50, 258);
            easyRadio.Name = "easyRadio";
            easyRadio.Text = "Easy   (10% enemy rebirth / 30% mini-game)";

            // ── mediumRadio ────────────────────────────────────────────────────
            mediumRadio.AutoSize = true;
            mediumRadio.Font = new Font("Segoe UI", 10F);
            mediumRadio.ForeColor = Color.Yellow;
            mediumRadio.Location = new Point(50, 288);
            mediumRadio.Name = "mediumRadio";
            mediumRadio.Text = "Medium (25% enemy rebirth / 30% mini-game)";

            // ── hardRadio ─────────────────────────────────────────────────────
            hardRadio.AutoSize = true;
            hardRadio.Font = new Font("Segoe UI", 10F);
            hardRadio.ForeColor = Color.OrangeRed;
            hardRadio.Location = new Point(50, 318);
            hardRadio.Name = "hardRadio";
            hardRadio.Text = "Hard   (40% rebirth / blackout mini-games!)";

            // ── beginButton ────────────────────────────────────────────────────
            beginButton.BackColor = Color.FromArgb(0, 150, 50);
            beginButton.Cursor = Cursors.Hand;
            beginButton.FlatAppearance.BorderSize = 0;
            beginButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 180, 70);
            beginButton.FlatStyle = FlatStyle.Flat;
            beginButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            beginButton.ForeColor = Color.White;
            beginButton.Location = new Point(30, 360);
            beginButton.Name = "beginButton";
            beginButton.Size = new Size(340, 50);
            beginButton.Text = "BEGIN";
            beginButton.UseVisualStyleBackColor = false;
            beginButton.Click += BeginButton_Click;

            // ── SetupForm ──────────────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 40);
            ClientSize = new Size(404, 430);
            Controls.Add(beginButton);
            Controls.Add(hardRadio);
            Controls.Add(mediumRadio);
            Controls.Add(easyRadio);
            Controls.Add(difficultyLabel);
            Controls.Add(sniperRadio);
            Controls.Add(shotgunRadio);
            Controls.Add(rifleRadio);
            Controls.Add(weaponLabel);
            Controls.Add(usernameTextBox);
            Controls.Add(usernameLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SetupForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Game Setup";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
