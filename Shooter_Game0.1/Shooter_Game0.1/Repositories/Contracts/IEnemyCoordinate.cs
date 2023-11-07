using Shooter_Game0._1.Models.Enemies.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Repositories.Contracts
{
    public interface IEnemyCoordinate
    {
        public Dictionary<Dictionary<int, int>, IEnemy> Enemiescoordinates { get; set; }
        void AddEnemy(Dictionary<int, int> coordinate, IEnemy enemy);
        bool RemoveEnemy(Dictionary<int, int> coordinate);
    }
}
