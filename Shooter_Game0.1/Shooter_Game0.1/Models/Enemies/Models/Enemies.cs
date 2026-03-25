using Shooter_Game0._1.Core;
using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Utilities;
using Shooter_Game0._1.Utilities.Messages;
using Shooter_Game0._1.Utilities.Randomizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shooter_Game0._1.Models.Enemies.Models
{
    public abstract class Enemy : IEnemy
    {
        private int enemySize;
        private double enemyHealth;
        private bool isAlreadyRegenerated;
        private bool isEnemyKilled;
        private double life;

        private readonly double initialLife;

        private static readonly Random _rng = new Random();
        DataBuilder builder;

        protected Enemy(int enemySize, double enemyHealth)
        {
            this.enemySize = enemySize;
            this.enemyHealth = enemyHealth;
            builder = new DataBuilder();
            this.life = enemySize * enemyHealth;
            this.initialLife = this.life;
        }


        public int EnemySize
        {
            get { return enemySize; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException(string.Format(ExceptionMessages.IvalidHealthValue));
                enemySize = value;
            }
        }

        public double EnemyHealth
        {
            get { return enemyHealth; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException(string.Format(ExceptionMessages.IvalidHealthValue));
                enemyHealth = value;
            }
        }

        public double Life
        {
            get { return life; }
            set { life = value; }
        }

        public bool IsAlreadyGenerated
        {
            get { return isAlreadyRegenerated; }
            set { isAlreadyRegenerated = value; }
        }

        public bool IsEnemyKilled
        {
            get { return isEnemyKilled; }
            set { isEnemyKilled = value; }
        }

        public abstract string RegenHealth();


        public abstract bool SpecialMove(Difficulty difficulty, string weaponType);


        public virtual bool TryRebirth(Difficulty difficulty)
        {
            int chancePercent = difficulty switch
            {
                Difficulty.Easy   => 10,
                Difficulty.Medium => 25,
                Difficulty.Hard   => 40,
                _                 => 0
            };

            int roll = _rng.Next(1, 101); // 1–100 inclusive
            if (roll <= chancePercent)
            {
                Life = initialLife * 0.25;
                return true;
            }
            return false;
        }

        public void RunCoordinates(IMap map, IEnemy enemy, Dictionary<Dictionary<int, int>, IEnemy> enemiesCoordinates)
        {
            Dictionary<int, int> newCoordinates = Randomizer.EnemiesGenerationRandomizer(map);
            while (map.CoordinateIsAlreadyInhabitated(newCoordinates, enemiesCoordinates))
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
