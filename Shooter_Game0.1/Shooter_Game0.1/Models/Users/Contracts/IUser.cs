using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Models.Users.Contracts
{
    public interface IUser
    {
        public string Username { get; }
        public int EnemiesKilled { get; set; }
        public double DamageDealt { get; set; }
        public double Points { get; set; }
    }
}
