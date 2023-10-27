using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shooter_Game0._1.Models.Enemies.Models
{
    public abstract class Enemies : IEnemy
    {
        private string name;
        private int enemySize;
        private double enemyHealth;


        public Enemies(string name,int enemySize, double enemyHealth)
        {
            this.name = name;
            this.enemySize = enemySize;
            this.enemyHealth = enemyHealth;
        }
        public string Name 
        {
            get { return name; }
            set 
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.EmptyName));
                }
                name = value;
            }
        }

        public int EnemySize
        {
            get { return enemySize; }
            set
            {
                if(value <= 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.IvalidHealthValue));
                }
                enemySize = value;
            }
        }

        public double EnemyHealth
        {
            get { return enemyHealth; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.IvalidHealthValue));
                }
                enemyHealth = value;
            }
        }

        public double Life => Math.Round(enemySize * enemyHealth,2);


    }
}
