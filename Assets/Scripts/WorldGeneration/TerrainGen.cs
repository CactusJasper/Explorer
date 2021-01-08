using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGen : MonoBehaviour
{
    public Tilemap miningMap;

    public Tile stoneBarrierTile;
    public Tile tier1StoneBarrierTile;

    public Tile grassTile;
    public Tile stoneTile;
    public Tile coalOreTile;
    public Tile ironOreTile;
    public Tile copperOreTile;
    public Tile silverOreTile;

    private int width = 32 * 8;
    private int height = 1200;
    private int[,] mineMap;

    private void Awake()
    {
        mineMap = GenerateArray(true);
        GenerateMineLevel(mineMap, miningMap);
    }

    private void GenerateMineLevel(int[,] map, Tilemap tilemap)
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if(map[x, y] == 1)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), grassTile);
                }
                else if(map[x, y] == 2)
                {
                    tilemap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if(map[x, y] == 3)
                {
                    tilemap.SetTile(new Vector3Int(x, -y, 0), coalOreTile);
                }
                else if(map[x, y] == 4)
                {
                    tilemap.SetTile(new Vector3Int(x, -y, 0), ironOreTile);
                }
                else if(map[x, y] == 501)
                {
                    tilemap.SetTile(new Vector3Int(x, -y, 0), stoneBarrierTile);
                }
                else if(map[x, y] == 502)
                {
                    tilemap.SetTile(new Vector3Int(x, -y, 0), tier1StoneBarrierTile);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
            }
        }
    }

    private int[,] GenerateArray(bool mine)
    {
        int[,] map = new int[width, height];

        if(mine)
        {
            /*
             * 0 = No Tile
             * 1 = Grass Tile
             * 2 = Stone Tile
             * 3 = Coal Ore Tile
             * 4 = Iron Ore Tile
             * 5 = Copper Ore Tile
             * 6 = Silver Ore Tile
             * 7 = Gold Ore Tile
             * 8 = Aluminium Ore Tile
             * 9 = Cobalt Ore Tile
             * 10 = Uranium Ore Tile
             * 11 = Diamond Ore Tile
             * 12 = Strontium Ore Tile
             * 13 = Ruby Ore Tile
             * 14 = Quartz Ore Tile
             * 15 = Sapphire Ore Tile
             * 16 = Jasper Jade Ore Tile
             * 17 = Amber Ore Tile
             * 18 = Topaz Ore Tile
             * ======== Dense Tiles ==========
             * 100 = Dense Coal Ore Tile
             * 101 = Dense Gold Ore Tile
             * 102 = Dense Cobalt Ore Tile
             * 103 = Dense Uranium Ore Tile
             * 104 = Dense Diamond Ore Tile
             * 105 = Dense Strontium Ore Tile
             * ======== Barrier Tiles ========
             * 500 = Air Barrier Tile
             * 501 = Stone Barrier Tile
             * 502 = Stone Barrier Tier 1 Tile
             * 503 = Stone Barrier Tier 2 Tile
             * 504 = Stone Barrier Tier 3 Tile
             * 505 = Stone Barrier Tier 4 Tile
             * 506 = Stone Barrier Tier 5 Tile
             * 507 = Stone Barrier Tier 6 Tile
             * 508 = Stone Barrier Tier 7 Tile
             * 509 = Stone Barrier Tier 8 Tile
             * 510 = Stone Barrier Tier 9 Tile
             */
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    if(x == map.GetLowerBound(0))
                    {
                        map[x, y] = 501; // Stone Barrier
                    }
                    else if (x == map.GetUpperBound(0))
                    {
                        map[x, y] = 501; // Stone Barrier
                    }
                    else if(y == map.GetUpperBound(1))
                    {
                        map[x, y] = 501; // Stone Barrier
                    }
                    else if(y == 0)
                    {
                        map[x, y] = 1; // Grass Tile
                    }
                    else if(y >= 4 && y <= 50)
                    {
                        if(Random.Range(0, 100) > 75)
                        {
                            map[x, y] = 3; // Coal Ore Tile
                        }
                        else
                        {
                            if(y > 30)
                            {
                                if(Random.Range(0, 100) > 95)
                                {
                                    map[x, y] = 4; // Iron Ore Tile
                                }
                                else
                                {
                                    map[x, y] = 2; // Stone Tile
                                }
                            }
                            else
                            {
                                map[x, y] = 2; // Stone Tile
                            }
                        }
                    }
                    else if(y == 51 || y == 52 || y == 53)
                    {
                        map[x, y] = 502; // Stone Barrier Tier 1 Tile
                    }
                    else
                    {
                        map[x, y] = 2; // Stone Tile
                    }
                }
            }
        }
        else
        {
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    map[x, y] = 0;
                }
            }
        }

        return map;
    }
}
