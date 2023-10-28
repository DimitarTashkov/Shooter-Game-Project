using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Models.Maps
{
    public class Map : IMap
    {
        private static int DefaultX = 5;
        private static int DefaultY = 5;

        private int x;
        private int y;
        private int[,] mapTerrain;

        public Map()
        {
            this.x = DefaultX;
            this.y = DefaultY;
        }
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

        public void GenerateTerrain()
        {
            //NOTE!!! our terrain size might not always be square
            //We can make it to return the array but I prefer void method more
            int[,] generatedTerrain = new int[x, y];
            mapTerrain = generatedTerrain;
        }
    }
}
