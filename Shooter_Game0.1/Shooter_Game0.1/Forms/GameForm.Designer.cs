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
            endButton = new Button();
            SuspendLayout();
            // 
            // mapPanel
            // 
            mapPanel.BackColor = Color.FromArgb(40, 40, 50);
            mapPanel.BorderStyle = BorderStyle.FixedSingle;
            mapPanel.Location = new Point(10, 10);
            mapPanel.Name = "mapPanel";
            mapPanel.Size = new Size(320, 320);
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
            combatLog.Location = new Point(340, 10);
            combatLog.Name = "combatLog";
            combatLog.SelectionMode = SelectionMode.None;
            combatLog.Size = new Size(360, 260);
            combatLog.TabIndex = 1;
            // 
            // statsLabel
            // 
            statsLabel.Font = new Font("Segoe UI", 9F);
            statsLabel.ForeColor = Color.White;
            statsLabel.Location = new Point(340, 278);
            statsLabel.Name = "statsLabel";
            statsLabel.Size = new Size(360, 22);
            statsLabel.TabIndex = 2;
            statsLabel.Text = "Kills: 0 | Damage: 0 | Points: 0";
            // 
            // enemiesLeftLabel
            // 
            enemiesLeftLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            enemiesLeftLabel.ForeColor = Color.Yellow;
            enemiesLeftLabel.Location = new Point(340, 305);
            enemiesLeftLabel.Name = "enemiesLeftLabel";
            enemiesLeftLabel.Size = new Size(360, 22);
            enemiesLeftLabel.TabIndex = 3;
            enemiesLeftLabel.Text = "Enemies remaining: 0";
            // 
            // weaponLabel
            // 
            weaponLabel.AutoSize = true;
            weaponLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            weaponLabel.ForeColor = Color.Cyan;
            weaponLabel.Location = new Point(12, 350);
            weaponLabel.Name = "weaponLabel";
            weaponLabel.Size = new Size(80, 20);
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
            hintButton.Location = new Point(340, 342);
            hintButton.Name = "hintButton";
            hintButton.Size = new Size(175, 38);
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
            undoButton.Location = new Point(480, 342);
            undoButton.Name = "undoButton";
            undoButton.Size = new Size(110, 38);
            undoButton.TabIndex = 6;
            undoButton.Text = "UNDO (Ctrl+Z)";
            undoButton.UseVisualStyleBackColor = false;
            undoButton.Click += UndoButton_Click;
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
            endButton.Location = new Point(525, 342);
            endButton.Name = "endButton";
            endButton.Size = new Size(175, 38);
            endButton.TabIndex = 6;
            endButton.Text = "END GAME";
            endButton.UseVisualStyleBackColor = false;
            endButton.Click += EndButton_Click;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 35);
            ClientSize = new Size(710, 395);
            Controls.Add(endButton);
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
