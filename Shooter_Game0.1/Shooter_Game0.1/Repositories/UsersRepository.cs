using Shooter_Game0._1.Models.Users.Contracts;
using Shooter_Game0._1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Repositories
{
    public class UsersRepository : IRepository<IUser>
    {
        private List<IUser> users;
        public UsersRepository()
        {
            users = new List<IUser>();
        }
        public void AddNew(IUser name)
        {
            users.Add(name);
        }

        public IReadOnlyCollection<IUser> Models() => users.AsReadOnly();

        public bool RemoveByName(string typeName) => users.Remove(users.FirstOrDefault(x => x.Username == typeName));

    }
}
