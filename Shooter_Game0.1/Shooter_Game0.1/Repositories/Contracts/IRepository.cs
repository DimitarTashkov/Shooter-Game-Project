using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Repositories.Contracts
{
    public interface IRepository<T>
    {
        IReadOnlyCollection<T> Models();

        void AddNew(T name);

        bool RemoveByName(string typeName);
    }
}
