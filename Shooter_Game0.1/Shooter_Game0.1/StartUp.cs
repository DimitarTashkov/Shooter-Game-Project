// File: StartUp.cs
using Shooter_Game0._1.Forms;

namespace Shooter_Game0._1
{
    public class StartUp
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenuForm());
        }
    }
}
