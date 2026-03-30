using Shooter_Game0._1.Models.Weapons.Models;
using Shooter_Game0._1.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shooter_Game0._1.Forms.MiniGames
{
    public partial class WarriorMiniGameForm : Form
    {
        private readonly System.Windows.Forms.Timer _moveTimer;
        private readonly System.Windows.Forms.Timer _gameTimer;
        private readonly System.Windows.Forms.Timer _blinkTimer;

        private Point _targetPos;
        private int   _velocityX;
        private int   _velocityY;
        private const int TargetRadius   = 24;
        private int  _timeRemainingMs    = 3000;
        private bool _blinkActive        = false;
        private readonly Random _rng     = new Random();
        private readonly string _weaponType;

        public WarriorMiniGameForm(Difficulty difficulty, string weaponType)
        {
            InitializeComponent();
            _weaponType = weaponType;

            if (difficulty == Difficulty.Hard)
            {
                _instructionLabel.Text      = "HARD MODE: Click the charging Warrior — fast and dark!";
                _instructionLabel.ForeColor = Color.OrangeRed;
            }

            _targetPos = new Point(
                _rng.Next(TargetRadius + 5, _gamePanel.Width  - TargetRadius - 5),
                _rng.Next(TargetRadius + 5, _gamePanel.Height - TargetRadius - 5));

            int speed  = difficulty == Difficulty.Hard ? 9 : 7;
            _velocityX = (_rng.Next(2) == 0 ? 1 : -1) * speed;
            _velocityY = (_rng.Next(2) == 0 ? 1 : -1) * (speed - 2);

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

            Shown += (s, e) => ApplyWeaponEffect();
        }

        // ── Weapon effect ──────────────────────────────────────────────────────

        private void ApplyWeaponEffect()
        {
            switch (_weaponType)
            {
                case "Rifle":
                    new Rifle().SpecialAction(this);
                    break;

                case "Shotgun":
                    bool cleared = new Shotgun().SpecialAction(this);
                    if (!cleared)
                        BeginInvoke(() => Resolve(won: false));
                    break;

                case "Sniper":
                    new Sniper().SpecialAction(this);
                    break;
            }
        }

        // ── Paint ──────────────────────────────────────────────────────────────

        private void GamePanel_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var rect = new Rectangle(
                _targetPos.X - TargetRadius, _targetPos.Y - TargetRadius,
                TargetRadius * 2,            TargetRadius * 2);

            g.FillEllipse(new SolidBrush(Color.FromArgb(220, 20, 60)), rect);
            g.DrawEllipse(new Pen(Color.OrangeRed, 2), rect);

            using var font = new Font("Segoe UI", 13, FontStyle.Bold);
            var sz = g.MeasureString("W", font);
            g.DrawString("W", font, Brushes.White,
                _targetPos.X - sz.Width  / 2,
                _targetPos.Y - sz.Height / 2);
        }

        // ── Mouse click ────────────────────────────────────────────────────────

        private void GamePanel_MouseClick(object? sender, MouseEventArgs e)
        {
            if (_blinkActive) return;
            int dx = e.X - _targetPos.X;
            int dy = e.Y - _targetPos.Y;
            if (dx * dx + dy * dy <= TargetRadius * TargetRadius)
                Resolve(won: true);
        }

        // ── Timer callbacks ────────────────────────────────────────────────────

        private void MoveTimer_Tick(object? sender, EventArgs e)
        {
            _targetPos.X += _velocityX;
            _targetPos.Y += _velocityY;

            int margin = TargetRadius + 2;
            if (_targetPos.X < margin || _targetPos.X > _gamePanel.Width  - margin) _velocityX = -_velocityX;
            if (_targetPos.Y < margin || _targetPos.Y > _gamePanel.Height - margin) _velocityY = -_velocityY;

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
            _moveTimer.Stop(); _gameTimer.Stop(); _blinkTimer.Stop();
            DialogResult = won ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _moveTimer.Stop(); _gameTimer.Stop(); _blinkTimer.Stop();
            base.OnFormClosing(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                _moveTimer?.Dispose();
                _gameTimer?.Dispose();
                _blinkTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
