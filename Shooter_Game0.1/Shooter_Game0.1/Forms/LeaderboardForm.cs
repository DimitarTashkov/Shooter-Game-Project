using Shooter_Game0._1.Data;

namespace Shooter_Game0._1.Forms
{
    public partial class LeaderboardForm : Form
    {
        public LeaderboardForm()
        {
            InitializeComponent();
            LoadScores();
        }

        private void LoadScores()
        {
            try
            {
                using var context = new ShooterGameContext();
                context.Database.EnsureCreated();

                var scores = context.GetTopScores(10);
                scoresGrid.DataSource = scores.Select((s, i) => new
                {
                    Rank = i + 1,
                    s.Username,
                    s.Score,
                    Date = s.DateAchieved.ToString("yyyy-MM-dd HH:mm")
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load leaderboard: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CloseButton_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
