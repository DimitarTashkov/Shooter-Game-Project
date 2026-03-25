// File: Repositories/UsersRepository.cs
using Shooter_Game0._1.Models.Users;
using Shooter_Game0._1.Models.Users.Contracts;
using Shooter_Game0._1.Repositories.Contracts;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Shooter_Game0._1.Repositories
{
    public class UsersRepository : IRepository<IUser>
    {
        private const string FilePath = "leaderboard.json";

        private readonly List<IUser> users;

        public UsersRepository()
        {
            users = LoadUsers();
        }

        public IReadOnlyCollection<IUser> Models() => users.AsReadOnly();

        public void AddNew(IUser user)
        {
            users.Add(user);
            SaveUsers();
        }

        public bool RemoveByName(string typeName)
        {
            var target = users.FirstOrDefault(x => x.Username == typeName);
            if (target == null) return false;
            users.Remove(target);
            SaveUsers();
            return true;
        }

        // ── Persistence ───────────────────────────────────────────────────────

        private void SaveUsers()
        {
            var records = users
                .OfType<User>()
                .Select(u => new UserRecord(u.Username, u.EnemiesKilled, u.DamageDealt, u.Points))
                .ToList();

            string json = JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        private static List<IUser> LoadUsers()
        {
            if (!File.Exists(FilePath))
                return new List<IUser>();

            string json = File.ReadAllText(FilePath);
            var records = JsonSerializer.Deserialize<List<UserRecord>>(json);
            if (records == null) return new List<IUser>();

            return records.Select(r =>
            {
                var u = new User(r.Username)
                {
                    EnemiesKilled = r.EnemiesKilled,
                    DamageDealt   = r.DamageDealt,
                    Points        = r.Points
                };
                return (IUser)u;
            }).ToList();
        }

        // ── Private DTO (serialization only) ─────────────────────────────────

        private record UserRecord(
            string Username,
            int    EnemiesKilled,
            double DamageDealt,
            double Points);
    }
}
