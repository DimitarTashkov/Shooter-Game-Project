using Shooter_Game0._1.Core.Contracts;
using Shooter_Game0._1.IO;
using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Models.Weapons.Contracts;
using Shooter_Game0._1.Utilities.Messages;
using Shooter_Game0._1.Utilities.Randomizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Core
{
    public class Engine : IEngine
    {
        //this is our main method. Treat it like input and output commands
        IController controller;
        DataBuilder builder;
        Reader reader;
        Writer writer;
        public Engine()
        {
            controller = new Controller();
            builder = new DataBuilder();
            reader = new Reader();
            writer = new Writer();
        }
        public void Run()
        {
            IMap map = Randomizer.MapRandomizer();
            IEnemy enemy = Randomizer.EnemiesRandomizer();
            IWeapon weapon = Randomizer.WeaponsRandomizer();            
            int maxEnemiesCount = map.X + map.Y;
            Random random = new Random();
            int enemiesCount =  maxEnemiesCount;
            writer.WriteLine("Enter username to begin gameplay");
            string username = reader.ReadLine();
            while(!ValidateUsername(username))
            {
                writer.WriteLine(string.Format(OutputMessages.InvalidUsername));
                username = reader.ReadLine();
            }
            controller.GenerateEnemies(map, enemiesCount);
            writer.WriteLine(string.Format(OutputMessages.WelcomeToOurGame,username));
            writer.WriteLine(string.Format(OutputMessages.GameInfo));

            string command = string.Empty;
            int coordinateX = 0;
            int coordinatesY = 0;
            while (command  != "Report")
            {
                string infoReturn = string.Empty;
                switch (command)
                {
                    case "Shoot":

                         coordinateX = int.Parse(reader.ReadLine());
                         coordinatesY = int.Parse(reader.ReadLine());
                        while(coordinateX < 0 || coordinateX >= map.X || coordinatesY< 0 || coordinatesY >= map.Y)
                        {
                            writer.WriteLine(string.Format(ExceptionMessages.ShootInbound, map.X, map.Y));
                             coordinateX = int.Parse(reader.ReadLine());
                             coordinatesY = int.Parse(reader.ReadLine());
                        }

                       infoReturn = controller.Shoot(coordinateX, coordinatesY);
                        writer.WriteLine(infoReturn);
                        break;

                    case "StatsUpdate":

                        controller.StatsUpdate(username);
                        break;
                    case "Hint":
                        string closestEnemy = controller.Hint(coordinateX, coordinatesY, map.Terrain, controller.EnemiesCoordinates);
                        writer.WriteLine(closestEnemy);
                        break;
                }
                command = reader.ReadLine();
            }
            controller.Report();

        }     
        private bool ValidateUsername(string username)
        {
            if(string.IsNullOrEmpty(username))
            {
                return false;
            }
            return true;
        }
        
    }
}
