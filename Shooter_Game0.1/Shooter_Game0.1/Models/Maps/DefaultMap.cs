using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Models.Maps
{
    public class DefaultMap : Map
    {
        private const int DefaultX = 5;
        private const int DefaultY = 5;
        public DefaultMap() : base(DefaultX, DefaultY)
        {
        }
    }
}
