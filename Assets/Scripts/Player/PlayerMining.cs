using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMining : MonoBehaviour
{
    public Tilemap mineMap;
    public LayerMask mineLayer;
    private InputHandler inputHandler;

    private void Awake()
    {
        inputHandler = GameObject.FindGameObjectWithTag("InputHandler").GetComponent<InputHandler>();
    }

    private void Update()
    {
        // TODO: Implment the actual time based ming this can then be hooked into remote config so that we can update the times for each tile (Helps with balance post release)
        if(Input.GetKeyDown(inputHandler.mineKey))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            mineMap.SetTile(mineMap.WorldToCell(worldPos), null);
        }
    }
}
