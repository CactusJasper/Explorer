using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Explorer.Player;

namespace Explorer.FileHandling {
    public static class SaveHandler {
        // Overhaul when save games are to be implemented
        public static void SavePlayerData(PlayerData playerData) {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = $"{Application.persistentDataPath}/data.explorer";
            FileStream stream = new FileStream(path, FileMode.Create);
            Player.PlayerDataSerializable data = new Player.PlayerDataSerializable(playerData);
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static Player.PlayerDataSerializable LoadPlayerData() {
            string path = $"{Application.persistentDataPath}/data.explorer";
            if(!File.Exists(path)) return null;
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Player.PlayerDataSerializable data = (Player.PlayerDataSerializable)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
    }
}
