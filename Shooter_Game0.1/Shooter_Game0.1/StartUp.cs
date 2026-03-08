using Microsoft.EntityFrameworkCore;
using Shooter_Game0._1.Data;
using Shooter_Game0._1.Forms;

namespace Shooter_Game0._1
{
    public class StartUp 
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // Apply any pending EF Core migrations at startup
            using (var context = new ShooterGameContext())
            {
                context.Database.Migrate();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenuForm());
        }
    }
}
