namespace Shooter_Game0._1.Forms
{
    partial class LeaderboardForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label titleLabel;
        private DataGridView scoresGrid;
        private Button closeButton;

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
            scoresGrid = new DataGridView();
            closeButton = new Button();
            ((System.ComponentModel.ISupportInitialize)scoresGrid).BeginInit();
            SuspendLayout();
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            titleLabel.ForeColor = Color.Gold;
            titleLabel.Location = new Point(130, 15);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(210, 32);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "🏆 LEADERBOARD";
            // 
            // scoresGrid
            // 
            scoresGrid.AllowUserToAddRows = false;
            scoresGrid.AllowUserToDeleteRows = false;
            scoresGrid.AllowUserToResizeRows = false;
            scoresGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            scoresGrid.BackgroundColor = Color.FromArgb(35, 35, 45);
            scoresGrid.BorderStyle = BorderStyle.None;
            scoresGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            scoresGrid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 120, 200),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                SelectionBackColor = Color.FromArgb(0, 120, 200),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            scoresGrid.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(40, 40, 55),
                ForeColor = Color.White,
                Font = new Font("Consolas", 10F),
                SelectionBackColor = Color.FromArgb(60, 60, 80),
                SelectionForeColor = Color.White
            };
            scoresGrid.EnableHeadersVisualStyles = false;
            scoresGrid.GridColor = Color.FromArgb(60, 60, 70);
            scoresGrid.Location = new Point(20, 60);
            scoresGrid.Name = "scoresGrid";
            scoresGrid.ReadOnly = true;
            scoresGrid.RowHeadersVisible = false;
            scoresGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            scoresGrid.Size = new Size(440, 300);
            scoresGrid.TabIndex = 1;
            // 
            // closeButton
            // 
            closeButton.BackColor = Color.FromArgb(100, 100, 110);
            closeButton.Cursor = Cursors.Hand;
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            closeButton.ForeColor = Color.White;
            closeButton.Location = new Point(170, 375);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(140, 40);
            closeButton.TabIndex = 2;
            closeButton.Text = "CLOSE";
            closeButton.UseVisualStyleBackColor = false;
            closeButton.Click += CloseButton_Click;
            // 
            // LeaderboardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 35);
            ClientSize = new Size(480, 430);
            Controls.Add(closeButton);
            Controls.Add(scoresGrid);
            Controls.Add(titleLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LeaderboardForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Leaderboard";
            ((System.ComponentModel.ISupportInitialize)scoresGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
