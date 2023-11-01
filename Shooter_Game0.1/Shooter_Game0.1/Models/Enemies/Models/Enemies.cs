using Shooter_Game0._1.Core;
using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Utilities.Messages;
using Shooter_Game0._1.Utilities.Randomizer;
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
        private int enemySize;
        private double enemyHealth;
        private double life;
        private bool isAlreadyRegenerated;
        private bool isEnemyKilled;
        DataBuilder builder;


        public Enemies(int enemySize, double enemyHealth)
        {
            this.enemySize = enemySize;
            this.enemyHealth = enemyHealth;
            builder = new DataBuilder();
        }
        //Validation is optinal as our enemies have greater stats than the validation.
        //NOTE: if you change the stats to invalid one you will need it tho

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

        public double Life
        {
            get { return life; }
            set
            {
  
                life = enemyHealth*enemySize;
            }
        }
        public bool IsAlreadyGenerated
        {
            get { return isAlreadyRegenerated; }
            set
            {

                isAlreadyRegenerated = value;
            }
        }
        public bool IsEnemyKilled
        {
            get { return isEnemyKilled; }
            set
            {

                isEnemyKilled = value;
            }
        }

        public abstract string RegenHealth();

        public void RunCoordinates(IMap map,IEnemy enemy, Dictionary<Dictionary<int, int>, IEnemy> enemiesCoordinates)
        {

            Dictionary<int, int> newCoordinates = Randomizer.EnemiesGenerationRandomizer(map); 
            
            while(enemiesCoordinates.ContainsKey(newCoordinates))
            {
                 newCoordinates = Randomizer.EnemiesGenerationRandomizer(map);
            }

            enemiesCoordinates.Add(newCoordinates, enemy);                      
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Enemy Name: {this.GetType().Name}");
            sb.AppendLine($"Enemy size: {EnemySize}");
            sb.AppendLine($"Enemy Life: {Life}");
            sb.AppendLine($"Has enemy regenerated health: {IsAlreadyGenerated}");
            return sb.ToString().Trim();
        }


    }
}
