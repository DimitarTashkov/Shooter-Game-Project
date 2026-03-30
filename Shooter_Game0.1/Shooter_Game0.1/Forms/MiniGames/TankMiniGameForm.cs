using Shooter_Game0._1.Models.Weapons.Models;
using Shooter_Game0._1.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shooter_Game0._1.Forms.MiniGames
{
    public partial class TankMiniGameForm : Form
    {
        private readonly System.Windows.Forms.Timer _gameTimer;
        private readonly System.Windows.Forms.Timer _blinkTimer;

        private static readonly int[]    ShieldRadii  = { 22, 38, 56 };
        private static readonly string[] ShieldNames  = { "Small", "Medium", "Large" };

        private static readonly Color[] ShieldColors =
        {
            Color.FromArgb(60, 180, 230),
            Color.FromArgb(230, 160, 30),
            Color.FromArgb(180, 50,  50)
        };

        private readonly Point[] _shieldPositions = new Point[3];
        private readonly bool[]  _shieldClicked   = new bool[3];

        private int  _currentStep     = 0;
        private int  _timeRemainingMs = 5000;
        private bool _blinkActive     = false;
        private readonly Random _rng  = new Random();
        private readonly string _weaponType;

        public TankMiniGameForm(Difficulty difficulty, string weaponType)
        {
            InitializeComponent();
            _weaponType = weaponType;

            if (difficulty == Difficulty.Hard)
            {
                _instructionLabel.Text      = "HARD MODE: Click shields  Small \u2192 Medium \u2192 Large! Blackouts active!";
                _instructionLabel.ForeColor = Color.OrangeRed;
            }

            PlaceShields();

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

        // ── Shield placement ───────────────────────────────────────────────────

        private void PlaceShields()
        {
            int[] zones = { 0, 1, 2 };
            for (int i = 2; i > 0; i--)
            {
                int j = _rng.Next(i + 1);
                (zones[i], zones[j]) = (zones[j], zones[i]);
            }

            int zoneW = _gamePanel.Width / 3;
            for (int i = 0; i < 3; i++)
            {
                int r    = ShieldRadii[i];
                int zone = zones[i];
                int xMin = zone * zoneW + r + 8;
                int xMax = (zone + 1) * zoneW - r - 8;
                int yMin = r + 8;
                int yMax = _gamePanel.Height - r - 8;
                _shieldPositions[i] = new Point(
                    _rng.Next(xMin, Math.Max(xMin + 1, xMax)),
                    _rng.Next(yMin, Math.Max(yMin + 1, yMax)));
            }
        }

        // ── Paint ──────────────────────────────────────────────────────────────

        private void GamePanel_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            for (int i = 0; i < 3; i++)
            {
                if (_shieldClicked[i]) continue;

                int  r      = ShieldRadii[i];
                bool isNext = (i == _currentStep);
                var  rect   = new Rectangle(_shieldPositions[i].X - r, _shieldPositions[i].Y - r, r * 2, r * 2);

                using var fill = new SolidBrush(ShieldColors[i]);
                g.FillEllipse(fill, rect);

                using var pen = new Pen(isNext ? Color.White : Color.FromArgb(100, 100, 100), isNext ? 3 : 1);
                g.DrawEllipse(pen, rect);

                string lbl = ShieldNames[i][0].ToString();
                using var font = new Font("Segoe UI", Math.Max(r / 2.5f, 8), FontStyle.Bold);
                var sz = g.MeasureString(lbl, font);
                g.DrawString(lbl, font,
                    isNext ? Brushes.White : Brushes.DimGray,
                    _shieldPositions[i].X - sz.Width  / 2,
                    _shieldPositions[i].Y - sz.Height / 2);
            }
        }

        // ── Mouse click ────────────────────────────────────────────────────────

        private void GamePanel_MouseClick(object? sender, MouseEventArgs e)
        {
            if (_blinkActive) return;

            for (int i = 0; i < 3; i++)
            {
                if (_shieldClicked[i]) continue;

                int r  = ShieldRadii[i];
                int dx = e.X - _shieldPositions[i].X;
                int dy = e.Y - _shieldPositions[i].Y;

                if (dx * dx + dy * dy <= r * r)
                {
                    if (i != _currentStep) { Resolve(won: false); return; }

                    _shieldClicked[i] = true;
                    _currentStep++;
                    _gamePanel.Invalidate();

                    if (_currentStep >= 3) { Resolve(won: true); return; }

                    _stepLabel.Text = $"Step {_currentStep + 1} of 3: Click the {ShieldNames[_currentStep].ToUpper()} shield";
                    return;
                }
            }
        }

        // ── Timer callbacks ────────────────────────────────────────────────────

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            _timeRemainingMs -= 100;
            _countdownLabel.Text = $"Time: {_timeRemainingMs / 1000.0:F1}s";
            if (_timeRemainingMs <= 1500)
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
