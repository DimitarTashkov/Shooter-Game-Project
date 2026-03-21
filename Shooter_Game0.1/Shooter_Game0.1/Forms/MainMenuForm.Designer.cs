namespace Shooter_Game0._1.Forms
{
    partial class MainMenuForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label titleLabel;
        private Label subtitleLabel;
        private Button startButton;
        private Button loadGameButton;
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
            loadGameButton = new Button();
            leaderboardButton = new Button();
            exitButton = new Button();
            SuspendLayout();
            // 
            // titleLabel
            // 
            titleLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(0, 180, 255);
            titleLabel.Location = new Point(10, 35);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(464, 55);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "SHOOTER GAME";
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // subtitleLabel
            // 
            subtitleLabel.Font = new Font("Segoe UI", 10F);
            subtitleLabel.ForeColor = Color.FromArgb(140, 140, 160);
            subtitleLabel.Location = new Point(10, 95);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(464, 22);
            subtitleLabel.TabIndex = 1;
            subtitleLabel.Text = "Console-to-WinForms Edition";
            subtitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // startButton
            // 
            startButton.BackColor = Color.FromArgb(0, 120, 200);
            startButton.Cursor = Cursors.Hand;
            startButton.FlatAppearance.BorderSize = 0;
            startButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 150, 240);
            startButton.FlatStyle = FlatStyle.Flat;
            startButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            startButton.ForeColor = Color.White;
            startButton.Location = new Point(132, 150);
            startButton.Name = "startButton";
            startButton.Size = new Size(220, 50);
            startButton.TabIndex = 2;
            startButton.Text = "START GAME";
            startButton.UseVisualStyleBackColor = false;
            startButton.Click += StartButton_Click;
            // 
            // loadGameButton
            // 
            loadGameButton.BackColor = Color.FromArgb(40, 140, 180);
            loadGameButton.Cursor = Cursors.Hand;
            loadGameButton.FlatAppearance.BorderSize = 0;
            loadGameButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 170, 210);
            loadGameButton.FlatStyle = FlatStyle.Flat;
            loadGameButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            loadGameButton.ForeColor = Color.White;
            loadGameButton.Location = new Point(132, 220);
            loadGameButton.Name = "loadGameButton";
            loadGameButton.Size = new Size(220, 50);
            loadGameButton.TabIndex = 3;
            loadGameButton.Text = "LOAD GAME";
            loadGameButton.UseVisualStyleBackColor = false;
            loadGameButton.Click += LoadGameButton_Click;
            // 
            // leaderboardButton
            // 
            leaderboardButton.BackColor = Color.FromArgb(140, 110, 20);
            leaderboardButton.Cursor = Cursors.Hand;
            leaderboardButton.FlatAppearance.BorderSize = 0;
            leaderboardButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(170, 140, 30);
            leaderboardButton.FlatStyle = FlatStyle.Flat;
            leaderboardButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            leaderboardButton.ForeColor = Color.White;
            leaderboardButton.Location = new Point(132, 290);
            leaderboardButton.Name = "leaderboardButton";
            leaderboardButton.Size = new Size(220, 50);
            leaderboardButton.TabIndex = 4;
            leaderboardButton.Text = "LEADERBOARD";
            leaderboardButton.UseVisualStyleBackColor = false;
            leaderboardButton.Click += LeaderboardButton_Click;
            // 
            // exitButton
            // 
            exitButton.BackColor = Color.FromArgb(180, 40, 40);
            exitButton.Cursor = Cursors.Hand;
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(210, 60, 60);
            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            exitButton.ForeColor = Color.White;
            exitButton.Location = new Point(132, 360);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(220, 50);
            exitButton.TabIndex = 5;
            exitButton.Text = "EXIT";
            exitButton.UseVisualStyleBackColor = false;
            exitButton.Click += ExitButton_Click;
            // 
            // MainMenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 40);
            ClientSize = new Size(484, 440);
            Controls.Add(exitButton);
            Controls.Add(leaderboardButton);
            Controls.Add(loadGameButton);
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
