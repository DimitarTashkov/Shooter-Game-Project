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
                var gameForm = new GameForm(setupForm.Username, setupForm.SelectedWeapon);
                gameForm.FormClosed += (s, args) => Show();
                gameForm.Show();
            }
        }

        private void ExitButton_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
