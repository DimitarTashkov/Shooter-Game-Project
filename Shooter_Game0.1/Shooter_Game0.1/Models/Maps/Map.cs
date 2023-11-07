using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Models.Maps
{
    public abstract class Map : IMap
    {
        private static int DefaultX = 5;
        private static int DefaultY = 5;

        
        private int x;
        private int y;
        private string[,] terrain;
        public Map(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get { return x; }
            set 
            {
                if(value<= 0 || value >= 10)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidCoordinates));
                }
                x = value; 
            }
        }

        public int Y
        {
            get { return y; }
            set
            {
                if (value <= 0 || value >= 10)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidCoordinates));
                }
                y = value;
            }
        }
        public string[,] Terrain 
        {
            get { return terrain; }
            set
            {
                terrain = value;
            }
        }

        public void GenerateTerrain()
        {
            //NOTE!!! our terrain size might not always be square
            //We can make it to return the array but I prefer void method more
            string[,] generatedTerrain = new string[x, y];
            for (int rows = 0; rows < generatedTerrain.GetLength(0); rows++)
            {
                for (int cols = 0; cols < generatedTerrain.GetLength(1); cols++)
                {
                    generatedTerrain[rows, cols] = "-";
                }
                Console.WriteLine();
            }
            terrain = generatedTerrain;
        }

        public void VisualizeMap(string[,] terrain)
        {
            for (int rows = 0; rows < terrain.GetLength(0); rows++)
            {
                for (int cols = 0; cols < terrain.GetLength(1); cols++)
                {
                    Console.Write(terrain[rows, cols]);
                }
                Console.WriteLine();
            }
        }
        public bool CoordinateIsAlreadyInhabitated(Dictionary<int,int> enemyCoordinate,Dictionary<Dictionary<int,int>,IEnemy> enemiescoordinates)
        {
            int x = 0;
            int y = 0;
            foreach (var kvp in enemyCoordinate)
            {
                x = kvp.Key;
                y = kvp.Value;
            }
            foreach (var kvp in enemiescoordinates)
            {
                Dictionary<int, int> coordinate = kvp.Key;

                // Check if the current dictionary entry's coordinates match the target coordinates
                if (coordinate.ContainsKey(x) && coordinate[x] == y)
                {
                    
                    return true;
                }
            }
            return false;
        }
    }     
}
