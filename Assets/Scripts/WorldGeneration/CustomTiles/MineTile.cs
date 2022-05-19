using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Explorer.WorldGeneration.CustomTiles {
    public class MineTile : Tile {
        [SerializeField]
        private int tileId = 0;
        [SerializeField]
        private int requiredDrillStrength = 0;

        public void SetTileId(int id) {
            tileId = id;
        }

        public void SetRequiredDrillStrength(int strength) {
            requiredDrillStrength = strength;
        }

        public int GetTileId() {
            return tileId;
        }

        public int GetRequiredDrillStrength() {
            return requiredDrillStrength;
        }

        #if UNITY_EDITOR
        [MenuItem("Assets/Create/Custom Tiles/Mine Tile")]
        public static void CreateAnimatedTile() {
            string path = EditorUtility.SaveFilePanelInProject("Save Mine Tile", "New Mine Tile", "asset", "Save Mine Tile", "Assets/Tiles");
            if (path == "")
                return;

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<MineTile>(), path);
        }
        #endif
    }
}
