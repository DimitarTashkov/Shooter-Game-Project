namespace Shooter_Game0._1.Data
{
    public class PlayerScore
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public double Score { get; set; }
        public DateTime DateAchieved { get; set; } = DateTime.Now;
    }
}
