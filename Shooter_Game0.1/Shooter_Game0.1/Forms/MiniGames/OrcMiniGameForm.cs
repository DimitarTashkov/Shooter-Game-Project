// File: Forms/MiniGames/OrcMiniGameForm.cs
using Shooter_Game0._1.Models.Weapons.Models;
using Shooter_Game0._1.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shooter_Game0._1.Forms.MiniGames
{
    public partial class OrcMiniGameForm : Form
    {
        private readonly System.Windows.Forms.Timer _moveTimer;
        private readonly System.Windows.Forms.Timer _gameTimer;
        private readonly System.Windows.Forms.Timer _blinkTimer;

        private Point _targetPos;
        private const int TargetRadius   = 22;
        private int  _timeRemainingMs    = 3000;
        private bool _blinkActive        = false;
        private readonly Random _rng     = new Random();
        private readonly string _weaponType;

        public OrcMiniGameForm(Difficulty difficulty, string weaponType)
        {
            InitializeComponent();
            _weaponType = weaponType;

            if (difficulty == Difficulty.Hard)
            {
                _instructionLabel.Text      = "HARD MODE: Click the Orc before time runs out! Watch for blackouts!";
                _instructionLabel.ForeColor = Color.OrangeRed;
            }

            RandomizeTargetPosition();

            _moveTimer = new System.Windows.Forms.Timer { Interval = 500 };
            _moveTimer.Tick += (s, e) => { RandomizeTargetPosition(); _gamePanel.Invalidate(); };
            _moveTimer.Start();

            _gameTimer = new System.Windows.Forms.Timer { Interval = 100 };
            _gameTimer.Tick += GameTimer_Tick;
            _gameTimer.Start();

            _blinkTimer = new System.Windows.Forms.Timer { Interval = 1200 };
            _blinkTimer.Tick += BlinkTimer_Tick;
            if (difficulty == Difficulty.Hard)
                _blinkTimer.Start();

            // Weapon effect fires once the form is fully visible
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

            g.FillEllipse(new SolidBrush(Color.FromArgb(50, 205, 50)), rect);
            g.DrawEllipse(new Pen(Color.LightGreen, 2), rect);

            using var font = new Font("Segoe UI", 13, FontStyle.Bold);
            var sz = g.MeasureString("O", font);
            g.DrawString("O", font, Brushes.White,
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

        // ── Helpers ────────────────────────────────────────────────────────────

        private void RandomizeTargetPosition()
        {
            int margin = TargetRadius + 4;
            _targetPos = new Point(
                _rng.Next(margin, _gamePanel.Width  - margin),
                _rng.Next(margin, _gamePanel.Height - margin));
        }

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
