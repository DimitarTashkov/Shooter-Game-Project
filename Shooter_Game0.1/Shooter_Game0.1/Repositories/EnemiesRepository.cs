using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Repositories
{
    public class EnemiesRepository : IRepository<IEnemy>
    {
        private List<IEnemy> enemies;
        public EnemiesRepository()
        {
            enemies = new List<IEnemy>();
        }
        public void AddNew(IEnemy name)
        {
            enemies.Add(name);
        }

        public IReadOnlyCollection<IEnemy> Models() => enemies.AsReadOnly();


        public bool RemoveByName(string typeName)
            => enemies.Remove(enemies.FirstOrDefault(e => e.GetType().Name == typeName));

    }
}
