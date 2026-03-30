using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shooter_Game0._1.Forms.MiniGames
{
    public partial class ShotgunJamForm : Form
    {
        // ── Timer ─────────────────────────────────────────────────────────────
        private readonly System.Windows.Forms.Timer _gameTimer;

        // ── Game state ────────────────────────────────────────────────────────
        private int _spaceCount        = 0;
        private const int RequiredPresses = 5;
        private int _timeRemainingMs   = 5000;

        public ShotgunJamForm()
        {
            InitializeComponent();

            _gameTimer = new System.Windows.Forms.Timer { Interval = 100 };
            _gameTimer.Tick += GameTimer_Tick;
            _gameTimer.Start();

            KeyDown += ShotgunJamForm_KeyDown;
        }

        // ── Key handling ───────────────────────────────────────────────────────

        private void ShotgunJamForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space) return;

            _spaceCount++;
            _counterLabel.Text  = $"{_spaceCount} / {RequiredPresses}";
            _progressBar.Value  = Math.Min(_spaceCount, RequiredPresses);

            if (_spaceCount >= RequiredPresses)
                Resolve(cleared: true);

            e.Handled = e.SuppressKeyPress = true;
        }

        // ── Button click ───────────────────────────────────────────────────────

        private void CancelButton_Click(object? sender, EventArgs e) => Resolve(cleared: false);

        // ── Timer callback ─────────────────────────────────────────────────────

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            _timeRemainingMs -= 100;
            _countdownLabel.Text = $"Time: {_timeRemainingMs / 1000.0:F1}s";
            if (_timeRemainingMs <= 2000)
                _countdownLabel.ForeColor = Color.OrangeRed;
            if (_timeRemainingMs <= 0)
                Resolve(cleared: false);
        }

        // ── Resolution ─────────────────────────────────────────────────────────

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
            {
                components?.Dispose();
                _gameTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
