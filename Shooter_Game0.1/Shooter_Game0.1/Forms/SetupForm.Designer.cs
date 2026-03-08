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
        private Button beginButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            usernameLabel = new Label();
            usernameTextBox = new TextBox();
            weaponLabel = new Label();
            rifleRadio = new RadioButton();
            shotgunRadio = new RadioButton();
            sniperRadio = new RadioButton();
            beginButton = new Button();
            SuspendLayout();
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Font = new Font("Segoe UI", 12F);
            usernameLabel.ForeColor = Color.White;
            usernameLabel.Location = new Point(30, 20);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(127, 21);
            usernameLabel.TabIndex = 0;
            usernameLabel.Text = "Enter Username:";
            // 
            // usernameTextBox
            // 
            usernameTextBox.BackColor = Color.FromArgb(50, 50, 60);
            usernameTextBox.Font = new Font("Segoe UI", 12F);
            usernameTextBox.ForeColor = Color.White;
            usernameTextBox.Location = new Point(30, 50);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(340, 29);
            usernameTextBox.TabIndex = 1;
            // 
            // weaponLabel
            // 
            weaponLabel.AutoSize = true;
            weaponLabel.Font = new Font("Segoe UI", 12F);
            weaponLabel.ForeColor = Color.White;
            weaponLabel.Location = new Point(30, 100);
            weaponLabel.Name = "weaponLabel";
            weaponLabel.Size = new Size(118, 21);
            weaponLabel.TabIndex = 2;
            weaponLabel.Text = "Select Weapon:";
            // 
            // rifleRadio
            // 
            rifleRadio.AutoSize = true;
            rifleRadio.Checked = true;
            rifleRadio.Font = new Font("Segoe UI", 10F);
            rifleRadio.ForeColor = Color.LightGreen;
            rifleRadio.Location = new Point(50, 135);
            rifleRadio.Name = "rifleRadio";
            rifleRadio.Size = new Size(217, 23);
            rifleRadio.TabIndex = 3;
            rifleRadio.TabStop = true;
            rifleRadio.Text = "Rifle  (20% headshot chance)";
            rifleRadio.UseVisualStyleBackColor = true;
            // 
            // shotgunRadio
            // 
            shotgunRadio.AutoSize = true;
            shotgunRadio.Font = new Font("Segoe UI", 10F);
            shotgunRadio.ForeColor = Color.Orange;
            shotgunRadio.Location = new Point(50, 168);
            shotgunRadio.Name = "shotgunRadio";
            shotgunRadio.Size = new Size(239, 23);
            shotgunRadio.TabIndex = 4;
            shotgunRadio.Text = "Shotgun  (33% headshot chance)";
            shotgunRadio.UseVisualStyleBackColor = true;
            // 
            // sniperRadio
            // 
            sniperRadio.AutoSize = true;
            sniperRadio.Font = new Font("Segoe UI", 10F);
            sniperRadio.ForeColor = Color.Cyan;
            sniperRadio.Location = new Point(50, 201);
            sniperRadio.Name = "sniperRadio";
            sniperRadio.Size = new Size(228, 23);
            sniperRadio.TabIndex = 5;
            sniperRadio.Text = "Sniper  (10% headshot chance)";
            sniperRadio.UseVisualStyleBackColor = true;
            // 
            // beginButton
            // 
            beginButton.BackColor = Color.FromArgb(0, 150, 50);
            beginButton.Cursor = Cursors.Hand;
            beginButton.FlatAppearance.BorderSize = 0;
            beginButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 180, 70);
            beginButton.FlatStyle = FlatStyle.Flat;
            beginButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            beginButton.ForeColor = Color.White;
            beginButton.Location = new Point(30, 260);
            beginButton.Name = "beginButton";
            beginButton.Size = new Size(340, 50);
            beginButton.TabIndex = 6;
            beginButton.Text = "BEGIN";
            beginButton.UseVisualStyleBackColor = false;
            beginButton.Click += BeginButton_Click;
            // 
            // SetupForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 40);
            ClientSize = new Size(404, 341);
            Controls.Add(beginButton);
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
