using System.IO;
using System.Text.Json;
using Shooter_Game0._1.Models.SaveData;

namespace Shooter_Game0._1.Utilities.Serialization
{
    public static class GameSerializer
    {
        private const string SaveFilePath = "savegame.json";

        public static void SaveGame(SessionState state)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(state, options);
            File.WriteAllText(SaveFilePath, jsonString);
        }

        public static SessionState? LoadGame()
        {
            if (!File.Exists(SaveFilePath)) return null;
            string jsonString = File.ReadAllText(SaveFilePath);
            return JsonSerializer.Deserialize<SessionState>(jsonString);
        }
    }
}