// File: Forms/LeaderboardForm.cs
using Shooter_Game0._1.Repositories;
using System.Linq;
using System.Windows.Forms;

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
            var repo = new UsersRepository();

            var scores = repo.Models()
                .OrderByDescending(u => u.Points)
                .Take(10)
                .Select((u, i) => new
                {
                    Rank          = i + 1,
                    u.Username,
                    Score         = u.Points,
                    EnemiesKilled = u.EnemiesKilled
                })
                .ToList();

            scoresGrid.DataSource = scores;
        }

        private void CloseButton_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
