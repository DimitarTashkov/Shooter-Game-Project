// File: Forms/MiniGames/WarriorMiniGameForm.cs
// Phase 2 – WARRIOR mini-game: "Hit the Berserker!"
// A target shoots across the panel very fast. Click it within 3 seconds.
// Hard mode: random blackout blink.
using System;
using System.Drawing;
using System.Windows.Forms;
using Shooter_Game0._1.Utilities;

namespace Shooter_Game0._1.Forms.MiniGames
{
    public class WarriorMiniGameForm : Form
    {
        private readonly System.Windows.Forms.Timer _moveTimer;
        private readonly System.Windows.Forms.Timer _gameTimer;
        private readonly System.Windows.Forms.Timer _blinkTimer;

        private readonly Panel _gamePanel;
        private readonly Label _countdownLabel;
        private readonly Panel _blinkOverlay;

        private Point _targetPos;
        private int _velocityX;
        private int _velocityY;
        private const int TargetRadius = 24;
        private int _timeRemainingMs = 3000;
        private bool _blinkActive = false;
        private readonly Random _rng = new Random();

        public WarriorMiniGameForm(Difficulty difficulty)
        {
            Text = "WARRIOR MINI-GAME: Hit the Berserker!";
            Size = new Size(560, 340);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.FromArgb(30, 8, 8);

            var instruction = new Label
            {
                Text = difficulty == Difficulty.Hard
                    ? "HARD MODE: Click the charging Warrior — fast and dark!"
                    : "Click the charging Warrior before the timer runs out!",
                ForeColor = difficulty == Difficulty.Hard ? Color.OrangeRed : Color.Tomato,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(10, 5),
                Size = new Size(530, 22),
                TextAlign = ContentAlignment.MiddleCenter
            };

            _countdownLabel = new Label
            {
                Text = "Time: 3.0s",
                ForeColor = Color.Yellow,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Location = new Point(215, 30),
                Size = new Size(120, 26),
                TextAlign = ContentAlignment.MiddleCenter
            };

            _gamePanel = new Panel
            {
                Location = new Point(10, 60),
                Size = new Size(524, 230),
                BackColor = Color.FromArgb(45, 15, 15),
                BorderStyle = BorderStyle.FixedSingle
            };
            _gamePanel.Paint += GamePanel_Paint;
            _gamePanel.MouseClick += GamePanel_MouseClick;

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

            // Start target at random position, moving fast
            _targetPos = new Point(
                _rng.Next(TargetRadius + 5, _gamePanel.Width - TargetRadius - 5),
                _rng.Next(TargetRadius + 5, _gamePanel.Height - TargetRadius - 5));

            int speed = difficulty == Difficulty.Hard ? 9 : 7;
            _velocityX = (_rng.Next(0, 2) == 0 ? 1 : -1) * speed;
            _velocityY = (_rng.Next(0, 2) == 0 ? 1 : -1) * (speed - 2);

            // Movement timer – 16ms ≈ 60 fps
            _moveTimer = new System.Windows.Forms.Timer { Interval = 16 };
            _moveTimer.Tick += MoveTimer_Tick;
            _moveTimer.Start();

            _gameTimer = new System.Windows.Forms.Timer { Interval = 100 };
            _gameTimer.Tick += GameTimer_Tick;
            _gameTimer.Start();

            _blinkTimer = new System.Windows.Forms.Timer { Interval = 1000 };
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

            g.FillEllipse(new SolidBrush(Color.FromArgb(220, 20, 60)), rect);
            g.DrawEllipse(new Pen(Color.OrangeRed, 2), rect);

            using var font = new Font("Segoe UI", 13, FontStyle.Bold);
            var sz = g.MeasureString("W", font);
            g.DrawString("W", font, Brushes.White,
                _targetPos.X - sz.Width / 2,
                _targetPos.Y - sz.Height / 2);
        }

        // ── Interaction ────────────────────────────────────────────────────────

        private void GamePanel_MouseClick(object? sender, MouseEventArgs e)
        {
            if (_blinkActive) return;

            int dx = e.X - _targetPos.X;
            int dy = e.Y - _targetPos.Y;
            if (dx * dx + dy * dy <= TargetRadius * TargetRadius)
                Resolve(won: true);
        }

        // ── Timers ─────────────────────────────────────────────────────────────

        private void MoveTimer_Tick(object? sender, EventArgs e)
        {
            _targetPos.X += _velocityX;
            _targetPos.Y += _velocityY;

            int margin = TargetRadius + 2;
            if (_targetPos.X < margin || _targetPos.X > _gamePanel.Width - margin)
                _velocityX = -_velocityX;
            if (_targetPos.Y < margin || _targetPos.Y > _gamePanel.Height - margin)
                _velocityY = -_velocityY;

            _targetPos.X = Math.Clamp(_targetPos.X, margin, _gamePanel.Width  - margin);
            _targetPos.Y = Math.Clamp(_targetPos.Y, margin, _gamePanel.Height - margin);

            _gamePanel.Invalidate();
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            _timeRemainingMs -= 100;
            _countdownLabel.Text = $"Time: {_timeRemainingMs / 1000.0:F1}s";
            if (_timeRemainingMs <= 1000)
                _countdownLabel.ForeColor = Color.OrangeRed;
            if (_timeRemainingMs <= 0)
                Resolve(won: false);
        }

        private void BlinkTimer_Tick(object? sender, EventArgs e)
        {
            if (_blinkActive) return;
            _blinkOverlay.Visible = true;
            _blinkActive = true;

            var restore = new System.Windows.Forms.Timer { Interval = 300 };
            restore.Tick += (s, _) =>
            {
                ((System.Windows.Forms.Timer)s!).Stop();
                ((System.Windows.Forms.Timer)s!).Dispose();
                _blinkOverlay.Visible = false;
                _blinkActive = false;
            };
            restore.Start();
        }

        // ── Resolution ─────────────────────────────────────────────────────────

        private void Resolve(bool won)
        {
            _moveTimer.Stop();
            _gameTimer.Stop();
            _blinkTimer.Stop();
            DialogResult = won ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _moveTimer.Stop();
            _gameTimer.Stop();
            _blinkTimer.Stop();
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
