using Shooter_Game0._1.IO.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.IO
{
    public class Reader : IReader
    {
        public string ReadLine() => Console.ReadLine();        
    }
}
