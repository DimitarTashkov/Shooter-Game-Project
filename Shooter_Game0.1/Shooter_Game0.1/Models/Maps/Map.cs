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
        private int x;
        private int y;

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

        public int[,] GenerateTerrain()
        {
            throw new NotImplementedException();
        }
    }
}
