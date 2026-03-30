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

        public void UpdateModel(IUser model)
        {
            int index = users.FindIndex(u => u.Username == model.Username);
            if (index >= 0)
                users[index] = model;
            else
                users.Add(model);

            SaveUsers();
        }

        // ── Persistence ───────────────────────────────────────────────────────

        private void SaveUsers()
        {
            var dtos = users.Select(u => new UserDTO
            {
                Username      = u.Username,
                EnemiesKilled = u.EnemiesKilled,
                DamageDealt   = u.DamageDealt,
                Points        = u.Points
            }).ToList();

            string json = JsonSerializer.Serialize(dtos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        private List<IUser> LoadUsers()
        {
            if (!File.Exists(FilePath))
                return new List<IUser>();

            string json = File.ReadAllText(FilePath);
            var dtos = JsonSerializer.Deserialize<List<UserDTO>>(json);
            if (dtos == null) return new List<IUser>();

            return dtos.Select(dto =>
            {
                var user = new User(dto.Username)
                {
                    EnemiesKilled = dto.EnemiesKilled,
                    DamageDealt   = dto.DamageDealt,
                    Points        = dto.Points
                };
                return (IUser)user;
            }).ToList();
        }

        private class UserDTO
        {
            public string Username      { get; set; } = string.Empty;
            public int    EnemiesKilled { get; set; }
            public double DamageDealt   { get; set; }
            public double Points        { get; set; }
        }
    }
}
