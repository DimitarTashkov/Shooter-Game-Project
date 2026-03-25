// File: Forms/MiniGames/OrcMiniGameForm.Designer.cs
namespace Shooter_Game0._1.Forms.MiniGames
{
    partial class OrcMiniGameForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label  _instructionLabel;
        private Label  _countdownLabel;
        private Panel  _gamePanel;
        private Panel  _blinkOverlay;

        private void InitializeComponent()
        {
            _instructionLabel = new Label();
            _countdownLabel   = new Label();
            _gamePanel        = new Panel();
            _blinkOverlay     = new Panel();
            SuspendLayout();
            //
            // _instructionLabel
            //
            _instructionLabel.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            _instructionLabel.ForeColor = Color.LightGreen;
            _instructionLabel.Location  = new Point(10, 5);
            _instructionLabel.Name      = "_instructionLabel";
            _instructionLabel.Size      = new Size(430, 22);
            _instructionLabel.Text      = "Click the ORC before the 3-second timer expires!";
            _instructionLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // _countdownLabel
            //
            _countdownLabel.Font      = new Font("Segoe UI", 13F, FontStyle.Bold);
            _countdownLabel.ForeColor = Color.Yellow;
            _countdownLabel.Location  = new Point(165, 30);
            _countdownLabel.Name      = "_countdownLabel";
            _countdownLabel.Size      = new Size(120, 26);
            _countdownLabel.Text      = "Time: 3.0s";
            _countdownLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // _gamePanel
            //
            _gamePanel.BackColor    = Color.FromArgb(30, 45, 30);
            _gamePanel.BorderStyle  = BorderStyle.FixedSingle;
            _gamePanel.Location     = new Point(10, 60);
            _gamePanel.Name         = "_gamePanel";
            _gamePanel.Size         = new Size(424, 270);
            _gamePanel.Paint        += GamePanel_Paint;
            _gamePanel.MouseClick   += GamePanel_MouseClick;
            //
            // _blinkOverlay  (Hard-mode darkness effect – starts hidden)
            //
            _blinkOverlay.BackColor = Color.Black;
            _blinkOverlay.Location  = new Point(10, 60);
            _blinkOverlay.Name      = "_blinkOverlay";
            _blinkOverlay.Size      = new Size(424, 270);
            _blinkOverlay.Visible   = false;
            //
            // OrcMiniGameForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode       = AutoScaleMode.Font;
            BackColor           = Color.FromArgb(18, 28, 18);
            ClientSize          = new Size(460, 380);
            Controls.Add(_instructionLabel);
            Controls.Add(_countdownLabel);
            Controls.Add(_gamePanel);
            Controls.Add(_blinkOverlay);
            _blinkOverlay.BringToFront();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox     = false;
            MinimizeBox     = false;
            Name            = "OrcMiniGameForm";
            StartPosition   = FormStartPosition.CenterParent;
            Text            = "ORC MINI-GAME: Catch the Thief!";
            ResumeLayout(false);
        }
    }
}
