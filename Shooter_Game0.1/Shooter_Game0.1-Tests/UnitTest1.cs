using Shooter_Game0._1.Models.Enemies.Contracts;
using Shooter_Game0._1.Models.Enemies.Models;
using Shooter_Game0._1.Models.Map.Contracts;
using Shooter_Game0._1.Models.Maps;
using Shooter_Game0._1.Models.Weapons.Contracts;
using Shooter_Game0._1.Models.Weapons.Models;
using Shooter_Game0._1.Utilities.Messages;
using Shooter_Game0._1.Utilities.Randomizer;

namespace Shooter_Game0._1_Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestRifleConstructor()
        {
            IWeapon weapon = new Rifle();

            string weaponName = weapon.GetType().Name;
            double weaponAmmos = weapon.AmmoType;
            double weaponPower = weapon.Power;

            weapon.CalculateDamage();
            double weaponDamage = weapon.Damage;

            Assert.AreEqual(weapon.GetType().Name, weaponName);
            Assert.AreEqual(weapon.AmmoType, weaponAmmos);
            Assert.AreEqual(weapon.Power, weaponPower);
            Assert.AreEqual(weapon.Damage, weaponDamage);
        }
        [Test]
        public void TestShotgunConstructor()
        {
            IWeapon weapon = new Shotgun();

            string weaponName = weapon.GetType().Name;
            double weaponAmmos = weapon.AmmoType;
            double weaponPower = weapon.Power;

            weapon.CalculateDamage();
            double weaponDamage = weapon.Damage;

            Assert.AreEqual(weapon.GetType().Name, weaponName);
            Assert.AreEqual(weapon.AmmoType, weaponAmmos);
            Assert.AreEqual(weapon.Power, weaponPower);
            Assert.AreEqual(weapon.Damage, weaponDamage);
        }
        [Test]
        public void TestSniperConstructor()
        {
            IWeapon weapon = new Sniper();

            string weaponName = weapon.GetType().Name;
            double weaponAmmos = weapon.AmmoType;
            double weaponPower = weapon.Power;

            weapon.CalculateDamage();
            double weaponDamage = weapon.Damage;

            Assert.AreEqual(weapon.GetType().Name, weaponName);
            Assert.AreEqual(weapon.AmmoType, weaponAmmos);
            Assert.AreEqual(weapon.Power, weaponPower);
            Assert.AreEqual(weapon.Damage, weaponDamage);
        }
        [Test]
        public void TestOrcConstructor()
        {
            IEnemy enemy = new Orc();

            string name = enemy.GetType().Name;
            int enemySize = enemy.EnemySize;
            double enemyHealth = enemy.EnemyHealth;

            enemy.CalculateLife();
            double enemyLife = enemy.Life;

            Assert.AreEqual(enemy.GetType().Name,name);
            Assert.AreEqual(enemy.EnemySize, enemySize);
            Assert.AreEqual(enemy.EnemyHealth, enemyHealth);
            Assert.AreEqual(enemy.Life, enemyLife);
        }
        [Test]
        public void TestTankConstructor()
        {
            IEnemy enemy = new Tank();

            string name = enemy.GetType().Name;
            int enemySize = enemy.EnemySize;
            double enemyHealth = enemy.EnemyHealth;

            enemy.CalculateLife();
            double enemyLife = enemy.Life;

            Assert.AreEqual(enemy.GetType().Name, name);
            Assert.AreEqual(enemy.EnemySize, enemySize);
            Assert.AreEqual(enemy.EnemyHealth, enemyHealth);
            Assert.AreEqual(enemy.Life, enemyLife);
        }
        [Test]
        public void TestWarriorConstructor()
        {
            IEnemy enemy = new Warrior();

            string name = enemy.GetType().Name;
            int enemySize = enemy.EnemySize;
            double enemyHealth = enemy.EnemyHealth;

            enemy.CalculateLife();
            double enemyLife = enemy.Life;

            Assert.AreEqual(enemy.GetType().Name, name);
            Assert.AreEqual(enemy.EnemySize, enemySize);
            Assert.AreEqual(enemy.EnemyHealth, enemyHealth);
            Assert.AreEqual(enemy.Life, enemyLife);
        }
        [Test]
        public void TestWizardConstructor()
        {
            IEnemy enemy = new Wizard();

            string name = enemy.GetType().Name;
            int enemySize = enemy.EnemySize;
            double enemyHealth = enemy.EnemyHealth;

            enemy.CalculateLife();
            double enemyLife = enemy.Life;

            Assert.AreEqual(enemy.GetType().Name, name);
            Assert.AreEqual(enemy.EnemySize, enemySize);
            Assert.AreEqual(enemy.EnemyHealth, enemyHealth);
            Assert.AreEqual(enemy.Life, enemyLife);
        }
        [Test]
        public void TestDefaultMapConstructor()
        {
            IMap defaultMap = new Map();

            defaultMap.GenerateTerrain();
            int[,] mapTerrain = defaultMap.MapTerrain;
            int defaultX = defaultMap.X;
            int defaultY = defaultMap.Y;

            Assert.AreEqual(defaultX, defaultMap.X);
            Assert.AreEqual(defaultY, defaultMap.Y);
            Assert.AreEqual(mapTerrain, defaultMap.MapTerrain);
            
        }
        [Test]
        public void TestCustomMapConstructor()
        {
            //NOTE: You see here is not a perfect square and it works
            IMap customMap = new Map(3,4);

            customMap.GenerateTerrain();
            int[,] mapTerrain = customMap.MapTerrain;
            int defaultX = customMap.X;
            int defaultY = customMap.Y;

            Assert.AreEqual(defaultX, customMap.X);
            Assert.AreEqual(defaultY, customMap.Y);
            Assert.AreEqual(mapTerrain, customMap.MapTerrain);

        }
        [Test]
        public void TestRegenerationShouldReturnFalse()
        {
            IEnemy enemy1 = new Orc();
            IEnemy enemy2 = new Tank();
            IEnemy enemy3 = new Warrior();
            IEnemy enemy4 = new Wizard();

            Assert.AreEqual(enemy1.IsAlreadyGenerated, false);
            Assert.AreEqual(enemy2.IsAlreadyGenerated, false);
            Assert.AreEqual(enemy3.IsAlreadyGenerated, false);
            Assert.AreEqual(enemy4.IsAlreadyGenerated, false);
        }
        [Test]
        public void TestRegenerationShouldReturnTrue()
        {
            IEnemy enemy1 = new Orc();
            IEnemy enemy2 = new Tank();
            IEnemy enemy3 = new Warrior();
            IEnemy enemy4 = new Wizard();

            enemy1.RegenHealth();
            enemy2.RegenHealth();
            enemy3.RegenHealth();
            enemy4.RegenHealth();

            double enemy1Life = enemy1.Life;
            double enemy2Life = enemy2.Life;
            double enemy3Life = enemy3.Life;
            double enemy4Life = enemy4.Life;

            Assert.AreEqual(enemy1.Life, enemy1Life);
            Assert.AreEqual(enemy2.Life, enemy2Life);
            Assert.AreEqual(enemy3.Life, enemy3Life);
            Assert.AreEqual(enemy4.Life, enemy4Life);

            Assert.AreEqual(enemy1.IsAlreadyGenerated, true);
            Assert.AreEqual(enemy2.IsAlreadyGenerated, true);
            Assert.AreEqual(enemy3.IsAlreadyGenerated, true);
            Assert.AreEqual(enemy4.IsAlreadyGenerated, true);

        }
        [Test]
        public void TestRegenerationCorrectMessageReturn()
        {
            IEnemy enemy1 = new Orc();
            IEnemy enemy2 = new Tank();
            IEnemy enemy3 = new Warrior();
            IEnemy enemy4 = new Wizard();

            enemy1.CalculateLife();
            enemy2.CalculateLife();
            enemy3.CalculateLife();
            enemy4.CalculateLife();

            double getEnemy1InitialLife = enemy1.Life;
            double getEnemy2InitialLife = enemy2.Life;
            double getEnemy3InitialLife = enemy3.Life;
            double getEnemy4InitialLife = enemy4.Life;

           string getMessage1 =  enemy1.RegenHealth();
            string getMessage2 = enemy2.RegenHealth();
            string getMessage3 = enemy3.RegenHealth();
            string getMessage4 = enemy4.RegenHealth();

            double enemy1RegenerationCalculation = (getEnemy1InitialLife / 10) * 3;
            double enemy2RegenerationCalculation = (getEnemy2InitialLife / 10) * 4;
            double enemy3RegenerationCalculation = (getEnemy3InitialLife / 10);
            double enemy4RegenerationCalculation = (getEnemy4InitialLife / 10) * 2;

            Assert.AreEqual(getMessage1, $"{enemy1.GetType().Name} has regenerated {enemy1RegenerationCalculation}");
            Assert.AreEqual(getMessage2, $"{enemy2.GetType().Name} has regenerated {enemy2RegenerationCalculation}");
            Assert.AreEqual(getMessage3, $"{enemy3.GetType().Name} has regenerated {enemy3RegenerationCalculation}");
            Assert.AreEqual(getMessage4, $"{enemy4.GetType().Name} has regenerated {enemy4RegenerationCalculation}");
        }
        [Test]
        public void TestRegenerationOverflow()
        {
            IEnemy enemy1 = new Orc();
            IEnemy enemy2 = new Tank();
            IEnemy enemy3 = new Warrior();
            IEnemy enemy4 = new Wizard();
            enemy1.RegenHealth();
            enemy2.RegenHealth();
            enemy3.RegenHealth();
            enemy4.RegenHealth();

            string resultMessage1 = string.Format(ExceptionMessages.EnemyHasAlreadyBeenRegenerated, enemy1.GetType().Name);
            string resultMessage2 = string.Format(ExceptionMessages.EnemyHasAlreadyBeenRegenerated, enemy2.GetType().Name);
            string resultMessage3 = string.Format(ExceptionMessages.EnemyHasAlreadyBeenRegenerated, enemy3.GetType().Name);
            string resultMessage4 = string.Format(ExceptionMessages.EnemyHasAlreadyBeenRegenerated, enemy4.GetType().Name);

            Assert.AreEqual(enemy1.RegenHealth(), resultMessage1);
            Assert.AreEqual(enemy2.RegenHealth(), resultMessage2);
            Assert.AreEqual(enemy3.RegenHealth(), resultMessage3);
            Assert.AreEqual(enemy4.RegenHealth(), resultMessage4);

        }
        //NOTE: These tests are only for visualization. They are not always working properly due to the inconsistency of class Random
        //[Test]

        //public void RifleHeadShot()
        //{
        //    IWeapon weapon = new Rifle();
        //    int expectedNumberForHeadShot = 1;
        //    int weaponHeadshotRandomizer = Randomizer.RifleRandomizer();
        //    bool checkForHeadShot = false;
        //    if(weaponHeadshotRandomizer == expectedNumberForHeadShot)
        //    { 
        //        checkForHeadShot = true;
        //    }
        //    Assert.AreEqual(weapon.IsHeadShot(), checkForHeadShot);
        //}
        //[Test]
        //public void ShotgunHeadShot()
        //{
        //    IWeapon weapon = new Shotgun();
        //    int expectedNumberForHeadShot = 1;
        //    Random random = new Random();
        //    int randomizerNumber = random.Next(0, 3);
        //    bool checkForHeadShot = false;
        //    if (randomizerNumber == expectedNumberForHeadShot)
        //    {
        //        checkForHeadShot = true;
        //    }
        //    Assert.AreEqual(weapon.IsHeadShot(), checkForHeadShot);
        //}
        //[Test]
        //public void SniperHeadShot()
        //{
        //    IWeapon weapon = new Sniper();
        //    int expectedNumberForHeadShot = 1;
        //    Random random = new Random();
        //    int randomizerNumber = random.Next(0, 10);
        //    bool checkForHeadShot = false;
        //    if (randomizerNumber == expectedNumberForHeadShot)
        //    {
        //        checkForHeadShot = true;
        //    }
        //    Assert.AreEqual(weapon.IsHeadShot(), checkForHeadShot);
        //}
    }
        
}
