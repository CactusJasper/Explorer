using Explorer.FileHandling;
using UnityEngine;

namespace Explorer.Input {
    public class InputHandler : MonoBehaviour {
        // TODO: Implement remapping and saving to the config
        public KeyCode moveLeft = KeyCode.A;
        public KeyCode moveRight = KeyCode.D;
        public KeyCode jump = KeyCode.Space;
        public KeyCode mineKey = KeyCode.Mouse0;

        private void Awake() {
            DontDestroyOnLoad(gameObject);

            if(SettingsHandler.IsKeyMapped("moveLeft"))
                moveLeft = SettingsHandler.GetMappedKey("moveLeft");
            else
                SettingsHandler.SaveMappedKey("moveLeft", moveLeft);

            if(SettingsHandler.IsKeyMapped("moveRight"))
                moveRight = SettingsHandler.GetMappedKey("moveRight");
            else
                SettingsHandler.SaveMappedKey("moveRight", moveRight);

            if(SettingsHandler.IsKeyMapped("jump"))
                jump = SettingsHandler.GetMappedKey("jump");
            else
                SettingsHandler.SaveMappedKey("jump", jump);

            if(SettingsHandler.IsKeyMapped("mineKey"))
                mineKey = SettingsHandler.GetMappedKey("mineKey");
            else
                SettingsHandler.SaveMappedKey("mineKey", mineKey);
        }
    }
}
