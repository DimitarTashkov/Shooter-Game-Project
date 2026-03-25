// File: Forms/MiniGames/WizardMiniGameForm.cs
using Shooter_Game0._1.Models.Weapons.Models;
using Shooter_Game0._1.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shooter_Game0._1.Forms.MiniGames
{
    public partial class WizardMiniGameForm : Form
    {
        private readonly System.Windows.Forms.Timer _gameTimer;
        private readonly System.Windows.Forms.Timer _blinkTimer;

        private readonly Point[] _positions = new Point[3];
        private readonly int[]   _radii     = new int[3];
        private int  _realIndex;
        private int  _timeRemainingMs    = 8000;
        private bool _blinkActive        = false;
        private readonly Random _rng     = new Random();
        private readonly string _weaponType;

        private static readonly Color ColReal   = Color.FromArgb(148, 103, 189);
        private static readonly Color ColClone  = Color.FromArgb(80,  80,  80);
        private static readonly Color ColBorder = Color.FromArgb(200, 160, 255);

        public WizardMiniGameForm(Difficulty difficulty, string weaponType)
        {
            InitializeComponent();
            _weaponType = weaponType;

            if (difficulty == Difficulty.Hard)
            {
                _instructionLabel.Text      = "HARD MODE: Click the REAL Wizard — one chance, watch for blackouts!";
                _instructionLabel.ForeColor = Color.OrangeRed;
            }

            PlaceTargets();

            _gameTimer = new System.Windows.Forms.Timer { Interval = 100 };
            _gameTimer.Tick += GameTimer_Tick;
            _gameTimer.Start();

            _blinkTimer = new System.Windows.Forms.Timer { Interval = 1200 };
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

        // ── Target placement ───────────────────────────────────────────────────

        private void PlaceTargets()
        {
            int[] zones = { 0, 1, 2 };
            for (int i = 2; i > 0; i--)
            {
                int j = _rng.Next(i + 1);
                (zones[i], zones[j]) = (zones[j], zones[i]);
            }
            _realIndex = zones[0];

            for (int i = 0; i < 3; i++)
                _radii[i] = (i == _realIndex) ? 30 : 18;

            int zoneW = _gamePanel.Width / 3;
            for (int i = 0; i < 3; i++)
            {
                int r    = _radii[i];
                int zone = zones[i];
                int xMin = zone * zoneW + r + 10;
                int xMax = (zone + 1) * zoneW - r - 10;
                int yMin = r + 10;
                int yMax = _gamePanel.Height - r - 10;
                _positions[i] = new Point(
                    _rng.Next(xMin, Math.Max(xMin + 1, xMax)),
                    _rng.Next(yMin, Math.Max(yMin + 1, yMax)));
            }
        }

        // ── Paint ──────────────────────────────────────────────────────────────

        private void GamePanel_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using var borderPen = new Pen(ColBorder, 2);
            using var clonePen  = new Pen(Color.Gray, 1);

            for (int i = 0; i < 3; i++)
            {
                int  r      = _radii[i];
                bool isReal = (i == _realIndex);
                var  rect   = new Rectangle(_positions[i].X - r, _positions[i].Y - r, r * 2, r * 2);

                using var fill = new SolidBrush(isReal ? ColReal : ColClone);
                g.FillEllipse(fill, rect);
                g.DrawEllipse(isReal ? borderPen : clonePen, rect);

                string lbl = isReal ? "W" : "?";
                using var font = new Font("Segoe UI", r / 1.8f, FontStyle.Bold);
                var sz = g.MeasureString(lbl, font);
                g.DrawString(lbl, font, Brushes.White,
                    _positions[i].X - sz.Width  / 2,
                    _positions[i].Y - sz.Height / 2);
            }
        }

        // ── Mouse click ────────────────────────────────────────────────────────

        private void GamePanel_MouseClick(object? sender, MouseEventArgs e)
        {
            if (_blinkActive) return;
            for (int i = 0; i < 3; i++)
            {
                int r  = _radii[i];
                int dx = e.X - _positions[i].X;
                int dy = e.Y - _positions[i].Y;
                if (dx * dx + dy * dy <= r * r)
                {
                    Resolve(won: i == _realIndex);
                    return;
                }
            }
        }

        // ── Timer callbacks ────────────────────────────────────────────────────

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            _timeRemainingMs -= 100;
            _countdownLabel.Text = $"Time: {_timeRemainingMs / 1000.0:F1}s";
            if (_timeRemainingMs <= 2000)
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
            _gameTimer.Stop(); _blinkTimer.Stop();
            DialogResult = won ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _gameTimer.Stop(); _blinkTimer.Stop();
            base.OnFormClosing(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                _gameTimer?.Dispose();
                _blinkTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
