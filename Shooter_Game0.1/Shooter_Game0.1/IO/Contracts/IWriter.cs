using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.IO.Contracts
{
    public interface IWriter
    {
        void Write(string message);
        void WriteLine(string message);
    }
}
