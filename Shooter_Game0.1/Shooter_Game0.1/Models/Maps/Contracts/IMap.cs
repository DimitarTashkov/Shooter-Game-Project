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
        public int[,] MapTerrain { get; }
        public string[,] GenerateTerrain();
    }
}
