// File: Forms/MiniGames/OrcMiniGameForm.cs
// Phase 2 – ORC mini-game: "Catch the Thief!"
// The Orc target teleports every 500ms. Player must click it within 3 seconds.
// Hard mode: random blackout blink every ~1.2 seconds for 300ms.
using System;
using System.Drawing;
using System.Windows.Forms;
using Shooter_Game0._1.Utilities;

namespace Shooter_Game0._1.Forms.MiniGames
{
    public class OrcMiniGameForm : Form
    {
        // ── Timers ──────────────────────────────────────────────────────────
        private readonly System.Windows.Forms.Timer _moveTimer;
        private readonly System.Windows.Forms.Timer _gameTimer;
        private readonly System.Windows.Forms.Timer _blinkTimer;

        // ── Controls ────────────────────────────────────────────────────────
        private readonly Panel _gamePanel;
        private readonly Label _countdownLabel;
        private readonly Panel _blinkOverlay;

        // ── State ────────────────────────────────────────────────────────────
        private Point _targetPos;
        private const int TargetRadius = 22;
        private int _timeRemainingMs = 3000;
        private bool _blinkActive = false;
        private readonly Random _rng = new Random();

        public OrcMiniGameForm(Difficulty difficulty)
        {
            // ── Form appearance ──────────────────────────────────────────────
            Text = "ORC MINI-GAME: Catch the Thief!";
            Size = new Size(460, 380);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.FromArgb(18, 28, 18);

            // ── Instruction label ────────────────────────────────────────────
            var instruction = new Label
            {
                Text = difficulty == Difficulty.Hard
                    ? "HARD MODE: Click the Orc before time runs out! Watch for blackouts!"
                    : "Click the ORC before the 3-second timer expires!",
                ForeColor = difficulty == Difficulty.Hard ? Color.OrangeRed : Color.LightGreen,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(10, 5),
                Size = new Size(430, 22),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ── Countdown label ───────────────────────────────────────────────
            _countdownLabel = new Label
            {
                Text = "Time: 3.0s",
                ForeColor = Color.Yellow,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Location = new Point(165, 30),
                Size = new Size(120, 26),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ── Game panel ────────────────────────────────────────────────────
            _gamePanel = new Panel
            {
                Location = new Point(10, 60),
                Size = new Size(424, 270),
                BackColor = Color.FromArgb(30, 45, 30),
                BorderStyle = BorderStyle.FixedSingle
            };
            _gamePanel.Paint += GamePanel_Paint;
            _gamePanel.MouseClick += GamePanel_MouseClick;

            // ── Blink overlay (Hard mode) ─────────────────────────────────────
            _blinkOverlay = new Panel
            {
                Location = _gamePanel.Location,
                Size = _gamePanel.Size,
                BackColor = Color.Black,
                Visible = false
            };

            Controls.Add(instruction);
            Controls.Add(_countdownLabel);
            Controls.Add(_gamePanel);
            Controls.Add(_blinkOverlay);
            _blinkOverlay.BringToFront();

            // ── Initial target position ───────────────────────────────────────
            RandomizeTargetPosition();

            // ── Move timer – target teleports every 500ms ─────────────────────
            _moveTimer = new System.Windows.Forms.Timer { Interval = 500 };
            _moveTimer.Tick += (s, e) => { RandomizeTargetPosition(); _gamePanel.Invalidate(); };
            _moveTimer.Start();

            // ── Game timer – 100ms ticks for countdown ────────────────────────
            _gameTimer = new System.Windows.Forms.Timer { Interval = 100 };
            _gameTimer.Tick += GameTimer_Tick;
            _gameTimer.Start();

            // ── Blink timer (Hard only) ───────────────────────────────────────
            _blinkTimer = new System.Windows.Forms.Timer { Interval = 1200 };
            _blinkTimer.Tick += BlinkTimer_Tick;
            if (difficulty == Difficulty.Hard)
                _blinkTimer.Start();
        }

        // ── Paint ──────────────────────────────────────────────────────────────

        private void GamePanel_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var rect = new Rectangle(
                _targetPos.X - TargetRadius,
                _targetPos.Y - TargetRadius,
                TargetRadius * 2,
                TargetRadius * 2);

            g.FillEllipse(new SolidBrush(Color.FromArgb(50, 205, 50)), rect);
            g.DrawEllipse(new Pen(Color.LightGreen, 2), rect);

            using var font = new Font("Segoe UI", 13, FontStyle.Bold);
            var label = "O";
            var sz = g.MeasureString(label, font);
            g.DrawString(label, font, Brushes.White,
                _targetPos.X - sz.Width / 2,
                _targetPos.Y - sz.Height / 2);
        }

        // ── Interaction ────────────────────────────────────────────────────────

        private void GamePanel_MouseClick(object? sender, MouseEventArgs e)
        {
            if (_blinkActive) return; // screen is blacked out

            int dx = e.X - _targetPos.X;
            int dy = e.Y - _targetPos.Y;

            if (dx * dx + dy * dy <= TargetRadius * TargetRadius)
                Resolve(won: true);
        }

        // ── Timer callbacks ────────────────────────────────────────────────────

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            _timeRemainingMs -= 100;
            _countdownLabel.Text = $"Time: {_timeRemainingMs / 1000.0:F1}s";
            if (_timeRemainingMs <= 500)
                _countdownLabel.ForeColor = Color.OrangeRed;
            if (_timeRemainingMs <= 0)
                Resolve(won: false);
        }

        private void BlinkTimer_Tick(object? sender, EventArgs e)
        {
            if (_blinkActive) return;
            _blinkOverlay.Visible = true;
            _blinkActive = true;

            var restoreTimer = new System.Windows.Forms.Timer { Interval = 300 };
            restoreTimer.Tick += (s, _) =>
            {
                ((System.Windows.Forms.Timer)s!).Stop();
                ((System.Windows.Forms.Timer)s!).Dispose();
                _blinkOverlay.Visible = false;
                _blinkActive = false;
            };
            restoreTimer.Start();
        }

        // ── Helpers ────────────────────────────────────────────────────────────

        private void RandomizeTargetPosition()
        {
            int margin = TargetRadius + 4;
            _targetPos = new Point(
                _rng.Next(margin, _gamePanel.Width - margin),
                _rng.Next(margin, _gamePanel.Height - margin));
        }

        private void Resolve(bool won)
        {
            StopTimers();
            DialogResult = won ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        private void StopTimers()
        {
            _moveTimer.Stop();
            _gameTimer.Stop();
            _blinkTimer.Stop();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopTimers();
            base.OnFormClosing(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _moveTimer.Dispose();
                _gameTimer.Dispose();
                _blinkTimer.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
