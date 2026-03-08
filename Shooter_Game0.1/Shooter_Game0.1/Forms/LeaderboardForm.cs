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
            using var context = new ShooterGameContext();

            var scores = context.GetTopScores(10);
            scoresGrid.DataSource = scores.Select((s, i) => new
            {
                Rank = i + 1,
                s.Username,
                s.Score,
                Date = s.DateAchieved.ToString("yyyy-MM-dd HH:mm")
            }).ToList();

            scoresGrid.Columns["Rank"].Width = 50;
            scoresGrid.Columns["Username"].Width = 150;
            scoresGrid.Columns["Score"].Width = 100;
            scoresGrid.Columns["Date"].Width = 140;
        }

        private void CloseButton_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
