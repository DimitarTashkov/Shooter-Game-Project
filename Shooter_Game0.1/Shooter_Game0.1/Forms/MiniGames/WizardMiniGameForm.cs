// File: Forms/MiniGames/WizardMiniGameForm.cs
// Phase 2 – WIZARD mini-game: "Find the Real Wizard!"
// 3 targets: 1 real Wizard (purple, larger), 2 clones (grey, smaller).
// Player must click the real one. Wrong click = instant lose.
// Hard mode: random blackout blink.
using System;
using System.Drawing;
using System.Windows.Forms;
using Shooter_Game0._1.Utilities;

namespace Shooter_Game0._1.Forms.MiniGames
{
    public class WizardMiniGameForm : Form
    {
        private readonly System.Windows.Forms.Timer _gameTimer;
        private readonly System.Windows.Forms.Timer _blinkTimer;

        private readonly Panel _gamePanel;
        private readonly Label _countdownLabel;
        private readonly Panel _blinkOverlay;

        // Target data: position + radius for each of the 3 targets
        private readonly Point[] _positions = new Point[3];
        private readonly int[] _radii = new int[] { 30, 20, 20 }; // index 0 = real (larger)
        private int _realIndex;                                     // shuffled into [0,1,2]
        private int _timeRemainingMs = 8000;
        private bool _blinkActive = false;
        private readonly Random _rng = new Random();

        private static readonly Color ColReal   = Color.FromArgb(148, 103, 189);
        private static readonly Color ColClone  = Color.FromArgb(80,  80,  80);
        private static readonly Color ColBorder = Color.FromArgb(200, 160, 255);

        public WizardMiniGameForm(Difficulty difficulty)
        {
            Text = "WIZARD MINI-GAME: Find the Real Wizard!";
            Size = new Size(500, 420);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.FromArgb(20, 10, 30);

            var instruction = new Label
            {
                Text = difficulty == Difficulty.Hard
                    ? "HARD MODE: Click the REAL Wizard — one chance, watch for blackouts!"
                    : "Click the REAL Wizard (one of the three targets)!",
                ForeColor = difficulty == Difficulty.Hard ? Color.OrangeRed : Color.MediumPurple,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(10, 5),
                Size = new Size(470, 22),
                TextAlign = ContentAlignment.MiddleCenter
            };

            _countdownLabel = new Label
            {
                Text = "Time: 8.0s",
                ForeColor = Color.Yellow,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Location = new Point(190, 30),
                Size = new Size(120, 26),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var legendLabel = new Label
            {
                Text = "Hint: The real one is LARGER and PURPLE!",
                ForeColor = Color.FromArgb(200, 160, 255),
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                Location = new Point(10, 58),
                Size = new Size(470, 18),
                TextAlign = ContentAlignment.MiddleCenter
            };

            _gamePanel = new Panel
            {
                Location = new Point(10, 80),
                Size = new Size(464, 290),
                BackColor = Color.FromArgb(28, 15, 40),
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
            Controls.Add(legendLabel);
            Controls.Add(_gamePanel);
            Controls.Add(_blinkOverlay);
            _blinkOverlay.BringToFront();

            PlaceTargets();

            _gameTimer = new System.Windows.Forms.Timer { Interval = 100 };
            _gameTimer.Tick += GameTimer_Tick;
            _gameTimer.Start();

            _blinkTimer = new System.Windows.Forms.Timer { Interval = 1200 };
            _blinkTimer.Tick += BlinkTimer_Tick;
            if (difficulty == Difficulty.Hard)
                _blinkTimer.Start();
        }

        // ── Target placement ───────────────────────────────────────────────────

        private void PlaceTargets()
        {
            // Assign radii: one slot gets the larger radius (real), two get smaller (clones).
            // Shuffle which slot is "real".
            int[] shuffled = { 0, 1, 2 };
            for (int i = 2; i > 0; i--)
            {
                int j = _rng.Next(i + 1);
                (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
            }
            _realIndex = shuffled[0];

            // Re-assign radii so the real target is always the biggest
            for (int i = 0; i < 3; i++)
                _radii[i] = (i == _realIndex) ? 30 : 18;

            // Place in three non-overlapping zones
            int zoneW = _gamePanel.Width / 3;
            for (int i = 0; i < 3; i++)
            {
                int r = _radii[i];
                int xMin = i * zoneW + r + 10;
                int xMax = (i + 1) * zoneW - r - 10;
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
                int r = _radii[i];
                var rect = new Rectangle(_positions[i].X - r, _positions[i].Y - r, r * 2, r * 2);
                bool isReal = (i == _realIndex);

                using var fill = new SolidBrush(isReal ? ColReal : ColClone);
                g.FillEllipse(fill, rect);
                g.DrawEllipse(isReal ? borderPen : clonePen, rect);

                string lbl = isReal ? "W" : "?";
                using var font = new Font("Segoe UI", r / 1.8f, FontStyle.Bold);
                var sz = g.MeasureString(lbl, font);
                g.DrawString(lbl, font, Brushes.White,
                    _positions[i].X - sz.Width / 2,
                    _positions[i].Y - sz.Height / 2);
            }
        }

        // ── Interaction ────────────────────────────────────────────────────────

        private void GamePanel_MouseClick(object? sender, MouseEventArgs e)
        {
            if (_blinkActive) return;

            for (int i = 0; i < 3; i++)
            {
                int r = _radii[i];
                int dx = e.X - _positions[i].X;
                int dy = e.Y - _positions[i].Y;
                if (dx * dx + dy * dy <= r * r)
                {
                    Resolve(won: i == _realIndex);
                    return;
                }
            }
        }

        // ── Timers ─────────────────────────────────────────────────────────────

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
