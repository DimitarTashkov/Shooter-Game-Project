// File: Forms/SetupForm.Designer.cs
namespace Shooter_Game0._1.Forms
{
    partial class SetupForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label     usernameLabel;
        private TextBox   usernameTextBox;
        private GroupBox  weaponGroup;
        private RadioButton rifleRadio;
        private RadioButton shotgunRadio;
        private RadioButton sniperRadio;
        private GroupBox  difficultyGroup;
        private RadioButton easyRadio;
        private RadioButton mediumRadio;
        private RadioButton hardRadio;
        private Button    beginButton;

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
            weaponGroup     = new GroupBox();
            rifleRadio      = new RadioButton();
            shotgunRadio    = new RadioButton();
            sniperRadio     = new RadioButton();
            difficultyGroup = new GroupBox();
            easyRadio       = new RadioButton();
            mediumRadio     = new RadioButton();
            hardRadio       = new RadioButton();
            beginButton     = new Button();
            weaponGroup.SuspendLayout();
            difficultyGroup.SuspendLayout();
            SuspendLayout();

            // ── usernameLabel ──────────────────────────────────────────────────
            usernameLabel.AutoSize  = true;
            usernameLabel.Font      = new Font("Segoe UI", 12F);
            usernameLabel.ForeColor = Color.White;
            usernameLabel.Location  = new Point(15, 15);
            usernameLabel.Name      = "usernameLabel";
            usernameLabel.Text      = "Enter Username:";

            // ── usernameTextBox ────────────────────────────────────────────────
            usernameTextBox.BackColor = Color.FromArgb(50, 50, 60);
            usernameTextBox.Font      = new Font("Segoe UI", 12F);
            usernameTextBox.ForeColor = Color.White;
            usernameTextBox.Location  = new Point(15, 42);
            usernameTextBox.Name      = "usernameTextBox";
            usernameTextBox.Size      = new Size(370, 29);

            // ── rifleRadio ─────────────────────────────────────────────────────
            rifleRadio.AutoSize  = true;
            rifleRadio.Checked   = true;
            rifleRadio.Font      = new Font("Segoe UI", 10F);
            rifleRadio.ForeColor = Color.LightGreen;
            rifleRadio.Location  = new Point(15, 26);
            rifleRadio.Name      = "rifleRadio";
            rifleRadio.Text      = "Rifle  (20% headshot chance)";

            // ── shotgunRadio ───────────────────────────────────────────────────
            shotgunRadio.AutoSize  = true;
            shotgunRadio.Font      = new Font("Segoe UI", 10F);
            shotgunRadio.ForeColor = Color.Orange;
            shotgunRadio.Location  = new Point(15, 54);
            shotgunRadio.Name      = "shotgunRadio";
            shotgunRadio.Text      = "Shotgun  (33% headshot chance)";

            // ── sniperRadio ────────────────────────────────────────────────────
            sniperRadio.AutoSize  = true;
            sniperRadio.Font      = new Font("Segoe UI", 10F);
            sniperRadio.ForeColor = Color.Cyan;
            sniperRadio.Location  = new Point(15, 82);
            sniperRadio.Name      = "sniperRadio";
            sniperRadio.Text      = "Sniper  (10% headshot chance)";

            // ── weaponGroup ────────────────────────────────────────────────────
            weaponGroup.BackColor = Color.FromArgb(30, 30, 40);
            weaponGroup.Controls.Add(rifleRadio);
            weaponGroup.Controls.Add(shotgunRadio);
            weaponGroup.Controls.Add(sniperRadio);
            weaponGroup.Font      = new Font("Segoe UI", 11F, FontStyle.Bold);
            weaponGroup.ForeColor = Color.White;
            weaponGroup.Location  = new Point(15, 88);
            weaponGroup.Name      = "weaponGroup";
            weaponGroup.Size      = new Size(370, 113);
            weaponGroup.Text      = "Select Weapon";

            // ── easyRadio ─────────────────────────────────────────────────────
            easyRadio.AutoSize  = true;
            easyRadio.Checked   = true;
            easyRadio.Font      = new Font("Segoe UI", 10F);
            easyRadio.ForeColor = Color.LightGreen;
            easyRadio.Location  = new Point(15, 26);
            easyRadio.Name      = "easyRadio";
            easyRadio.Text      = "Easy   (10% rebirth / mini-games enabled)";

            // ── mediumRadio ────────────────────────────────────────────────────
            mediumRadio.AutoSize  = true;
            mediumRadio.Font      = new Font("Segoe UI", 10F);
            mediumRadio.ForeColor = Color.Yellow;
            mediumRadio.Location  = new Point(15, 54);
            mediumRadio.Name      = "mediumRadio";
            mediumRadio.Text      = "Medium (25% rebirth / mini-games enabled)";

            // ── hardRadio ─────────────────────────────────────────────────────
            hardRadio.AutoSize  = true;
            hardRadio.Font      = new Font("Segoe UI", 10F);
            hardRadio.ForeColor = Color.OrangeRed;
            hardRadio.Location  = new Point(15, 82);
            hardRadio.Name      = "hardRadio";
            hardRadio.Text      = "Hard   (40% rebirth / blackout mini-games!)";

            // ── difficultyGroup ────────────────────────────────────────────────
            difficultyGroup.BackColor = Color.FromArgb(30, 30, 40);
            difficultyGroup.Controls.Add(easyRadio);
            difficultyGroup.Controls.Add(mediumRadio);
            difficultyGroup.Controls.Add(hardRadio);
            difficultyGroup.Font      = new Font("Segoe UI", 11F, FontStyle.Bold);
            difficultyGroup.ForeColor = Color.White;
            difficultyGroup.Location  = new Point(15, 215);
            difficultyGroup.Name      = "difficultyGroup";
            difficultyGroup.Size      = new Size(370, 113);
            difficultyGroup.Text      = "Select Difficulty";

            // ── beginButton ────────────────────────────────────────────────────
            beginButton.BackColor                         = Color.FromArgb(0, 150, 50);
            beginButton.Cursor                            = Cursors.Hand;
            beginButton.FlatAppearance.BorderSize         = 0;
            beginButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 180, 70);
            beginButton.FlatStyle                         = FlatStyle.Flat;
            beginButton.Font                              = new Font("Segoe UI", 14F, FontStyle.Bold);
            beginButton.ForeColor                         = Color.White;
            beginButton.Location                          = new Point(15, 342);
            beginButton.Name                              = "beginButton";
            beginButton.Size                              = new Size(370, 50);
            beginButton.Text                              = "BEGIN";
            beginButton.UseVisualStyleBackColor           = false;
            beginButton.Click                             += BeginButton_Click;

            // ── SetupForm ──────────────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode       = AutoScaleMode.Font;
            BackColor           = Color.FromArgb(30, 30, 40);
            ClientSize          = new Size(404, 410);
            Controls.Add(usernameLabel);
            Controls.Add(usernameTextBox);
            Controls.Add(weaponGroup);
            Controls.Add(difficultyGroup);
            Controls.Add(beginButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox     = false;
            MinimizeBox     = false;
            Name            = "SetupForm";
            StartPosition   = FormStartPosition.CenterParent;
            Text            = "Game Setup";
            weaponGroup.ResumeLayout(false);
            weaponGroup.PerformLayout();
            difficultyGroup.ResumeLayout(false);
            difficultyGroup.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
