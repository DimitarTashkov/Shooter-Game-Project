using Shooter_Game0._1.Core;
using Shooter_Game0._1.Data;
using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Models.Users.Contracts;
using Shooter_Game0._1.Utilities.Randomizer;

namespace Shooter_Game0._1.Forms
{
    public partial class GameForm : Form
    {
        private readonly string username;
        private readonly string weaponType;
        private readonly Controller controller;
        private readonly IMap map;

        private int cursorRow;
        private int cursorCol;

        /// <summary>
        /// Dynamic cell size calculated from panel dimensions and map grid size.
        /// Supports Phase 3: Dynamic Form Resizing.
        /// </summary>
        private int CellWidth => mapPanel.Width / Math.Max(map.Y, 1);
        private int CellHeight => mapPanel.Height / Math.Max(map.X, 1);

        public GameForm(string username, string weaponType)
        {
            this.username = username;
            this.weaponType = weaponType;

            controller = new Controller();
            controller.SetWeaponType(weaponType);

            map = Randomizer.MapRandomizer();

            InitializeComponent();
            ApplyDynamicLayout();

            int enemiesCount = map.X + map.Y;
            string result = controller.GenerateEnemies(map, enemiesCount);

            LogMessage($"Welcome, {username}!");
            LogMessage($"Equipped weapon: {weaponType}");
            LogMessage(result);
            LogMessage("Click a cell or use WASD + Space to shoot.");
            LogMessage("Press H for hint, R for stats.");

            // Observer Pattern: subscribe to user stats changes
            IUser user = controller.GetOrCreateUser(username);
            user.StatsChanged += OnUserStatsChanged;

            UpdateEnemiesLeft();
        }

        private void ApplyDynamicLayout()
        {
            const int baseCellSize = 64;
            int mapPixelW = map.Y * baseCellSize;
            int mapPixelH = map.X * baseCellSize;
            int logWidth = 360;
            int bottomBar = 55;

            Text = $"Shooter Game — {username}";
            ClientSize = new Size(mapPixelW + logWidth + 30, Math.Max(mapPixelH, 300) + bottomBar + 20);

            mapPanel.Size = new Size(mapPixelW, mapPixelH);
            combatLog.Location = new Point(mapPixelW + 20, 10);
            combatLog.Size = new Size(logWidth, mapPixelH - 90);
            statsLabel.Location = new Point(mapPixelW + 20, mapPixelH - 70);
            statsLabel.Size = new Size(logWidth, 40);
            enemiesLeftLabel.Location = new Point(mapPixelW + 20, mapPixelH - 30);
            enemiesLeftLabel.Size = new Size(logWidth, 25);
            weaponLabel.Location = new Point(10, mapPixelH + 20);
            weaponLabel.Text = $"Weapon: {weaponType}";
            hintButton.Location = new Point(mapPixelW - 230, mapPixelH + 15);
            endButton.Location = new Point(mapPixelW - 115, mapPixelH + 15);

            // Phase 4: Enable optimized double buffering for flicker-free rendering
            SetStyle(ControlStyles.OptimizedDoubleBuffer
                   | ControlStyles.AllPaintingInWmPaint
                   | ControlStyles.UserPaint, true);
            UpdateStyles();

            // Repaint map panel on resize
            mapPanel.Resize += (s, e) => mapPanel.Invalidate();
        }

        // ═══════════════════════════════════════════
        //  INPUT
        // ═══════════════════════════════════════════

        private void GameForm_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W or Keys.Up:
                    if (cursorRow > 0) cursorRow--;
                    break;
                case Keys.S or Keys.Down:
                    if (cursorRow < map.X - 1) cursorRow++;
                    break;
                case Keys.A or Keys.Left:
                    if (cursorCol > 0) cursorCol--;
                    break;
                case Keys.D or Keys.Right:
                    if (cursorCol < map.Y - 1) cursorCol++;
                    break;
                case Keys.Space or Keys.Enter:
                    ShootAt(cursorRow, cursorCol);
                    break;
                case Keys.H:
                    ShowHint();
                    break;
                case Keys.R:
                    ShowStats();
                    break;
                default:
                    return;
            }
            mapPanel.Invalidate();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void MapPanel_MouseClick(object? sender, MouseEventArgs e)
        {
            int col = e.X / CellWidth;
            int row = e.Y / CellHeight;

            if (row >= 0 && row < map.X && col >= 0 && col < map.Y)
            {
                cursorRow = row;
                cursorCol = col;
                ShootAt(row, col);
                mapPanel.Invalidate();
            }
        }

        // ═══════════════════════════════════════════
        //  RENDERING  (GDI+)
        // ═══════════════════════════════════════════

        private void MapPanel_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int cw = CellWidth;
            int ch = CellHeight;

            using var cellBrush = new SolidBrush(Color.FromArgb(50, 50, 60));
            using var gridPen = new Pen(Color.FromArgb(70, 70, 80));
            using var coordFont = new Font("Consolas", Math.Max(cw / 10, 6));

