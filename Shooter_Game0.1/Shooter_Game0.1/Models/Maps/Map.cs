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
        private int[,] mapTerrain;

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
                if(value<= 0 || value > 10)
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
                if (value <= 0 || value > 10)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidCoordinates));
                }
                y = value;
            }
        }
        public int[,] MapTerrain
        {
            get { return mapTerrain; }
            set
            {
                if(mapTerrain.GetLength(0) <= 0 || mapTerrain.GetLength(1) <= 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.MapRowsOrColsShouldNotBeEmpty));
                }
                mapTerrain = value;
            }
        }

        public string[,] GenerateTerrain()
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
            return generatedTerrain;
        }
        public void VisualizeMap(string[,] map)
        {
            for (int rows = 0; rows < map.GetLength(0); rows++)
            {
                for (int cols = 0; cols < map.GetLength(1); cols++)
                {
                    Console.Write(map[rows,cols]);
                }
                Console.WriteLine();
            }
        }
    }
}
