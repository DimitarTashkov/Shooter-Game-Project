using Shooter_Game0._1.Models.Users.Contracts;
using Shooter_Game0._1.Utilities.Messages;
using Shooter_Game0._1.Utilities.Randomizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Models.Users
{
    public class User : IUser
    {
        private string username;
        private int enemiesKilled;
        private double damageDealt;
        private double points;

        public event EventHandler<UserStatsChangedEventArgs>? StatsChanged;

        public User(string username)
        {
            this.username = username;
        }

        public string Username
        {
            get { return username; }
            set
            {
                if (string.IsNullOrEmpty(username))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.EmptyOrInvalidUsername));
                }
                username = value;
            }
        }

        public int EnemiesKilled
        {
            get { return enemiesKilled; }
            set
            {
                enemiesKilled = value;
                OnStatsChanged();
            }
        }
        public double DamageDealt
        {
            get { return damageDealt; }
            set
            {
                damageDealt = value;
                OnStatsChanged();
            }
        }
        public double Points
        {
            get { return points; }
            set
            {
                points = value;
                OnStatsChanged();
            }
        }

        private void OnStatsChanged()
        {
            StatsChanged?.Invoke(this, new UserStatsChangedEventArgs
            {
                EnemiesKilled = enemiesKilled,
                DamageDealt = damageDealt,
                Points = points
            });
        }
    }
}
