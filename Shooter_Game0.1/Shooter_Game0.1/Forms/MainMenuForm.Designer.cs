namespace Shooter_Game0._1.Forms
{
    partial class MainMenuForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label titleLabel;
        private Label subtitleLabel;
        private Button startButton;
        private Button leaderboardButton;
        private Button exitButton;

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
            titleLabel = new Label();
            subtitleLabel = new Label();
            startButton = new Button();
            leaderboardButton = new Button();
            exitButton = new Button();
            SuspendLayout();
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(0, 180, 255);
            titleLabel.Location = new Point(90, 40);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(320, 51);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "SHOOTER GAME";
            // 
            // subtitleLabel
            // 
            subtitleLabel.AutoSize = true;
            subtitleLabel.Font = new Font("Segoe UI", 10F);
            subtitleLabel.ForeColor = Color.Gray;
            subtitleLabel.Location = new Point(140, 90);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(198, 19);
            subtitleLabel.TabIndex = 1;
            subtitleLabel.Text = "Console-to-WinForms Edition";
            // 
            // startButton
            // 
            startButton.BackColor = Color.FromArgb(0, 120, 200);
            startButton.Cursor = Cursors.Hand;
            startButton.FlatAppearance.BorderSize = 0;
            startButton.FlatStyle = FlatStyle.Flat;
            startButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            startButton.ForeColor = Color.White;
            startButton.Location = new Point(140, 160);
            startButton.Name = "startButton";
            startButton.Size = new Size(220, 50);
            startButton.TabIndex = 2;
            startButton.Text = "START GAME";
            startButton.UseVisualStyleBackColor = false;
            startButton.Click += StartButton_Click;
            // 
            // leaderboardButton
            // 
            leaderboardButton.BackColor = Color.FromArgb(80, 80, 20);
            leaderboardButton.Cursor = Cursors.Hand;
            leaderboardButton.FlatAppearance.BorderSize = 0;
            leaderboardButton.FlatStyle = FlatStyle.Flat;
            leaderboardButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            leaderboardButton.ForeColor = Color.White;
            leaderboardButton.Location = new Point(140, 230);
            leaderboardButton.Name = "leaderboardButton";
            leaderboardButton.Size = new Size(220, 50);
            leaderboardButton.TabIndex = 3;
            leaderboardButton.Text = "LEADERBOARD";
            leaderboardButton.UseVisualStyleBackColor = false;
            leaderboardButton.Click += LeaderboardButton_Click;
            // 
            // exitButton
            // 
            exitButton.BackColor = Color.FromArgb(180, 40, 40);
            exitButton.Cursor = Cursors.Hand;
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            exitButton.ForeColor = Color.White;
            exitButton.Location = new Point(140, 310);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(220, 50);
            exitButton.TabIndex = 4;
            exitButton.Text = "EXIT";
            exitButton.UseVisualStyleBackColor = false;
            exitButton.Click += ExitButton_Click;
            // 
            // MainMenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 40);
            ClientSize = new Size(484, 451);
            Controls.Add(exitButton);
            Controls.Add(leaderboardButton);
            Controls.Add(startButton);
            Controls.Add(subtitleLabel);
            Controls.Add(titleLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainMenuForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Shooter Game";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
