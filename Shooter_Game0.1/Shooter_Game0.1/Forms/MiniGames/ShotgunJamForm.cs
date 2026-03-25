// File: Forms/MiniGames/ShotgunJamForm.cs
// Phase 5 – SHOTGUN special action: weapon jam.
// Player must press SPACE 5 times within 5 seconds to unjam the weapon.
// DialogResult.OK  = jam cleared, shot proceeds.
// DialogResult.Cancel = jam not cleared, shot is blocked this turn.
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shooter_Game0._1.Forms.MiniGames
{
    public class ShotgunJamForm : Form
    {
        private readonly System.Windows.Forms.Timer _gameTimer;

        private readonly Label _warningLabel;
        private readonly Label _instructionLabel;
        private readonly Label _counterLabel;
        private readonly Label _countdownLabel;
        private readonly ProgressBar _progressBar;

        private int _spaceCount = 0;
        private const int RequiredPresses = 5;
        private int _timeRemainingMs = 5000;

        public ShotgunJamForm()
        {
            Text = "!! WEAPON JAMMED !!";
            Size = new Size(380, 280);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.FromArgb(60, 10, 10);
            KeyPreview = true;

            _warningLabel = new Label
            {
                Text = "!! SHOTGUN JAMMED !!",
                ForeColor = Color.OrangeRed,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Location = new Point(10, 15),
                Size = new Size(350, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            _instructionLabel = new Label
            {
                Text = "Press SPACE 5 times to clear the jam!",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(10, 65),
                Size = new Size(350, 26),
                TextAlign = ContentAlignment.MiddleCenter
            };

            _counterLabel = new Label
            {
                Text = "0 / 5",
                ForeColor = Color.Yellow,
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                Location = new Point(10, 100),
                Size = new Size(350, 50),
                TextAlign = ContentAlignment.MiddleCenter
            };

            _progressBar = new ProgressBar
            {
                Location = new Point(20, 158),
                Size = new Size(330, 20),
                Minimum = 0,
                Maximum = RequiredPresses,
                Value = 0,
                Style = ProgressBarStyle.Continuous
            };

            _countdownLabel = new Label
            {
                Text = "Time: 5.0s",
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 10),
                Location = new Point(10, 185),
                Size = new Size(350, 22),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var cancelBtn = new Button
            {
                Text = "Give up (lose turn)",
                BackColor = Color.FromArgb(100, 30, 30),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Location = new Point(110, 215),
                Size = new Size(150, 30),
                Cursor = Cursors.Hand
            };
            cancelBtn.FlatAppearance.BorderSize = 0;
            cancelBtn.Click += (s, e) => Resolve(cleared: false);

            Controls.Add(_warningLabel);
            Controls.Add(_instructionLabel);
            Controls.Add(_counterLabel);
            Controls.Add(_progressBar);
            Controls.Add(_countdownLabel);
            Controls.Add(cancelBtn);

            KeyDown += ShotgunJamForm_KeyDown;

            _gameTimer = new System.Windows.Forms.Timer { Interval = 100 };
            _gameTimer.Tick += GameTimer_Tick;
            _gameTimer.Start();
        }

        private void ShotgunJamForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                _spaceCount++;
                _counterLabel.Text = $"{_spaceCount} / {RequiredPresses}";
                _progressBar.Value = Math.Min(_spaceCount, RequiredPresses);

                if (_spaceCount >= RequiredPresses)
                    Resolve(cleared: true);

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            _timeRemainingMs -= 100;
            _countdownLabel.Text = $"Time: {_timeRemainingMs / 1000.0:F1}s";
            if (_timeRemainingMs <= 2000)
                _countdownLabel.ForeColor = Color.OrangeRed;
            if (_timeRemainingMs <= 0)
                Resolve(cleared: false);
        }

        private void Resolve(bool cleared)
        {
            _gameTimer.Stop();
            DialogResult = cleared ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _gameTimer.Stop();
            base.OnFormClosing(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _gameTimer.Dispose();
            base.Dispose(disposing);
        }
    }
}
