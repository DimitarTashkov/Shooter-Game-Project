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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            titleLabel = new Label();
            scoresGrid = new DataGridView();
            closeButton = new Button();
            Rank = new DataGridViewTextBoxColumn();
            Username = new DataGridViewTextBoxColumn();
            Score = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)scoresGrid).BeginInit();
            SuspendLayout();
            // 
            // titleLabel
            // 
            titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            titleLabel.ForeColor = Color.Gold;
            titleLabel.Location = new Point(10, 15);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(529, 45);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "🏆 LEADERBOARD";
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // scoresGrid
            // 
            scoresGrid.AllowUserToAddRows = false;
            scoresGrid.AllowUserToDeleteRows = false;
            scoresGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(42, 42, 55);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(0, 100, 180);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            scoresGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            scoresGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            scoresGrid.BackgroundColor = Color.FromArgb(35, 35, 45);
            scoresGrid.BorderStyle = BorderStyle.None;
            scoresGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            scoresGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(50, 50, 65);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.Gold;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(50, 50, 65);
            dataGridViewCellStyle2.SelectionForeColor = Color.Gold;
            scoresGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            scoresGrid.ColumnHeadersHeight = 35;
            scoresGrid.Columns.AddRange(new DataGridViewColumn[] { Rank, Username, Score });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(35, 35, 45);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(0, 100, 180);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            scoresGrid.DefaultCellStyle = dataGridViewCellStyle3;
            scoresGrid.EnableHeadersVisualStyles = false;
            scoresGrid.GridColor = Color.FromArgb(55, 55, 70);
            scoresGrid.Location = new Point(23, 70);
            scoresGrid.Margin = new Padding(3, 4, 3, 4);
            scoresGrid.Name = "scoresGrid";
            scoresGrid.ReadOnly = true;
            scoresGrid.RowHeadersVisible = false;
            scoresGrid.RowHeadersWidth = 51;
            scoresGrid.RowTemplate.Height = 32;
            scoresGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            scoresGrid.Size = new Size(503, 405);
            scoresGrid.TabIndex = 1;
            // 
            // closeButton
            // 
            closeButton.BackColor = Color.FromArgb(100, 100, 110);
            closeButton.Cursor = Cursors.Hand;
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 130, 140);
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            closeButton.ForeColor = Color.White;
            closeButton.Location = new Point(194, 490);
            closeButton.Margin = new Padding(3, 4, 3, 4);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(160, 48);
            closeButton.TabIndex = 2;
            closeButton.Text = "CLOSE";
            closeButton.UseVisualStyleBackColor = false;
            closeButton.Click += CloseButton_Click;
            // 
            // Rank
            // 
            Rank.DataPropertyName = "Rank";
            Rank.FillWeight = 15F;
            Rank.HeaderText = "Rank";
            Rank.MinimumWidth = 40;
            Rank.Name = "Rank";
            Rank.ReadOnly = true;
            //
            // Username
            //
            Username.DataPropertyName = "Username";
            Username.FillWeight = 55F;
            Username.HeaderText = "Username";
            Username.MinimumWidth = 80;
            Username.Name = "Username";
            Username.ReadOnly = true;
            //
            // Score
            //
            Score.DataPropertyName = "Score";
            Score.FillWeight = 30F;
            Score.HeaderText = "Score";
            Score.MinimumWidth = 60;
            Score.Name = "Score";
            Score.ReadOnly = true;
            // 
            // LeaderboardForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 35);
            ClientSize = new Size(549, 555);
            Controls.Add(closeButton);
            Controls.Add(scoresGrid);
            Controls.Add(titleLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LeaderboardForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Leaderboard";
            ((System.ComponentModel.ISupportInitialize)scoresGrid).EndInit();
            ResumeLayout(false);
        }

        private DataGridViewTextBoxColumn Rank;
        private DataGridViewTextBoxColumn Username;
        private DataGridViewTextBoxColumn Score;
    }
}
