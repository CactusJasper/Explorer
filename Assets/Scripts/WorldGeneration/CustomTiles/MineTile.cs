using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MineTile : Tile
{
    [SerializeField]
    private int tileId = 0;

    public void SetTileId(int id)
    {
        tileId = id;
    }

    public int GetTileId()
    {
        return tileId;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Custom Tiles/Mine Tile")]
    public static void CreateAnimatedTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Mine Tile", "New Mine Tile", "asset", "Save Mine Tile", "Assets/Tiles");
        if (path == "")
            return;

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<MineTile>(), path);
    }
#endif
}