            for (int row = 0; row < map.X; row++)
            {
                for (int col = 0; col < map.Y; col++)
                {
                    int x = col * cw;
                    int y = row * ch;

                    g.FillRectangle(cellBrush, x + 1, y + 1, cw - 2, ch - 2);
                    g.DrawRectangle(gridPen, x, y, cw, ch);
                    g.DrawString($"{row},{col}", coordFont, Brushes.DimGray, x + 2, y + 2);
                }
            }

            float enemyFontSize = Math.Max(Math.Min(cw, ch) / 4.5f, 8f);
            using var enemyFont = new Font("Segoe UI", enemyFontSize, FontStyle.Bold);
            foreach (var kvp in controller.EnemiesCoordinates)
            {
                foreach (var pos in kvp.Key)
                {
                    int row = pos.Key;
                    int col = pos.Value;
                    int x = col * cw;
                    int y = row * ch;

                    Color color = GetEnemyColor(kvp.Value);
                    using var brush = new SolidBrush(color);

                    int padX = cw / 8;
                    int padY = ch / 8;
                    g.FillEllipse(brush, x + padX, y + padY,
                        cw - padX * 2, ch - padY * 2);

                    string initial = kvp.Value.GetType().Name[..1];
                    var sz = g.MeasureString(initial, enemyFont);
                    g.DrawString(initial, enemyFont, Brushes.White,
                        x + (cw - sz.Width) / 2,
                        y + (ch - sz.Height) / 2);
                }
            }

            int cx = cursorCol * cw;
            int cy = cursorRow * ch;
            using var cursorPen = new Pen(Color.FromArgb(0, 180, 255), 3);
            g.DrawRectangle(cursorPen, cx + 3, cy + 3, cw - 6, ch - 6);

            int mx = cx + cw / 2;
            int my = cy + ch / 2;
            using var crossPen = new Pen(Color.FromArgb(0, 180, 255), 1);
            g.DrawLine(crossPen, mx - cw / 4, my, mx + cw / 4, my);
            g.DrawLine(crossPen, mx, my - ch / 4, mx, my + ch / 4);
        }

        // ═══════════════════════════════════════════
        //  GAME ACTIONS
        // ═══════════════════════════════════════════

        private void ShootAt(int row, int col)
        {
            if (controller.EnemiesCoordinates.Count == 0)
            {
                LogMessage("All enemies eliminated! Press End Game for your report.");
                return;
            }

            string result = controller.Shoot(row, col);
            LogMessage(result);
            UpdateEnemiesLeft();

            if (controller.EnemiesCoordinates.Count == 0)
            {
                LogMessage("*** ALL ENEMIES ELIMINATED! ***");
            }
        }

        private void ShowHint()
        {
            if (controller.EnemiesCoordinates.Count == 0)
            {
                LogMessage("No enemies left!");
                return;
            }
            string hint = controller.Hint(cursorRow, cursorCol,
                map.Terrain, controller.EnemiesCoordinates);
            LogMessage($"[HINT] {hint}");
        }

        private void ShowStats()
        {
            controller.StatsUpdate(username);
            string report = controller.GetReport();
            statsLabel.Text = report;
            LogMessage(report);
        }

        private void EndGame()
        {
            controller.StatsUpdate(username);
            string report = controller.GetReport();

            // Save score to leaderboard database
            try
            {
                using var context = new ShooterGameContext();
                context.Database.EnsureCreated();
                double score = controller.GetPlayerPoints(username);
                context.SaveScore(username, score);
            }
            catch (Exception ex)
            {
                LogMessage($"[DB] Could not save score: {ex.Message}");
            }

            MessageBox.Show(report, "Game Over — Final Report",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            Close();
        }

        // ═══════════════════════════════════════════
        //  HELPERS
        // ═══════════════════════════════════════════

        private void UpdateEnemiesLeft()
        {
            enemiesLeftLabel.Text = $"Enemies remaining: {controller.EnemiesCoordinates.Count}";
        }

        private void OnUserStatsChanged(object? sender, UserStatsChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => OnUserStatsChanged(sender, e));
                return;
            }
            statsLabel.Text = $"Kills: {e.EnemiesKilled} | Dmg: {Math.Round(e.DamageDealt, 1)} | Pts: {Math.Round(e.Points, 1)}";
        }

        private void LogMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            foreach (string line in message.Split(
                Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                combatLog.Items.Add(line);
            }

            if (combatLog.Items.Count > 0)
                combatLog.TopIndex = combatLog.Items.Count - 1;
        }

        private static Color GetEnemyColor(IEnemy enemy) => enemy.GetType().Name switch
        {
            "Orc" => Color.FromArgb(50, 205, 50),
            "Tank" => Color.FromArgb(140, 140, 140),
            "Warrior" => Color.FromArgb(220, 20, 60),
            "Wizard" => Color.FromArgb(148, 103, 189),
            _ => Color.Yellow
        };

        private void HintButton_Click(object? sender, EventArgs e) => ShowHint();
        private void EndButton_Click(object? sender, EventArgs e) => EndGame();
    }
}
