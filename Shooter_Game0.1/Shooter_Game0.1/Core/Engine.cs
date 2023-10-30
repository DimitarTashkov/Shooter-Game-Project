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
            Console.WriteLine("Welcome to our game! This is your generated map:");
            Console.WriteLine("Enter one of the following command: Shoot/StatsUpdate/Report");
            controller.GenerateEnemies(map, enemiesCount);

            string command = string.Empty;
            while (command  != "Report")
            {
                switch (command)
                {
                    case "Shoot":

                        int coordinateX = int.Parse(reader.ReadLine());
                        int coordinatesY = int.Parse(reader.ReadLine());
                        while(coordinateX < 0 || coordinateX >= map.X || coordinatesY< 0 || coordinatesY >= map.Y)
                        {
                            writer.WriteLine(string.Format(ExceptionMessages.ShootInbound, map.X, map.Y));
                             coordinateX = int.Parse(reader.ReadLine());
                             coordinatesY = int.Parse(reader.ReadLine());
                        }

                        controller.Shoot(coordinateX, coordinatesY);
                        break;

                    case "StatsUpdate":

                        string username = reader.ReadLine();
                        controller.StatsUpdate(username);
                        break;


                }
                command = reader.ReadLine();
            }
            controller.Report();

        }           
        
    }
}
