// File: Forms/MiniGames/ShotgunJamForm.Designer.cs
namespace Shooter_Game0._1.Forms.MiniGames
{
    partial class ShotgunJamForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label       _warningLabel;
        private Label       _instructionLabel;
        private Label       _counterLabel;
        private ProgressBar _progressBar;
        private Label       _countdownLabel;
        private Button      _cancelButton;

        private void InitializeComponent()
        {
            _warningLabel     = new Label();
            _instructionLabel = new Label();
            _counterLabel     = new Label();
            _progressBar      = new ProgressBar();
            _countdownLabel   = new Label();
            _cancelButton     = new Button();
            SuspendLayout();
            //
            // _warningLabel
            //
            _warningLabel.Font      = new Font("Segoe UI", 20F, FontStyle.Bold);
            _warningLabel.ForeColor = Color.OrangeRed;
            _warningLabel.Location  = new Point(10, 15);
            _warningLabel.Name      = "_warningLabel";
            _warningLabel.Size      = new Size(350, 40);
            _warningLabel.Text      = "!! SHOTGUN JAMMED !!";
            _warningLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // _instructionLabel
            //
            _instructionLabel.Font      = new Font("Segoe UI", 11F, FontStyle.Bold);
            _instructionLabel.ForeColor = Color.White;
            _instructionLabel.Location  = new Point(10, 65);
            _instructionLabel.Name      = "_instructionLabel";
            _instructionLabel.Size      = new Size(350, 26);
            _instructionLabel.Text      = "Press SPACE 5 times to clear the jam!";
            _instructionLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // _counterLabel
            //
            _counterLabel.Font      = new Font("Segoe UI", 26F, FontStyle.Bold);
            _counterLabel.ForeColor = Color.Yellow;
            _counterLabel.Location  = new Point(10, 100);
            _counterLabel.Name      = "_counterLabel";
            _counterLabel.Size      = new Size(350, 50);
            _counterLabel.Text      = "0 / 5";
            _counterLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // _progressBar
            //
            _progressBar.Location = new Point(20, 158);
            _progressBar.Maximum  = 5;
            _progressBar.Minimum  = 0;
            _progressBar.Name     = "_progressBar";
            _progressBar.Size     = new Size(330, 20);
            _progressBar.Style    = ProgressBarStyle.Continuous;
            _progressBar.Value    = 0;
            //
            // _countdownLabel
            //
            _countdownLabel.Font      = new Font("Segoe UI", 10F);
            _countdownLabel.ForeColor = Color.LightGray;
            _countdownLabel.Location  = new Point(10, 185);
            _countdownLabel.Name      = "_countdownLabel";
            _countdownLabel.Size      = new Size(350, 22);
            _countdownLabel.Text      = "Time: 5.0s";
            _countdownLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // _cancelButton
            //
            _cancelButton.BackColor                         = Color.FromArgb(100, 30, 30);
            _cancelButton.Cursor                            = Cursors.Hand;
            _cancelButton.FlatAppearance.BorderSize         = 0;
            _cancelButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(140, 50, 50);
            _cancelButton.FlatStyle                         = FlatStyle.Flat;
            _cancelButton.Font                              = new Font("Segoe UI", 9F);
            _cancelButton.ForeColor                         = Color.White;
            _cancelButton.Location                          = new Point(110, 215);
            _cancelButton.Name                              = "_cancelButton";
            _cancelButton.Size                              = new Size(150, 30);
            _cancelButton.Text                              = "Give up (lose turn)";
            _cancelButton.UseVisualStyleBackColor           = false;
            _cancelButton.Click                             += CancelButton_Click;
            //
            // ShotgunJamForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode       = AutoScaleMode.Font;
            BackColor           = Color.FromArgb(60, 10, 10);
            ClientSize          = new Size(380, 280);
            Controls.Add(_warningLabel);
            Controls.Add(_instructionLabel);
            Controls.Add(_counterLabel);
            Controls.Add(_progressBar);
            Controls.Add(_countdownLabel);
            Controls.Add(_cancelButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview      = true;
            MaximizeBox     = false;
            MinimizeBox     = false;
            Name            = "ShotgunJamForm";
            StartPosition   = FormStartPosition.CenterParent;
            Text            = "!! WEAPON JAMMED !!";
            ResumeLayout(false);
        }
    }
}
