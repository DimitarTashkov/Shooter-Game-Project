using Microsoft.EntityFrameworkCore;

namespace Shooter_Game0._1.Data
{
    public class ShooterGameContext : DbContext
    {
        public DbSet<PlayerScore> PlayerScores { get; set; }

        private readonly string _dbPath;

        public ShooterGameContext()
        {
            _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "shootergame.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerScore>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Score);
                entity.Property(e => e.DateAchieved);
            });
        }

        public void SaveScore(string username, double score)
        {
            PlayerScores.Add(new PlayerScore
            {
                Username = username,
                Score = score,
                DateAchieved = DateTime.Now
            });
            SaveChanges();
        }

        public List<PlayerScore> GetTopScores(int count = 10)
        {
            return PlayerScores
                .OrderByDescending(s => s.Score)
                .Take(count)
                .ToList();
        }
    }
}
