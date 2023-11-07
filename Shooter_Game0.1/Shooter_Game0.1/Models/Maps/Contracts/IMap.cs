using Shooter_Game0._1.Models.Enemies.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Models.Map.Contracts
{
    public interface IMap
    {
        public int X { get; }
        public int Y { get; }
        public string[,] Terrain { get; set; }
        public void GenerateTerrain();
        public void VisualizeMap(string[,] map);
        public bool CoordinateIsAlreadyInhabitated(Dictionary<int,int> enemyCoordinate, Dictionary<Dictionary<int, int>, IEnemy> enemiescoordinates);
    }
}
