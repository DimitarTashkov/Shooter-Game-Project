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
            IWeapon rifle = new Rifle();

            string rifleName = rifle.GetType().Name;
            double rifleAmmos = rifle.AmmoType;
            double riflePower = rifle.Power;

            rifle.CalculateDamage();
            double rifleDamage = rifle.Damage;

            Assert.AreEqual(rifle.GetType().Name, rifleName);
            Assert.AreEqual(rifle.AmmoType, rifleAmmos);
            Assert.AreEqual(rifle.Power, riflePower);
            Assert.AreEqual(rifle.Damage, rifleDamage);
        }
        [Test]
        public void TestShotgunConstructor()
        {
            IWeapon shotgun = new Shotgun();

            string shotgunName = shotgun.GetType().Name;
            double shotgunAmmos = shotgun.AmmoType;
            double shotgunPower = shotgun.Power;

            shotgun.CalculateDamage();
            double shotgunDamage = shotgun.Damage;

            Assert.AreEqual(shotgun.GetType().Name, shotgunName);
            Assert.AreEqual(shotgun.AmmoType, shotgunAmmos);
            Assert.AreEqual(shotgun.Power, shotgunPower);
            Assert.AreEqual(shotgun.Damage, shotgunDamage);
        }
        [Test]
        public void TestSniperConstructor()
        {
            IWeapon sniper = new Sniper();

            string sniperName = sniper.GetType().Name;
            double sniperAmmo = sniper.AmmoType;
            double sniperPower = sniper.Power;

            sniper.CalculateDamage();
            double sniperDamage = sniper.Damage;

            Assert.AreEqual(sniper.GetType().Name, sniperName);
            Assert.AreEqual(sniper.AmmoType, sniperAmmo);
            Assert.AreEqual(sniper.Power, sniperPower);
            Assert.AreEqual(sniper.Damage, sniperDamage);
        }
        [Test]
        public void TestOrcConstructor()
        {
            IEnemy orc = new Orc();

            string orcName = orc.GetType().Name;
            int orcSize = orc.EnemySize;
            double orcHealth = orc.EnemyHealth;

            orc.CalculateLife();
            double orcLife = orc.Life;

            Assert.AreEqual(orc.GetType().Name,orcName);
            Assert.AreEqual(orc.EnemySize, orcSize);
            Assert.AreEqual(orc.EnemyHealth, orcHealth);
            Assert.AreEqual(orc.Life, orcLife);
        }
        [Test]
        public void TestTankConstructor()
        {
            IEnemy tank = new Tank();

            string tankName = tank.GetType().Name;
            int tankSize = tank.EnemySize;
            double tankHealth = tank.EnemyHealth;

            tank.CalculateLife();
            double tankLife = tank.Life;

            Assert.AreEqual(tank.GetType().Name, tankName);
            Assert.AreEqual(tank.EnemySize, tankSize);
            Assert.AreEqual(tank.EnemyHealth, tankHealth);
            Assert.AreEqual(tank.Life, tankLife);
        }
        [Test]
        public void TestWarriorConstructor()
        {
            IEnemy warrior = new Warrior();

            string warriorName = warrior.GetType().Name;
            int warriorSize = warrior.EnemySize;
            double warriorHealth = warrior.EnemyHealth;

            warrior.CalculateLife();
            double warriorLife = warrior.Life;

            Assert.AreEqual(warrior.GetType().Name, warriorName);
            Assert.AreEqual(warrior.EnemySize, warriorSize);
            Assert.AreEqual(warrior.EnemyHealth, warriorHealth);
            Assert.AreEqual(warrior.Life, warriorLife);
        }
        [Test]
        public void TestWizardConstructor()
        {
            IEnemy wizard = new Wizard();

            string wizardName = wizard.GetType().Name;
            int wizardSize = wizard.EnemySize;
            double wizardHealth = wizard.EnemyHealth;

            wizard.CalculateLife();
            double wizardLife = wizard.Life;

            Assert.AreEqual(wizard.GetType().Name, wizardName);
            Assert.AreEqual(wizard.EnemySize, wizardSize);
            Assert.AreEqual(wizard.EnemyHealth, wizardHealth);
            Assert.AreEqual(wizard.Life, wizardLife);
        }
        [Test]
        public void TestDefaultMapConstructor()
        {
            IMap defaultMap = new DefaultMap();

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
            IMap customMap = new CustomMap(3,4);

            customMap.GenerateTerrain();
            int[,] customMapTerrain = customMap.MapTerrain;
            int customX = customMap.X;
            int customY = customMap.Y;

            Assert.AreEqual(customX, customMap.X);
            Assert.AreEqual(customY, customMap.Y);
            Assert.AreEqual(customMapTerrain, customMap.MapTerrain);

        }
        [Test]
        public void TestRegenerationShouldReturnFalse()
        {
            IEnemy orc = new Orc();
            IEnemy tank = new Tank();
            IEnemy warrior = new Warrior();
            IEnemy wizard = new Wizard();

            Assert.AreEqual(orc.IsAlreadyGenerated, false);
            Assert.AreEqual(tank.IsAlreadyGenerated, false);
            Assert.AreEqual(warrior.IsAlreadyGenerated, false);
            Assert.AreEqual(wizard.IsAlreadyGenerated, false);
        }
        [Test]
        public void TestRegenerationShouldReturnTrue()
        {
            IEnemy orc = new Orc();
            IEnemy tank = new Tank();
            IEnemy warrior = new Warrior();
            IEnemy wizard = new Wizard();

            orc.RegenHealth();
            tank.RegenHealth();
            warrior.RegenHealth();
            wizard.RegenHealth();

            double orcLife = orc.Life;
            double tankLife = tank.Life;
            double warriorLife = warrior.Life;
            double wizardLife = wizard.Life;

            Assert.AreEqual(orc.Life, orcLife);
            Assert.AreEqual(tank.Life, tankLife);
            Assert.AreEqual(warrior.Life, warriorLife);
            Assert.AreEqual(wizard.Life, wizardLife);

            Assert.AreEqual(orc.IsAlreadyGenerated, true);
            Assert.AreEqual(tank.IsAlreadyGenerated, true);
            Assert.AreEqual(warrior.IsAlreadyGenerated, true);
            Assert.AreEqual(wizard.IsAlreadyGenerated, true);

        }
        [Test]
        public void TestRegenerationCorrectMessageReturn()
        {
            IEnemy orc = new Orc();
            IEnemy tank = new Tank();
            IEnemy warrior = new Warrior();
            IEnemy wizard = new Wizard();

            orc.CalculateLife();
            tank.CalculateLife();
            warrior.CalculateLife();
            wizard.CalculateLife();

            double orcInitialLife = orc.Life;
            double tankInitialLife = tank.Life;
            double warriorInitilalLife = warrior.Life;
            double wizardInitialLife = wizard.Life;

           string orcReturnMessage =  orc.RegenHealth();
            string tankReturnMessage = tank.RegenHealth();
            string warriorReturnMessage = warrior.RegenHealth();
            string wizardReturnMessage = wizard.RegenHealth();

            double orcRegenerationCalculation = (orcInitialLife / 10) * 3;
            double tankRegenerationCalculation = (tankInitialLife / 10) * 4;
            double warriorRegenerationCalculation = (warriorInitilalLife / 10);
            double wizardRegenerationCalculation = (wizardInitialLife / 10) * 2;

            Assert.AreEqual(orcReturnMessage, $"{orc.GetType().Name} has regenerated {orcRegenerationCalculation}");
            Assert.AreEqual(tankReturnMessage, $"{tank.GetType().Name} has regenerated {tankRegenerationCalculation}");
            Assert.AreEqual(warriorReturnMessage, $"{warrior.GetType().Name} has regenerated {warriorRegenerationCalculation}");
            Assert.AreEqual(wizardReturnMessage, $"{wizard.GetType().Name} has regenerated {wizardRegenerationCalculation}");
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
