using Shooter_Game0._1.Utilities;

namespace Shooter_Game0._1.Models.SaveData
{
    public class SessionState
    {
        public string Username { get; set; } = string.Empty;
        public string WeaponType { get; set; } = string.Empty;

        public Difficulty Difficulty { get; set; } = Difficulty.Easy;

        public double DamageDealt { get; set; }
        public int EnemiesKilled { get; set; }
        public double Points { get; set; }

        public string MapType { get; set; } = string.Empty;
        public int MapX { get; set; }
        public int MapY { get; set; }

        public List<EnemyState> Enemies { get; set; } = new();
        public List<string> CombatLog { get; set; } = new();
        public List<MoveState> MoveHistory { get; set; } = new();
    }

    public class EnemyState
    {
        public string EnemyType { get; set; } = string.Empty;
        public double Life { get; set; }
        public bool IsAlreadyGenerated { get; set; }
        public bool IsEnemyKilled { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
    }

    public class MoveState
    {
        public int Row { get; set; }
        public int Col { get; set; }
    }
}
