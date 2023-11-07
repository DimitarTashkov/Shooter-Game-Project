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

        public bool RemoveEnemy(Dictionary<int, int> coordinates)
        {
            int x = 0;
            int y = 0;
            foreach (var kvp in coordinates)
            {
                x = kvp.Key;
                y = kvp.Value;
            }
            //ik remove method returns bool but I want extra security
            foreach (var kvp in enemiescoordinates)
            {
                Dictionary<int, int> coordinate = kvp.Key;

                // Check if the current dictionary entry's coordinates match the target coordinates
                if (coordinate.ContainsKey(x) && coordinate[x] == y)
                {
                    enemiescoordinates.Remove(coordinate);
                    return true;
                }
            }
            return false;
        }
    }
}
