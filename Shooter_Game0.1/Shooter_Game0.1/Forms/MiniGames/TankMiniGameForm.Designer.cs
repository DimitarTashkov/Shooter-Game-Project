// File: Forms/MiniGames/TankMiniGameForm.Designer.cs
namespace Shooter_Game0._1.Forms.MiniGames
{
    partial class TankMiniGameForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label _instructionLabel;
        private Label _countdownLabel;
        private Label _stepLabel;
        private Panel _gamePanel;
        private Panel _blinkOverlay;

        private void InitializeComponent()
        {
            _instructionLabel = new Label();
            _countdownLabel   = new Label();
            _stepLabel        = new Label();
            _gamePanel        = new Panel();
            _blinkOverlay     = new Panel();
            SuspendLayout();
            //
            // _instructionLabel
            //
            _instructionLabel.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            _instructionLabel.ForeColor = Color.SteelBlue;
            _instructionLabel.Location  = new Point(10, 5);
            _instructionLabel.Name      = "_instructionLabel";
            _instructionLabel.Size      = new Size(470, 22);
            _instructionLabel.Text      = "Click the shields in order:  Small \u2192 Medium \u2192 Large!";
            _instructionLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // _countdownLabel
            //
            _countdownLabel.Font      = new Font("Segoe UI", 13F, FontStyle.Bold);
            _countdownLabel.ForeColor = Color.Yellow;
            _countdownLabel.Location  = new Point(190, 30);
            _countdownLabel.Name      = "_countdownLabel";
            _countdownLabel.Size      = new Size(120, 26);
            _countdownLabel.Text      = "Time: 5.0s";
            _countdownLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // _stepLabel
            //
            _stepLabel.Font      = new Font("Segoe UI", 10F, FontStyle.Bold);
            _stepLabel.ForeColor = Color.LightCyan;
            _stepLabel.Location  = new Point(10, 58);
            _stepLabel.Name      = "_stepLabel";
            _stepLabel.Size      = new Size(470, 20);
            _stepLabel.Text      = "Step 1 of 3: Click the SMALL (blue) shield";
            _stepLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // _gamePanel
            //
            _gamePanel.BackColor  = Color.FromArgb(28, 28, 40);
            _gamePanel.BorderStyle = BorderStyle.FixedSingle;
            _gamePanel.Location   = new Point(10, 82);
            _gamePanel.Name       = "_gamePanel";
            _gamePanel.Size       = new Size(464, 290);
            _gamePanel.Paint      += GamePanel_Paint;
            _gamePanel.MouseClick += GamePanel_MouseClick;
            //
            // _blinkOverlay
            //
            _blinkOverlay.BackColor = Color.Black;
            _blinkOverlay.Location  = new Point(10, 82);
            _blinkOverlay.Name      = "_blinkOverlay";
            _blinkOverlay.Size      = new Size(464, 290);
            _blinkOverlay.Visible   = false;
            //
            // TankMiniGameForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode       = AutoScaleMode.Font;
            BackColor           = Color.FromArgb(18, 18, 28);
            ClientSize          = new Size(500, 420);
            Controls.Add(_instructionLabel);
            Controls.Add(_countdownLabel);
            Controls.Add(_stepLabel);
            Controls.Add(_gamePanel);
            Controls.Add(_blinkOverlay);
            _blinkOverlay.BringToFront();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox     = false;
            MinimizeBox     = false;
            Name            = "TankMiniGameForm";
            StartPosition   = FormStartPosition.CenterParent;
            Text            = "TANK MINI-GAME: Break the Armor!";
            ResumeLayout(false);
        }
    }
}
