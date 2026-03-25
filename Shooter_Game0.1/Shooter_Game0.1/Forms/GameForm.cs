// File: Forms/GameForm.cs
using Shooter_Game0._1.Core;
using Shooter_Game0._1.Data;
using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Models.Users.Contracts;
using Shooter_Game0._1.Utilities;
using Shooter_Game0._1.Utilities.Randomizer;
using Shooter_Game0._1.Models.SaveData;
using Shooter_Game0._1.Models.Maps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Shooter_Game0._1.Forms
{
    public partial class GameForm : Form
    {
        private readonly string username;
        private readonly string weaponType;
        private readonly Controller controller;
        private readonly IMap map;
        private readonly Difficulty difficulty;   // Phase 1

        private int cursorRow;
        private int cursorCol;

        private readonly Stack<(int row, int col)> moveHistory = new();
        private readonly Random _rng = new Random();


        private int CellWidth  => mapPanel.Width  / Math.Max(map.Y, 1);
        private int CellHeight => mapPanel.Height / Math.Max(map.X, 1);

        // ── Constructors ──────────────────────────────────────────────────────

        public GameForm(string username, string weaponType, Difficulty difficulty)
        {
            this.username   = username;
            this.weaponType = weaponType;
            this.difficulty = difficulty;

            controller = new Controller();
            controller.SetWeaponType(weaponType);

            map = Randomizer.MapRandomizer();

            InitializeComponent();
            ApplyDynamicLayout();

            int enemiesCount = map.X + map.Y;
            string result = controller.GenerateEnemies(map, enemiesCount);

            LogMessage($"Welcome, {username}!");
            LogMessage($"Equipped weapon: {weaponType}");
            LogMessage($"Difficulty: {difficulty}");
            LogMessage(result);
            LogMessage("Click a cell or WASD + Space to shoot.");
            LogMessage("Press H for hint, R for stats, Ctrl+Z to undo.");

            IUser user = controller.GetOrCreateUser(username);
            user.StatsChanged += OnUserStatsChanged;

            UpdateEnemiesLeft();
        }

        public GameForm(SessionState state)
        {
            this.username   = state.Username;
            this.weaponType = state.WeaponType;
            this.difficulty = state.Difficulty;

            controller = new Controller();

            map = state.MapType == "DefaultMap"
                ? (IMap)new DefaultMap()
                : new CustomMap(state.MapX, state.MapY);

            InitializeComponent();
            ApplyDynamicLayout();

            controller.LoadSessionState(state, map);

            foreach (var log in state.CombatLog)
                LogMessage(log);

            LogMessage($"--- Game Loaded | Difficulty: {difficulty} ---");

            foreach (var move in state.MoveHistory)
                moveHistory.Push((move.Row, move.Col));

            IUser user = controller.GetOrCreateUser(username);
            user.StatsChanged += OnUserStatsChanged;
            user.Points = user.Points;   // triggers stats-changed to refresh UI

            UpdateEnemiesLeft();
        }

        // ── Layout ────────────────────────────────────────────────────────────

        private void ApplyDynamicLayout()
        {
            const int baseCellSize = 64;
            int mapPixelW = map.Y * baseCellSize;
            int mapPixelH = map.X * baseCellSize;
            int logWidth  = 360;
            int bottomBar = 55;

            Text = $"Shooter Game — {username}  [{difficulty}]";
            ClientSize = new Size(mapPixelW + logWidth + 30, Math.Max(mapPixelH, 300) + bottomBar + 20);

            mapPanel.Size     = new Size(mapPixelW, mapPixelH);
            combatLog.Location = new Point(mapPixelW + 20, 10);
            combatLog.Size    = new Size(logWidth, mapPixelH - 90);
            statsLabel.Location = new Point(mapPixelW + 20, mapPixelH - 70);
            statsLabel.Size   = new Size(logWidth, 40);
            enemiesLeftLabel.Location = new Point(mapPixelW + 20, mapPixelH - 30);
            enemiesLeftLabel.Size = new Size(logWidth, 25);
            weaponLabel.Location = new Point(10, mapPixelH + 20);
            weaponLabel.Text  = $"Weapon: {weaponType}  |  Difficulty: {difficulty}";

            int buttonWidth = (logWidth - 15) / 4;
            hintButton.Location = new Point(mapPixelW + 20, mapPixelH + 10);
            hintButton.Size     = new Size(buttonWidth, 38);
            undoButton.Location = new Point(mapPixelW + 20 + buttonWidth + 5, mapPixelH + 10);
            undoButton.Size     = new Size(buttonWidth, 38);
            saveButton.Location = new Point(mapPixelW + 20 + (buttonWidth + 5) * 2, mapPixelH + 10);
            saveButton.Size     = new Size(buttonWidth, 38);
            endButton.Location  = new Point(mapPixelW + 20 + (buttonWidth + 5) * 3, mapPixelH + 10);
            endButton.Size      = new Size(buttonWidth, 38);

            SetStyle(ControlStyles.OptimizedDoubleBuffer
                   | ControlStyles.AllPaintingInWmPaint
                   | ControlStyles.UserPaint, true);
            UpdateStyles();
            mapPanel.Resize += (s, e) => mapPanel.Invalidate();
        }

        // ── Input ─────────────────────────────────────────────────────────────

        private void GameForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                UndoLastMove();
                e.Handled = e.SuppressKeyPress = true;
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.W or Keys.Up:    if (cursorRow > 0) cursorRow--;               break;
                case Keys.S or Keys.Down:  if (cursorRow < map.X - 1) cursorRow++;       break;
                case Keys.A or Keys.Left:  if (cursorCol > 0) cursorCol--;               break;
                case Keys.D or Keys.Right: if (cursorCol < map.Y - 1) cursorCol++;       break;
                case Keys.Space or Keys.Enter: ShootAt(cursorRow, cursorCol);            break;
                case Keys.H: ShowHint();                                                  break;
                case Keys.R: ShowStats();                                                 break;
                default: return;
            }
            mapPanel.Invalidate();
            e.Handled = e.SuppressKeyPress = true;
        }

        private void UndoLastMove()
        {
            string result = controller.UndoLastAction();
            LogMessage(result);

            if (result == "Action undone." && moveHistory.Count > 0)
            {
                var lastMove = moveHistory.Pop();
                cursorRow = lastMove.row;
                cursorCol = lastMove.col;
            }

            UpdateEnemiesLeft();
            mapPanel.Invalidate();
        }

        private void MapPanel_MouseClick(object? sender, MouseEventArgs e)
        {
            int col = e.X / CellWidth;
            int row = e.Y / CellHeight;
            if (row < 0 || row >= map.X || col < 0 || col >= map.Y) return;

            cursorRow = row;
            cursorCol = col;

            if (e.Button == MouseButtons.Right)
            {
                mapPanel.Invalidate();
                return;
            }

            ShootAt(row, col);
            mapPanel.Invalidate();
        }

        // ── Rendering ─────────────────────────────────────────────────────────

        private void MapPanel_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int cw = CellWidth;
            int ch = CellHeight;

            using var cellBrush = new SolidBrush(Color.FromArgb(50, 50, 60));
            using var gridPen   = new Pen(Color.FromArgb(70, 70, 80));
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
                    int x   = col * cw;
                    int y   = row * ch;

                    Color color = GetEnemyColor(kvp.Value);
                    using var brush = new SolidBrush(color);
                    int padX = cw / 8;
                    int padY = ch / 8;
                    g.FillEllipse(brush, x + padX, y + padY, cw - padX * 2, ch - padY * 2);

                    string initial = kvp.Value.GetType().Name[..1];
                    var sz = g.MeasureString(initial, enemyFont);
                    g.DrawString(initial, enemyFont, Brushes.White,
                        x + (cw - sz.Width)  / 2,
                        y + (ch - sz.Height) / 2);
                }
            }

            // Cursor crosshair
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

        // ── Game logic ────────────────────────────────────────────────────────

        /// <summary>
        /// Core shoot method integrating:
        /// - Phase 2: enemy SpecialMove (30% mini-game trigger on hit)
        /// - Phase 4: TryRebirth (handled inside ShootCommand)
        ///</summary>
        private void ShootAt(int row, int col)
        {
            if (controller.EnemiesCoordinates.Count == 0)
            {
                LogMessage("All enemies eliminated! Press End Game for your report.");
                return;
            }

            // ── Phase 2: Check if enemy is at this cell → maybe trigger mini-game ──
            IEnemy? enemy = GetEnemyAt(row, col);
            if (enemy != null && ShouldTriggerSpecialMove())
            {
                LogMessage($"[MINI-GAME] {enemy.GetType().Name} triggers special move!");
                bool playerWon = enemy.SpecialMove(difficulty, weaponType);

                if (playerWon)
                {
                    LogMessage("[MINI-GAME] You WON! Damage applied.");
                }
                else
                {
                    LogMessage("[MINI-GAME] You LOST! Enemy steals 150 points!");
                    ApplySpecialMovePenalty();
                    UpdateEnemiesLeft();
                    mapPanel.Invalidate();
                    return; // no shot fired this turn
                }
            }

            // ── Normal shot ───────────────────────────────────────────────────
            moveHistory.Push((row, col));

            string result = controller.Shoot(row, col, username, difficulty);
            LogMessage(result);
            UpdateEnemiesLeft();

            if (controller.EnemiesCoordinates.Count == 0)
                LogMessage("*** ALL ENEMIES ELIMINATED! ***");
        }

        /// <summary>Returns enemy at (row, col) or null if cell is empty.</summary>
        private IEnemy? GetEnemyAt(int row, int col)
        {
            return controller.EnemiesCoordinates
                .FirstOrDefault(kvp => kvp.Key.ContainsKey(row) && kvp.Key[row] == col)
                .Value;
        }

        /// <summary>30% chance to trigger a mini-game on a successful hit.</summary>
        private bool ShouldTriggerSpecialMove() => _rng.Next(1, 11) <= 3;

        /// <summary>Penalty when the player loses a mini-game: deduct 150 points.</summary>
        private void ApplySpecialMovePenalty()
        {
            IUser user = controller.GetOrCreateUser(username);
            user.Points = Math.Max(0, user.Points - 150);
        }

        private void ShowHint()
        {
            if (controller.EnemiesCoordinates.Count == 0) { LogMessage("No enemies left!"); return; }
            string hint = controller.Hint(cursorRow, cursorCol, map.Terrain, controller.EnemiesCoordinates);
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

        // ── Helpers ───────────────────────────────────────────────────────────

        private void UpdateEnemiesLeft()
        {
            enemiesLeftLabel.Text = $"Enemies remaining: {controller.EnemiesCoordinates.Count}";
        }

        private void OnUserStatsChanged(object? sender, UserStatsChangedEventArgs e)
        {
            if (InvokeRequired) { Invoke(() => OnUserStatsChanged(sender, e)); return; }
            statsLabel.Text = $"Kills: {e.EnemiesKilled} | Dmg: {Math.Round(e.DamageDealt, 1)} | Pts: {Math.Round(e.Points, 1)}";
        }

        private void LogMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;
            int maxExtent = combatLog.HorizontalExtent;
            using (Graphics g = combatLog.CreateGraphics())
            {
                foreach (string line in message.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
                {
                    combatLog.Items.Add(line);
                    int extent = (int)g.MeasureString(line, combatLog.Font).Width;
                    if (extent > maxExtent) maxExtent = extent;
                }
            }
            combatLog.HorizontalExtent = maxExtent + 20;
            if (combatLog.Items.Count > 0)
                combatLog.TopIndex = combatLog.Items.Count - 1;
        }

        private static Color GetEnemyColor(IEnemy enemy) => enemy.GetType().Name switch
        {
            "Orc"     => Color.FromArgb(50,  205, 50),
            "Tank"    => Color.FromArgb(140, 140, 140),
            "Warrior" => Color.FromArgb(220, 20,  60),
            "Wizard"  => Color.FromArgb(148, 103, 189),
            _         => Color.Yellow
        };

        private void HintButton_Click(object? sender, EventArgs e) => ShowHint();
        private void UndoButton_Click(object? sender, EventArgs e) => UndoLastMove();
        private void SaveButton_Click(object? sender, EventArgs e) => SaveGame();
        private void EndButton_Click(object? sender, EventArgs e)  => EndGame();

        private void SaveGame()
        {
            var state = controller.GetSessionState(username, difficulty);

            foreach (var item in combatLog.Items)
                state.CombatLog.Add(item.ToString() ?? string.Empty);

            var moves = moveHistory.ToList();
            moves.Reverse();
            foreach (var m in moves)
                state.MoveHistory.Add(new MoveState { Row = m.row, Col = m.col });

            Shooter_Game0._1.Utilities.Serialization.GameSerializer.SaveGame(state);
            LogMessage("[SYSTEM] Game successfully saved!");
        }
    }
}
