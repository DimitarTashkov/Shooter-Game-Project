namespace Shooter_Game0._1.Forms
{
    partial class GameForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel mapPanel;
        private ListBox combatLog;
        private Label statsLabel;
        private Label enemiesLeftLabel;
        private Label weaponLabel;
        private Button hintButton;
        private Button undoButton;
        private Button saveButton;
        private Button endButton;

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
            mapPanel = new Panel();
            combatLog = new ListBox();
            statsLabel = new Label();
            enemiesLeftLabel = new Label();
            weaponLabel = new Label();
            hintButton = new Button();
            undoButton = new Button();
            saveButton = new Button();
            endButton = new Button();
            SuspendLayout();
            // 
            // mapPanel
            // 
            mapPanel.BackColor = Color.FromArgb(40, 40, 50);
            mapPanel.BorderStyle = BorderStyle.FixedSingle;
            mapPanel.Location = new Point(11, 13);
            mapPanel.Margin = new Padding(3, 4, 3, 4);
            mapPanel.Name = "mapPanel";
            mapPanel.Size = new Size(365, 426);
            mapPanel.TabIndex = 0;
            mapPanel.Paint += MapPanel_Paint;
            mapPanel.MouseClick += MapPanel_MouseClick;
            // 
            // combatLog
            // 
            combatLog.BackColor = Color.FromArgb(20, 20, 30);
            combatLog.BorderStyle = BorderStyle.FixedSingle;
            combatLog.Font = new Font("Consolas", 9F);
            combatLog.ForeColor = Color.LightGreen;
            combatLog.HorizontalScrollbar = true;
            combatLog.ItemHeight = 14;
            combatLog.Location = new Point(389, 13);
            combatLog.Margin = new Padding(3, 4, 3, 4);
            combatLog.Name = "combatLog";
            combatLog.SelectionMode = SelectionMode.None;
            combatLog.Size = new Size(411, 344);
            combatLog.TabIndex = 1;
            // 
            // statsLabel
            // 
            statsLabel.Font = new Font("Segoe UI", 9F);
            statsLabel.ForeColor = Color.White;
            statsLabel.Location = new Point(389, 371);
            statsLabel.Name = "statsLabel";
            statsLabel.Size = new Size(411, 29);
            statsLabel.TabIndex = 2;
            statsLabel.Text = "Kills: 0 | Damage: 0 | Points: 0";
            // 
            // enemiesLeftLabel
            // 
            enemiesLeftLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            enemiesLeftLabel.ForeColor = Color.Yellow;
            enemiesLeftLabel.Location = new Point(389, 407);
            enemiesLeftLabel.Name = "enemiesLeftLabel";
            enemiesLeftLabel.Size = new Size(411, 29);
            enemiesLeftLabel.TabIndex = 3;
            enemiesLeftLabel.Text = "Enemies remaining: 0";
            // 
            // weaponLabel
            // 
            weaponLabel.AutoSize = true;
            weaponLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            weaponLabel.ForeColor = Color.Cyan;
            weaponLabel.Location = new Point(14, 467);
            weaponLabel.Name = "weaponLabel";
            weaponLabel.Size = new Size(91, 25);
            weaponLabel.TabIndex = 4;
            weaponLabel.Text = "Weapon:";
            // 
            // hintButton
            // 
            hintButton.BackColor = Color.FromArgb(80, 80, 20);
            hintButton.Cursor = Cursors.Hand;
            hintButton.FlatAppearance.BorderSize = 0;
            hintButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(110, 110, 30);
            hintButton.FlatStyle = FlatStyle.Flat;
            hintButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            hintButton.ForeColor = Color.White;
            hintButton.Location = new Point(231, 456);
            hintButton.Margin = new Padding(3, 4, 3, 4);
            hintButton.Name = "hintButton";
            hintButton.Size = new Size(200, 51);
            hintButton.TabIndex = 5;
            hintButton.Text = "HINT (H)";
            hintButton.UseVisualStyleBackColor = false;
            hintButton.Click += HintButton_Click;
            // 
            // undoButton
            // 
            undoButton.BackColor = Color.FromArgb(180, 40, 40);
            undoButton.Cursor = Cursors.Hand;
            undoButton.FlatAppearance.BorderSize = 0;
            undoButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(210, 60, 60);
            undoButton.FlatStyle = FlatStyle.Flat;
            undoButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            undoButton.ForeColor = Color.White;
            undoButton.Location = new Point(450, 456);
            undoButton.Margin = new Padding(3, 4, 3, 4);
            undoButton.Name = "undoButton";
            undoButton.Size = new Size(126, 51);
            undoButton.TabIndex = 6;
            undoButton.Text = "UNDO (Ctrl+Z)";
            undoButton.UseVisualStyleBackColor = false;
            undoButton.Click += UndoButton_Click;
            // 
            // saveButton
            // 
            saveButton.BackColor = Color.FromArgb(40, 140, 180);
            saveButton.Cursor = Cursors.Hand;
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 170, 210);
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            saveButton.ForeColor = Color.White;
            saveButton.Location = new Point(0, 0); // Position is handled by ApplyDynamicLayout
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(126, 51);
            saveButton.TabIndex = 7;
            saveButton.Text = "SAVE GAME";
            saveButton.UseVisualStyleBackColor = false;
            saveButton.Click += SaveButton_Click;
            // 
            // endButton
            // 
            endButton.BackColor = Color.FromArgb(180, 40, 40);
            endButton.Cursor = Cursors.Hand;
            endButton.FlatAppearance.BorderSize = 0;
            endButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(210, 60, 60);
            endButton.FlatStyle = FlatStyle.Flat;
            endButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            endButton.ForeColor = Color.White;
            endButton.Location = new Point(599, 456);
            endButton.Margin = new Padding(3, 4, 3, 4);
            endButton.Name = "endButton";
            endButton.Size = new Size(200, 51);
            endButton.TabIndex = 6;
            endButton.Text = "END GAME";
            endButton.UseVisualStyleBackColor = false;
            endButton.Click += EndButton_Click;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 35);
            ClientSize = new Size(811, 527);
            Controls.Add(endButton);
            Controls.Add(saveButton);
            Controls.Add(undoButton);
            Controls.Add(hintButton);
            Controls.Add(weaponLabel);
            Controls.Add(enemiesLeftLabel);
            Controls.Add(statsLabel);
            Controls.Add(combatLog);
            Controls.Add(mapPanel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "GameForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Shooter Game";
            KeyDown += GameForm_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
