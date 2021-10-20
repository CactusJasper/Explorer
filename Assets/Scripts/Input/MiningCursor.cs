using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MiningCursor : MonoBehaviour
{
    public Tilemap cursorMap;
    public Tilemap mineMap;
    public Tile cursorTile;
    private Vector3Int prevPos;

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int tilePos = cursorMap.WorldToCell(worldPos);
        
        if(tilePos.y > 0)
        {
            Cursor.visible = true;
            cursorMap.SetTile(prevPos, null);
            prevPos = new Vector3Int(tilePos.x, 0, 0);
        }
        else
        {
            Cursor.visible = false;
            cursorMap.SetTile(prevPos, null);
            cursorMap.SetTile(tilePos, cursorTile);
            prevPos = tilePos;
        }

        if(Input.GetMouseButton(0))
        {
            //Debug.Log($"Mine Tile type {terrainGen.GetTileId(mineMap.WorldToCell(worldPos))}");
            //mineMap.SetTile(mineMap.WorldToCell(worldPos), null);
        }
    }
}
