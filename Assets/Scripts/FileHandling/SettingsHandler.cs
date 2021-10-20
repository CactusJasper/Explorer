using UnityEngine;

public class SettingsHandler
{
    public static void SaveMappedKey(string keyLabel, KeyCode key)
    {
        PlayerPrefs.SetInt(keyLabel, (int)key);
    }

    public static KeyCode GetMappedKey(string keyLabel)
    {
        return (KeyCode)PlayerPrefs.GetInt(keyLabel);
    }

    public static bool IsKeyMapped(string keyLabel)
    {
        return PlayerPrefs.HasKey(keyLabel);
    }
}
