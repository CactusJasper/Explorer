using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGen : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    public Tilemap miningMap;
    public Tilemap mineBgMap;
    public Tilemap upperMap;

    public Tile airBarrierTile;
    public Tile stoneBarrierTile;
    public Tile tier1StoneBarrierTile;
    public Tile tier2StoneBarrierTile;
    public Tile tier3StoneBarrierTile;
    public Tile tier4StoneBarrierTile;
    public Tile tier5StoneBarrierTile;
    public Tile tier6StoneBarrierTile;
    public Tile tier7StoneBarrierTile;
    public Tile tier8StoneBarrierTile;
    public Tile tier9StoneBarrierTile;

    public MineTile grassTile;
    public MineTile stoneTile;
    public MineTile coalOreTile;
    public MineTile ironOreTile;
    public MineTile copperOreTile;
    public MineTile silverOreTile;
    public MineTile goldOreTile;
    public Tile aluminiumOreTile;
    public Tile cobaltOreTile;
    public Tile uraniumOreTile;
    public Tile diamondOreTile;
    public Tile strontiumOreTile;
    public Tile rubyOreTile;
    public Tile quartzOreTile;
    public Tile sapphireOreTile;
    public Tile jasperJadeOreTile;
    public Tile amberOreTile;
    public Tile topazOreTile;

    private int width = 70;//64 * 2;
    private int height = 1200;
    private int[,] mineMap;

    private void Awake()
    {
        StartCoroutine("InitalLoadWorld");
    }

    private IEnumerator InitalLoadWorld()
    {
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        mineMap = GenerateArray(true);
        stopwatch.Stop();
        UnityEngine.Debug.Log($"Map Array Generation took {stopwatch.ElapsedMilliseconds}ms");

        stopwatch.Reset();

        stopwatch.Start();
        GenerateMineLevel(mineMap, miningMap, mineBgMap);
        stopwatch.Stop();
        UnityEngine.Debug.Log($"Map Generation took {stopwatch.ElapsedMilliseconds}ms");
        stopwatch.Reset();

        stopwatch.Start();
        GenerateUpperMap();
        stopwatch.Stop();
        UnityEngine.Debug.Log($"Upper Map Generation took {stopwatch.ElapsedMilliseconds}ms");
        stopwatch.Reset();

        //yield return new WaitForEndOfFrame();
        //stopwatch.Start();
        //miningMap.gameObject.GetComponent<CompositeCollider2D>().GenerateGeometry();
        //stopwatch.Stop();
        //yield return new WaitForEndOfFrame();

        player.SetActive(true);

        //UnityEngine.Debug.Log($"Colider Generation took {stopwatch.ElapsedMilliseconds}ms");
        yield return new WaitForSecondsRealtime(0.01F);

        throw new System.Exception("Update to new MineTile Asset to allow for the ming system to take advantage of tile type id's");
    }

    public int GetTileId(Vector3Int position)
    {
        if(typeof(MineTile).IsInstanceOfType(miningMap.GetTile(position)))
        {
            MineTile tile = (MineTile)miningMap.GetTile(position);
            return tile.GetTileId();
        }
        else
        {
            return 0;
        }
    }

    private void GenerateUpperMap()
    {
        int[,] map = new int[width, height];

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y <= 50; y++)
            {
                if(x == map.GetLowerBound(0))
                {
                    upperMap.SetTile(new Vector3Int(x, y, 0), airBarrierTile);
                }
                else if(x == map.GetUpperBound(0))
                {
                    upperMap.SetTile(new Vector3Int(x, y, 0), airBarrierTile);
                }
                else if(y == 50)
                {
                    upperMap.SetTile(new Vector3Int(x, y, 0), airBarrierTile);
                }
            }
        }
    }

    private void GenerateMineLevel(int[,] map, Tilemap tilemap, Tilemap bgmap)
    {
        tilemap.ClearAllTiles();
        bgmap.ClearAllTiles();
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
        Vector3Int[,] positions = new Vector3Int[width, height];
        Vector3Int[,] bgPositions = new Vector3Int[width, height];
        Tile[,] tiles = new Tile[width, height];
        Tile[,] bgTiles = new Tile[width, height];

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if(map[x, y] == 1)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = bgTiles[x, y] = grassTile;
                    //tilemap.SetTile(new Vector3Int(x, y, 0), grassTile);
                    //bgmap.SetTile(new Vector3Int(x, y, 0), grassTile);
                }
                else if(map[x, y] == 2)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if(map[x, y] == 3)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = coalOreTile;
                    bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), coalOreTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if(map[x, y] == 4)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = ironOreTile;
                    bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), ironOreTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if(map[x, y] == 5)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = copperOreTile;
                    bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), copperOreTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if(map[x, y] == 6)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = silverOreTile;
                    bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), silverOreTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if(map[x, y] == 7)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = goldOreTile;
                    bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), goldOreTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if (map[x, y] == 8)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = aluminiumOreTile;
                    bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), aluminiumOreTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if (map[x, y] == 9)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = cobaltOreTile;
                    bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), cobaltOreTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if (map[x, y] == 10)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = uraniumOreTile;
                    bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), uraniumOreTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if (map[x, y] == 11)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = diamondOreTile;
                    bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), diamondOreTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if (map[x, y] == 12)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = strontiumOreTile;
                    bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), strontiumOreTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if (map[x, y] == 13)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = rubyOreTile;
                    bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), rubyOreTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if (map[x, y] == 14)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = quartzOreTile;
                    bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), quartzOreTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if (map[x, y] == 15)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = sapphireOreTile;
                    bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), sapphireOreTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if (map[x, y] == 16)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = jasperJadeOreTile;
                    bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), jasperJadeOreTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if (map[x, y] == 17)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = amberOreTile;
                    bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), amberOreTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if (map[x, y] == 18)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = topazOreTile;
                    bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), topazOreTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
                else if(map[x, y] == 501)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = bgTiles[x, y] = stoneBarrierTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), stoneBarrierTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneBarrierTile);
                }
                else if(map[x, y] == 502)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = bgTiles[x, y] = tier1StoneBarrierTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), tier1StoneBarrierTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), tier1StoneBarrierTile);
                }
                else if(map[x, y] == 503)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = bgTiles[x, y] = tier2StoneBarrierTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), tier2StoneBarrierTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), tier2StoneBarrierTile);
                }
                else if (map[x, y] == 504)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = bgTiles[x, y] = tier3StoneBarrierTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), tier3StoneBarrierTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), tier3StoneBarrierTile);
                }
                else if (map[x, y] == 505)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = bgTiles[x, y] = tier4StoneBarrierTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), tier4StoneBarrierTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), tier4StoneBarrierTile);
                }
                else if (map[x, y] == 506)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = bgTiles[x, y] = tier5StoneBarrierTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), tier5StoneBarrierTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), tier5StoneBarrierTile);
                }
                else if (map[x, y] == 507)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = bgTiles[x, y] = tier6StoneBarrierTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), tier6StoneBarrierTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), tier6StoneBarrierTile);
                }
                else if (map[x, y] == 508)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = bgTiles[x, y] = tier7StoneBarrierTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), tier7StoneBarrierTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), tier7StoneBarrierTile);
                }
                else if (map[x, y] == 509)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = bgTiles[x, y] = tier8StoneBarrierTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), tier8StoneBarrierTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), tier8StoneBarrierTile);
                }
                else if (map[x, y] == 510)
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = bgTiles[x, y] = tier9StoneBarrierTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), tier9StoneBarrierTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), tier9StoneBarrierTile);
                }
                else
                {
                    positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                    tiles[x, y] = bgTiles[x, y] = stoneTile;
                    //tilemap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                    //bgmap.SetTile(new Vector3Int(x, -y, 0), stoneTile);
                }
            }
        }

        tilemap.SetTiles(Vector3IntToArray(positions), TileToArray(tiles));
        bgmap.SetTiles(Vector3IntToArray(bgPositions), TileToArray(bgTiles));
    }
    // TODO: Overhaul all the generation sizes(Map height is too large to have good load times) and add the rest of the ores
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
                    else if(x == map.GetUpperBound(0))
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
                    else if(y == 52 || y == 53 || y == 54)
                    {
                        map[x, y] = 502; // Stone Barrier Tier 1 Tile
                    }
                    else if(y > 55 && y <= 110)
                    {
                        if(Random.Range(0, 100) > 70)
                        {
                            if(Random.Range(0, 100) > 80)
                            {
                                map[x, y] = 4; // Iron Ore Tile
                            }
                            else
                            {
                                if(y < 75)
                                {
                                    map[x, y] = 3; // Coal Ore Tile
                                }
                                else
                                {
                                    if(Random.Range(0, 100) > 60)
                                    {
                                        map[x, y] = 4; // Iron Ore Tile
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
                            if(y > 75)
                            {
                                if(y > 95)
                                { 
                                    if(Random.Range(0, 100) > 50)
                                    {
                                        if(Random.Range(0, 100) > 85)
                                        {
                                            map[x, y] = 5; // Copper Ore Tile
                                        }
                                        else
                                        {
                                            map[x, y] = 2; // Stone Tile
                                        }
                                    }
                                    else
                                    {
                                        if(Random.Range(0, 100) > 93)
                                        {
                                            map[x, y] = 6; // Silver Ore Tile
                                        }
                                        else
                                        {
                                            map[x, y] = 2; // Stone Tile
                                        }
                                    }
                                }
                                else
                                {
                                    if(Random.Range(0, 100) > 85)
                                    {
                                        map[x, y] = 5; // Copper Ore Tile
                                    }
                                    else
                                    {
                                        map[x, y] = 2; // Stone Tile
                                    }
                                }
                            }
                            else
                            {
                                map[x, y] = 2; // Stone Tile
                            }
                        }
                    }
                    else if(y == 112)
                    {
                        if(Random.Range(0, 100) > 50)
                        {
                            map[x, y] = 503; // Tier 2 Stone Barrier Tile
                        }
                        else
                        {
                            map[x, y] = 2; // Stone Tile
                        }
                    }
                    else if(y == 113 || y == 114)
                    {
                        map[x, y] = 503; // Tier 2 Stone Barrier Tile
                    }
                    else if(y == 115)
                    {
                        if (Random.Range(0, 100) > 50)
                        {
                            map[x, y] = 503; // Tier 2 Stone Barrier Tile
                        }
                        else
                        {
                            map[x, y] = 2; // Stone Tile
                        }
                    }
                    else if(y > 116 && y < 175)
                    {
                        if(Random.Range(0, 100) > 93)
                        {
                            map[x, y] = 6; // Silver Ore Tile
                        }
                        else
                        {
                            if(y > 150)
                            { 
                                if(Random.Range(0, 100) > 97)
                                {
                                    map[x, y] = 7; // Gold Ore Tile
                                }
                                else
                                {
                                    map[x, y] = 2; // Stone Tile
                                }
                            }
                            else
                            {
                                if(Random.Range(0, 100) > 95)
                                {
                                    if(Random.Range(0, 100) > 50)
                                    {
                                        map[x, y] = 4; // Iron Ore Tile
                                    }
                                    else
                                    {
                                        map[x, y] = 5; // Copper Ore Tile
                                    }
                                }
                                else
                                {
                                    map[x, y] = 2; // Stone Tile
                                }
                            }
                        }
                    }
                    else if (y == 176)
                    {
                        if (Random.Range(0, 100) > 45)
                        {
                            map[x, y] = 504; // Tier 3 Stone Barrier Tile
                        }
                        else
                        {
                            map[x, y] = 2; // Stone Tile
                        }
                    }
                    else if (y == 177 || y == 178 || y == 179)
                    {
                        map[x, y] = 504; // Tier 3 Stone Barrier Tile
                    }
                    else if (y == 180)
                    {
                        if (Random.Range(0, 100) > 45)
                        {
                            map[x, y] = 504; // Tier 3 Stone Barrier Tile
                        }
                        else
                        {
                            map[x, y] = 2; // Stone Tile
                        }
                    }
                    else if(y > 181 && y < 280)
                    {
                        if(Random.Range(0, 100) > 75)
                        {
                            if(y < 240)
                            {
                                if(Random.Range(0, 100) > 83)
                                {
                                    map[x, y] = 7; // Gold Ore Tile
                                }
                                else
                                {
                                    if(Random.Range(0, 100) > 81)
                                    { 
                                        map[x, y] = 6; // Silver Ore Tile
                                    }
                                    else
                                    {
                                        map[x, y] = 2; // Stone Tile
                                    }
                                }
                            }
                            else
                            { 
                                if(Random.Range(0, 100) > 90)
                                {
                                    map[x, y] = 7; // Gold Ore Tile
                                }
                                else
                                {
                                    if(Random.Range(0, 100) > 95)
                                    { 
                                        map[x, y] = 8; // Aluminium Ore Tile
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
                            if(y > 240)
                            {
                                if(Random.Range(0, 100) > 94)
                                {
                                    map[x, y] = 8; // Aluminium Ore Tile
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
                    else if(y == 281)
                    {
                        if(Random.Range(0, 100) > 35)
                        {
                            map[x, y] = 505; // Tier 4 Stone Barrier
                        }
                        else
                        {
                            map[x, y] = 2; // Stone Tile
                        }
                    }
                    else if(y == 282 || y == 283 || y == 284)
                    {
                        map[x, y] = 505; // Tier 4 Stone Barrier
                    }
                    else if(y == 285)
                    {
                        if (Random.Range(0, 100) > 35)
                        {
                            map[x, y] = 505; // Tier 4 Stone Barrier
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

    // Convert a 2D Vector3Int array into a single 1D Vector3Int array for using SetTiles
    private static Vector3Int[] Vector3IntToArray(Vector3Int[,] input)
    {
        int size = input.Length;
        Vector3Int[] result = new Vector3Int[size];

        int write = 0;
        for (int i = 0; i <= input.GetUpperBound(0); i++)
        {
            for (int z = 0; z <= input.GetUpperBound(1); z++)
            {
                result[write++] = input[i, z];
            }
        }
        
        return result;
    }

    // Convert a 2D Tile array into a single 1D Tile array for using SetTiles
    private static Tile[] TileToArray(Tile[,] input)
    {
        int size = input.Length;
        Tile[] result = new Tile[size];

        int write = 0;
        for (int i = 0; i <= input.GetUpperBound(0); i++)
        {
            for (int z = 0; z <= input.GetUpperBound(1); z++)
            {
                result[write++] = input[i, z];
            }
        }
        
        return result;
    }
}
