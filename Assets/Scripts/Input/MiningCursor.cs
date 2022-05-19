using UnityEngine;
using UnityEngine.Tilemaps;

namespace Explorer.Input {
    public class MiningCursor : MonoBehaviour {
        public Tilemap cursorMap;
        public Tile cursorTile;
        private Vector3Int prevPos;

        private void Update() {
            Vector3 mousePos = UnityEngine.Input.mousePosition;
            if(Camera.main == null) return;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3Int tilePos = cursorMap.WorldToCell(worldPos);
        
            if(tilePos.y > 0) {
                Cursor.visible = true;
                cursorMap.SetTile(prevPos, null);
                prevPos = new Vector3Int(tilePos.x, 0, 0);
            } else {
                Cursor.visible = false;
                cursorMap.SetTile(prevPos, null);
                cursorMap.SetTile(tilePos, cursorTile);
                prevPos = tilePos;
            }
        }
    }
}
