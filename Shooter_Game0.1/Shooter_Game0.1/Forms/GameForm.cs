using Shooter_Game0._1.Core;
using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Utilities.Randomizer;

namespace Shooter_Game0._1.Forms
{
    public class GameForm : Form
    {
        private const int CellSize = 64;

        private readonly string username;
        private readonly string weaponType;
        private readonly Controller controller;
        private readonly IMap map;

        private int cursorRow;
        private int cursorCol;

        private Panel mapPanel = null!;
        private ListBox combatLog = null!;
        private Label statsLabel = null!;
        private Label enemiesLeftLabel = null!;
        private Label weaponLabel = null!;

        public GameForm(string username, string weaponType)
        {
            this.username = username;
            this.weaponType = weaponType;

            controller = new Controller();
            controller.SetWeaponType(weaponType);

            map = Randomizer.MapRandomizer();
            int enemiesCount = map.X + map.Y;

            InitializeComponents();

            string result = controller.GenerateEnemies(map, enemiesCount);

            LogMessage($"Welcome, {username}!");
            LogMessage($"Equipped weapon: {weaponType}");
            LogMessage(result);
            LogMessage("Click a cell or use WASD + Space to shoot.");
            LogMessage("Press H for hint, R for stats.");

            UpdateEnemiesLeft();
        }

        private void InitializeComponents()
        {
            int mapPixelW = map.Y * CellSize;
            int mapPixelH = map.X * CellSize;
            int logWidth = 360;
            int bottomBar = 55;

            Text = $"Shooter Game — {username}";
            ClientSize = new Size(mapPixelW + logWidth + 30, Math.Max(mapPixelH, 300) + bottomBar + 20);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(25, 25, 35);
            KeyPreview = true;
            DoubleBuffered = true;

            // ── Map panel ──
            mapPanel = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(mapPixelW, mapPixelH),
                BackColor = Color.FromArgb(40, 40, 50),
                BorderStyle = BorderStyle.FixedSingle
            };
            mapPanel.Paint += MapPanel_Paint;
            mapPanel.MouseClick += MapPanel_MouseClick;

            // ── Combat log ──
            combatLog = new ListBox
            {
                Location = new Point(mapPixelW + 20, 10),
                Size = new Size(logWidth, mapPixelH - 90),
                BackColor = Color.FromArgb(20, 20, 30),
                ForeColor = Color.LightGreen,
                Font = new Font("Consolas", 9),
                BorderStyle = BorderStyle.FixedSingle,
                SelectionMode = SelectionMode.None,
                HorizontalScrollbar = true
            };

            // ── Stats labels ──
            statsLabel = new Label
            {
                Location = new Point(mapPixelW + 20, mapPixelH - 70),
                Size = new Size(logWidth, 40),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9),
                Text = "Kills: 0 | Damage: 0 | Points: 0"
            };

            enemiesLeftLabel = new Label
            {
                Location = new Point(mapPixelW + 20, mapPixelH - 30),
                Size = new Size(logWidth, 25),
                ForeColor = Color.Yellow,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            // ── Bottom bar ──
            weaponLabel = new Label
            {
                Location = new Point(10, mapPixelH + 20),
                AutoSize = true,
                ForeColor = Color.Cyan,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Text = $"Weapon: {weaponType}"
            };

            var hintButton = new Button
            {
                Text = "HINT (H)",
                Location = new Point(mapPixelW - 230, mapPixelH + 15),
                Size = new Size(105, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(80, 80, 20),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            hintButton.FlatAppearance.BorderSize = 0;
            hintButton.Click += (s, e) => ShowHint();

            var endButton = new Button
            {
                Text = "END GAME",
                Location = new Point(mapPixelW - 115, mapPixelH + 15),
                Size = new Size(105, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(180, 40, 40),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            endButton.FlatAppearance.BorderSize = 0;
            endButton.Click += (s, e) => EndGame();

            Controls.AddRange([
                mapPanel, combatLog, statsLabel, enemiesLeftLabel,
                weaponLabel, hintButton, endButton
            ]);

            KeyDown += GameForm_KeyDown;
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
            int col = e.X / CellSize;
            int row = e.Y / CellSize;

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

            // Draw grid cells
            using var cellBrush = new SolidBrush(Color.FromArgb(50, 50, 60));
            using var gridPen = new Pen(Color.FromArgb(70, 70, 80));
            using var coordFont = new Font("Consolas", 7);

            for (int row = 0; row < map.X; row++)
            {
                for (int col = 0; col < map.Y; col++)
                {
                    int x = col * CellSize;
                    int y = row * CellSize;

                    g.FillRectangle(cellBrush, x + 1, y + 1, CellSize - 2, CellSize - 2);
                    g.DrawRectangle(gridPen, x, y, CellSize, CellSize);
                    g.DrawString($"{row},{col}", coordFont, Brushes.DimGray, x + 2, y + 2);
                }
            }

            // Draw enemies
            using var enemyFont = new Font("Segoe UI", 14, FontStyle.Bold);
            foreach (var kvp in controller.EnemiesCoordinates)
            {
                foreach (var pos in kvp.Key)
                {
                    int row = pos.Key;
                    int col = pos.Value;
                    int x = col * CellSize;
                    int y = row * CellSize;

                    Color color = GetEnemyColor(kvp.Value);
                    using var brush = new SolidBrush(color);

                    int pad = 8;
                    g.FillEllipse(brush, x + pad, y + pad,
                        CellSize - pad * 2, CellSize - pad * 2);

                    string initial = kvp.Value.GetType().Name[..1];
                    var sz = g.MeasureString(initial, enemyFont);
                    g.DrawString(initial, enemyFont, Brushes.White,
                        x + (CellSize - sz.Width) / 2,
                        y + (CellSize - sz.Height) / 2);
                }
            }

            // Draw player cursor / crosshair
            int cx = cursorCol * CellSize;
            int cy = cursorRow * CellSize;
            using var cursorPen = new Pen(Color.FromArgb(0, 180, 255), 3);
            g.DrawRectangle(cursorPen, cx + 3, cy + 3, CellSize - 6, CellSize - 6);

            int mx = cx + CellSize / 2;
            int my = cy + CellSize / 2;
            using var crossPen = new Pen(Color.FromArgb(0, 180, 255), 1);
            g.DrawLine(crossPen, mx - 14, my, mx + 14, my);
            g.DrawLine(crossPen, mx, my - 14, mx, my + 14);
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
    }
}
