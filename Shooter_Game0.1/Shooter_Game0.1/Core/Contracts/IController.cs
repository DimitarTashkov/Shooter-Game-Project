using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Models.Users.Contracts;
using Shooter_Game0._1.Models.Weapons.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Core.Contracts
{
    public interface IController
    {
        public Dictionary<Dictionary<int, int>, IEnemy> EnemiesCoordinates { get; }
        public string GenerateEnemies(IMap map, int countOfEnemies);
        public string Shoot(int xCoordinate, int yCoordinate);
        public string Hint
            (int xCoordinate, int yCoordinate, string[,] terrain,Dictionary<Dictionary<int,int>,IEnemy> enemiesCoordinates);
        public void StatsUpdate(string username);
        public void Report();

    }
}
