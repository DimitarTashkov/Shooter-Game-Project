// File: Forms/MainMenuForm.cs
using Shooter_Game0._1.Utilities.Serialization;

namespace Shooter_Game0._1.Forms
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object? sender, EventArgs e)
        {
            using var setupForm = new SetupForm();
            if (setupForm.ShowDialog() == DialogResult.OK)
            {
                Hide();
                // Phase 1: pass selected difficulty to GameForm
                var gameForm = new GameForm(setupForm.Username, setupForm.SelectedWeapon, setupForm.SelectedDifficulty);
                gameForm.FormClosed += (s, args) => Show();
                gameForm.Show();
            }
        }

        private void LoadGameButton_Click(object? sender, EventArgs e)
        {
            var state = GameSerializer.LoadGame();
            if (state == null)
            {
                MessageBox.Show("No saved game found.", "Load Game",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Hide();
            // Difficulty is restored from SessionState inside the GameForm(state) constructor
            var gameForm = new GameForm(state);
            gameForm.FormClosed += (s, args) => Show();
            gameForm.Show();
        }

        private void ExitButton_Click(object? sender, EventArgs e) => Application.Exit();

        private void LeaderboardButton_Click(object? sender, EventArgs e)
        {
            using var leaderboardForm = new LeaderboardForm();
            leaderboardForm.ShowDialog();
        }
    }
}
