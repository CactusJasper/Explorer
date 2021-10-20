using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveHandler
{
    // Rehaaul when save games are to be implmented
    public static void SavePlayerData(PlayerData playerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = $"{Application.persistentDataPath}/data.explorer";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerDataSerilizable data = new PlayerDataSerilizable(playerData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerDataSerilizable LoadPlayerData()
    {
        string path = $"{Application.persistentDataPath}/data.explorer";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerDataSerilizable data = (PlayerDataSerilizable)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }
}
