using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Repositories
{
    public class EnemiesCoordinatesRepository : IEnemyCoordinate
    {
        private Dictionary<Dictionary<int, int>, IEnemy> enemiescoordinates;
        public EnemiesCoordinatesRepository()
        {
            enemiescoordinates = new Dictionary<Dictionary<int, int>, IEnemy>();
        }

        public Dictionary<Dictionary<int, int>, IEnemy> Enemiescoordinates
        {
            get { return enemiescoordinates;}
            set { enemiescoordinates = value; }
        }

        public void AddEnemy(Dictionary<int, int> coordinate, IEnemy enemy)
        {
            enemiescoordinates.Add(coordinate, enemy);
        }

        public bool RemoveEnemy(Dictionary<int, int> coordinate)
        {
            //ik remove method returns bool but I want extra security
            if(!enemiescoordinates.ContainsKey(coordinate))
            {
                return false;
            }
            enemiescoordinates.Remove(coordinate);
            return true;
        }
    }
}
