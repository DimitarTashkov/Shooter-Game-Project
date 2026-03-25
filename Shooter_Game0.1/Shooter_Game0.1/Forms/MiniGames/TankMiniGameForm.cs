// File: Forms/MiniGames/TankMiniGameForm.cs
// Phase 2 – TANK mini-game: "Break the Armor!"
// 3 shields of different sizes appear. Player must click them smallest → largest.
// Clicking wrong order = immediate fail. Hard mode: random blackout blink.
using System;
using System.Drawing;
using System.Windows.Forms;
using Shooter_Game0._1.Utilities;

namespace Shooter_Game0._1.Forms.MiniGames
{
    public class TankMiniGameForm : Form
    {
        private readonly System.Windows.Forms.Timer _gameTimer;
        private readonly System.Windows.Forms.Timer _blinkTimer;

        private readonly Panel _gamePanel;
        private readonly Label _countdownLabel;
        private readonly Label _stepLabel;
        private readonly Panel _blinkOverlay;

        // Shield data: 3 shields, sorted radii: [0]=small, [1]=medium, [2]=large
        private static readonly int[] ShieldRadii   = { 22, 38, 56 };
        private static readonly string[] ShieldNames = { "Small", "Medium", "Large" };
        private readonly Point[] _shieldPositions = new Point[3];
        private readonly bool[] _shieldClicked = new bool[3];

        private int _currentStep = 0;   // which shield must be clicked next (0=small first)
        private int _timeRemainingMs = 5000;
        private bool _blinkActive = false;
        private readonly Random _rng = new Random();

        private static readonly Color[] ShieldColors =
        {
            Color.FromArgb(60, 180, 230),    // small  – light blue
            Color.FromArgb(230, 160, 30),    // medium – gold
            Color.FromArgb(180, 50,  50)     // large  – dark red
        };

        public TankMiniGameForm(Difficulty difficulty)
        {
            Text = "TANK MINI-GAME: Break the Armor!";
            Size = new Size(500, 420);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.FromArgb(18, 18, 28);

            var instruction = new Label
            {
                Text = difficulty == Difficulty.Hard
                    ? "HARD MODE: Click shields  Small → Medium → Large! Blackouts active!"
                    : "Click the shields in order:  Small → Medium → Large!",
                ForeColor = difficulty == Difficulty.Hard ? Color.OrangeRed : Color.SteelBlue,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(10, 5),
                Size = new Size(470, 22),
                TextAlign = ContentAlignment.MiddleCenter
            };

            _countdownLabel = new Label
            {
                Text = "Time: 5.0s",
                ForeColor = Color.Yellow,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Location = new Point(190, 30),
                Size = new Size(120, 26),
                TextAlign = ContentAlignment.MiddleCenter
            };

            _stepLabel = new Label
            {
                Text = "Step 1 of 3: Click the SMALL (blue) shield",
                ForeColor = Color.LightCyan,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 58),
                Size = new Size(470, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            _gamePanel = new Panel
            {
                Location = new Point(10, 82),
                Size = new Size(464, 290),
                BackColor = Color.FromArgb(28, 28, 40),
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
            Controls.Add(_stepLabel);
            Controls.Add(_gamePanel);
            Controls.Add(_blinkOverlay);
            _blinkOverlay.BringToFront();

            PlaceShields();

            _gameTimer = new System.Windows.Forms.Timer { Interval = 100 };
            _gameTimer.Tick += GameTimer_Tick;
            _gameTimer.Start();

            _blinkTimer = new System.Windows.Forms.Timer { Interval = 1200 };
            _blinkTimer.Tick += BlinkTimer_Tick;
            if (difficulty == Difficulty.Hard)
                _blinkTimer.Start();
        }

        // ── Shield placement ───────────────────────────────────────────────────

        private void PlaceShields()
        {
            // Divide the panel into 3 horizontal zones, place each shield in one zone.
            // Shuffle which zone gets which shield so the layout isn't always identical.
            int[] zones = { 0, 1, 2 };
            for (int i = 2; i > 0; i--)
            {
                int j = _rng.Next(i + 1);
                (zones[i], zones[j]) = (zones[j], zones[i]);
            }

            int zoneW = _gamePanel.Width / 3;
            for (int i = 0; i < 3; i++)
            {
                int zone = zones[i];
                int r = ShieldRadii[i];
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
                if (_shieldClicked[i]) continue; // already destroyed

                int r = ShieldRadii[i];
                var rect = new Rectangle(_shieldPositions[i].X - r, _shieldPositions[i].Y - r, r * 2, r * 2);

                // Glow for the "currently expected" shield
                bool isNext = (i == _currentStep);
                using var fill = new SolidBrush(ShieldColors[i]);
                g.FillEllipse(fill, rect);

                using var borderPen = new Pen(isNext ? Color.White : Color.FromArgb(100, 100, 100), isNext ? 3 : 1);
                g.DrawEllipse(borderPen, rect);

                using var font = new Font("Segoe UI", Math.Max(r / 2.5f, 8), FontStyle.Bold);
                string lbl = ShieldNames[i][0].ToString(); // S, M, L
                var sz = g.MeasureString(lbl, font);
                g.DrawString(lbl, font, isNext ? Brushes.White : Brushes.DimGray,
                    _shieldPositions[i].X - sz.Width / 2,
                    _shieldPositions[i].Y - sz.Height / 2);
            }
        }

        // ── Interaction ────────────────────────────────────────────────────────

        private void GamePanel_MouseClick(object? sender, MouseEventArgs e)
        {
            if (_blinkActive) return;

            for (int i = 0; i < 3; i++)
            {
                if (_shieldClicked[i]) continue;

                int r = ShieldRadii[i];
                int dx = e.X - _shieldPositions[i].X;
                int dy = e.Y - _shieldPositions[i].Y;

                if (dx * dx + dy * dy <= r * r)
                {
                    if (i != _currentStep)
                    {
                        // Wrong order → lose
                        Resolve(won: false);
                        return;
                    }

                    _shieldClicked[i] = true;
                    _currentStep++;
                    _gamePanel.Invalidate();

                    if (_currentStep >= 3)
                    {
                        Resolve(won: true);
                        return;
                    }

                    UpdateStepLabel();
                    return;
                }
            }
        }

        // ── Timers ─────────────────────────────────────────────────────────────

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

        // ── Helpers ────────────────────────────────────────────────────────────

        private void UpdateStepLabel()
        {
            if (_currentStep < 3)
                _stepLabel.Text = $"Step {_currentStep + 1} of 3: Click the {ShieldNames[_currentStep].ToUpper()} shield";
        }

        private void Resolve(bool won)
        {
            _gameTimer.Stop();
            _blinkTimer.Stop();
            DialogResult = won ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _gameTimer.Stop();
            _blinkTimer.Stop();
            base.OnFormClosing(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _gameTimer.Dispose();
                _blinkTimer.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
