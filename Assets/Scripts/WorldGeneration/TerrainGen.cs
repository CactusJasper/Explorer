using System.Collections;
using System.Diagnostics;
using Explorer.WorldGeneration.CustomTiles;
using UnityEngine;
using UnityEngine.Tilemaps;
using Debug = UnityEngine.Debug;

namespace Explorer.WorldGeneration {
    public class TerrainGen : MonoBehaviour {
        [SerializeField]
        private GameObject player;
        public Tilemap miningMap;
        public Tilemap mineBgMap;
        public Tilemap upperMap;

        #region Tiles
        public MineTile airBarrierTile;
        public MineTile stoneBarrierTile;
        public MineTile tier1StoneBarrierTile;
        public MineTile tier2StoneBarrierTile;
        public MineTile tier3StoneBarrierTile;
        public MineTile tier4StoneBarrierTile;
        public MineTile tier5StoneBarrierTile;
        public MineTile tier6StoneBarrierTile;
        public MineTile tier7StoneBarrierTile;
        public MineTile tier8StoneBarrierTile;
        public MineTile tier9StoneBarrierTile;

        public MineTile grassTile;
        public MineTile stoneTile;
        public MineTile coalOreTile;
        public MineTile ironOreTile;
        public MineTile copperOreTile;
        public MineTile silverOreTile;
        public MineTile goldOreTile;
        public MineTile aluminiumOreTile;
        public MineTile cobaltOreTile;
        public MineTile uraniumOreTile;
        public MineTile diamondOreTile;
        public MineTile strontiumOreTile;
        public MineTile rubyOreTile;
        public MineTile quartzOreTile;
        public MineTile sapphireOreTile;
        public MineTile jasperJadeOreTile;
        public MineTile amberOreTile;
        public MineTile topazOreTile;
        #endregion

        private const int width = 70;
        private const int height = 1200;
        private int[,] mineMap;

        private void Awake() {
            StartCoroutine(nameof(InitialLoadWorld));
        }

        private IEnumerator InitialLoadWorld() {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            mineMap = GenerateMapArray();
            stopwatch.Stop();
            Debug.Log($"Map Array Generation took {stopwatch.ElapsedMilliseconds}ms");
            stopwatch.Reset();

            stopwatch.Start();
            GenerateMineLevel(miningMap, mineBgMap);
            stopwatch.Stop();
            Debug.Log($"Map Generation took {stopwatch.ElapsedMilliseconds}ms");
            stopwatch.Reset();

            stopwatch.Start();
            GenerateUpperMap();
            stopwatch.Stop();
            Debug.Log($"Upper Map Generation took {stopwatch.ElapsedMilliseconds}ms");
            stopwatch.Reset();

            player.SetActive(true);

            yield return new WaitForSecondsRealtime(0.0F);
        }

        public int GetTileId(Vector3Int position) {
            if(!(miningMap.GetTile(position) is MineTile)) return 0;
            MineTile tile = (MineTile)miningMap.GetTile(position);
            return tile.GetTileId();
        }

        public int GetTileDrillStrengthRequired(Vector3Int position) {
            if(!(miningMap.GetTile(position) is MineTile)) return 0;
            MineTile tile = (MineTile)miningMap.GetTile(position);
            return tile.GetRequiredDrillStrength();
        }

        private void GenerateUpperMap() {
            int[,] map = new int[width, height];

            for(int x = 0; x < width; x++) {
                for(int y = 0; y <= 50; y++) {
                    if(x == map.GetLowerBound(0)) upperMap.SetTile(new Vector3Int(x, y, 0), airBarrierTile);
                    else if(x == map.GetUpperBound(0)) upperMap.SetTile(new Vector3Int(x, y, 0), airBarrierTile);
                    else if(y == 50) upperMap.SetTile(new Vector3Int(x, y, 0), airBarrierTile);
                }
            }
        }
        
        private void GenerateMineLevel(Tilemap tilemap, Tilemap bgMap) {
            tilemap.ClearAllTiles();
            bgMap.ClearAllTiles();
            Vector3Int[,] positions = new Vector3Int[width, height];
            Vector3Int[,] bgPositions = new Vector3Int[width, height];
            Tile[,] tiles = new Tile[width, height];
            Tile[,] bgTiles = new Tile[width, height];

            for(int x = 0; x < width; x++) {
                for(int y = 0; y < height; y++) {
                    switch(mineMap[x, y]) {
                        case TileIds.GRASS:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = bgTiles[x, y] = grassTile;
                            break;
                        case TileIds.STONE:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = bgTiles[x, y] = stoneTile;
                            break;
                        case TileIds.COAL_ORE:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = coalOreTile;
                            bgTiles[x, y] = stoneTile;
                            break;
                        case TileIds.IRON_ORE:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = ironOreTile;
                            bgTiles[x, y] = stoneTile;
                            break;
                        case TileIds.COPPER_ORE:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = copperOreTile;
                            bgTiles[x, y] = stoneTile;
                            break;
                        case TileIds.SILVER_ORE:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = silverOreTile;
                            bgTiles[x, y] = stoneTile;
                            break;
                        case TileIds.GOLD_ORE:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = goldOreTile;
                            bgTiles[x, y] = stoneTile;
                            break;
                        case TileIds.ALUMINIUM_ORE:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = aluminiumOreTile;
                            bgTiles[x, y] = stoneTile;
                            break;
                        case TileIds.COBALT_ORE:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = cobaltOreTile;
                            bgTiles[x, y] = stoneTile;
                            break;
                        case TileIds.URANIUM_ORE:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = uraniumOreTile;
                            bgTiles[x, y] = stoneTile;
                            break;
                        case TileIds.DIAMOND_ORE:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = diamondOreTile;
                            bgTiles[x, y] = stoneTile;
                            break;
                        case TileIds.STRONTIUM_ORE:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = strontiumOreTile;
                            bgTiles[x, y] = stoneTile;
                            break;
                        case TileIds.RUBY_ORE:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = rubyOreTile;
                            bgTiles[x, y] = stoneTile;
                            break;
                        case TileIds.QUARTZ_ORE:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = quartzOreTile;
                            bgTiles[x, y] = stoneTile;
                            break;
                        case TileIds.SAPPHIRE_ORE:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = sapphireOreTile;
                            bgTiles[x, y] = stoneTile;
                            break;
                        case TileIds.JASPER_JADE_ORE:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = jasperJadeOreTile;
                            bgTiles[x, y] = stoneTile;
                            break;
                        case TileIds.AMBER_ORE:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = amberOreTile;
                            bgTiles[x, y] = stoneTile;
                            break;
                        case TileIds.TOPAZ_ORE:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = topazOreTile;
                            bgTiles[x, y] = stoneTile;
                            break;
                        case TileIds.STONE_BARRIER:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = bgTiles[x, y] = stoneBarrierTile;
                            break;
                        case TileIds.STONE_BARRIER_TIER_1:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = bgTiles[x, y] = tier1StoneBarrierTile;
                            break;
                        case TileIds.STONE_BARRIER_TIER_2:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = bgTiles[x, y] = tier2StoneBarrierTile;
                            break;
                        case TileIds.STONE_BARRIER_TIER_3:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = bgTiles[x, y] = tier3StoneBarrierTile;
                            break;
                        case TileIds.STONE_BARRIER_TIER_4:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = bgTiles[x, y] = tier4StoneBarrierTile;
                            break;
                        case TileIds.STONE_BARRIER_TIER_5:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = bgTiles[x, y] = tier5StoneBarrierTile;
                            break;
                        case TileIds.STONE_BARRIER_TIER_6:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = bgTiles[x, y] = tier6StoneBarrierTile;
                            break;
                        case TileIds.STONE_BARRIER_TIER_7:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = bgTiles[x, y] = tier7StoneBarrierTile;
                            break;
                        case TileIds.STONE_BARRIER_TIER_8:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = bgTiles[x, y] = tier8StoneBarrierTile;
                            break;
                        case TileIds.STONE_BARRIER_TIER_9:
                            positions[x, y] = bgPositions[x, y] = new Vector3Int(x, -y, 0);
                            tiles[x, y] = bgTiles[x, y] = tier9StoneBarrierTile;
                            break;
                    }
                }
            }

            tilemap.SetTiles(Vector3IntToArray(positions), TileToArray(tiles));
            bgMap.SetTiles(Vector3IntToArray(bgPositions), TileToArray(bgTiles));
        }
    
        // TODO: Add rest of ores when able too
        private int[,] GenerateMapArray()
        {
            mineMap = new int[width, height];

            for(int x = 0; x < width; x++) {
                for(int y = 0; y < height; y++) {
                    // Generate Outer Barriers
                    if(x == mineMap.GetLowerBound(0)) {
                        mineMap[x, y] = TileIds.STONE_BARRIER;
                        continue;
                    }
                    if(x == mineMap.GetUpperBound(0)) {
                        mineMap[x, y] = TileIds.STONE_BARRIER;
                        continue;
                    }

                    if(y == mineMap.GetUpperBound(1)) {
                        mineMap[x, y] = TileIds.STONE_BARRIER;
                        continue;
                    }
                    
                    // Generate Grass & Level Barriers
                    switch(y) {
                        case 0:
                            mineMap[x, y] = TileIds.GRASS;
                            continue;
                        case 52:
                        case 53:
                        case 54:
                            mineMap[x, y] = TileIds.STONE_BARRIER_TIER_1;
                            continue;
                        case 113:
                        case 114:
                            mineMap[x, y] = TileIds.STONE_BARRIER_TIER_2;
                            continue;
                        case 112:
                        case 115:
                            if(Random.Range(0, 100) > 50) mineMap[x, y] = TileIds.STONE_BARRIER_TIER_2;
                            else mineMap[x, y] = TileIds.STONE;
                            continue;
                        case 177:
                        case 178:
                        case 179:
                            mineMap[x, y] = TileIds.STONE_BARRIER_TIER_3;
                            continue;
                        case 176:
                        case 180:
                            if(Random.Range(0, 100) > 45) mineMap[x, y] = TileIds.STONE_BARRIER_TIER_3;
                            else mineMap[x, y] = TileIds.STONE;
                            continue;
                        case 282:
                        case 283:
                        case 284:
                            mineMap[x, y] = TileIds.STONE_BARRIER_TIER_4;
                            continue;
                        case 281:
                        case 285:
                            if(Random.Range(0, 100) > 35) mineMap[x, y] = TileIds.STONE_BARRIER_TIER_4;
                            else mineMap[x, y] = TileIds.STONE;
                            continue;
                    }

                    // Generate Ores
                    if(y >= 4 && y <= 50) {
                        // Generates Iron/Coal Ore and Stone
                        mineMap[x, y] = Random.Range(0, 100) > 75 ? TileIds.COAL_ORE : y > 30 && Random.Range(0, 100) > 95 ? TileIds.IRON_ORE : TileIds.STONE;
                        continue;
                    } 
                    
                    if(y > 55 && y <= 110) {
                        if(Random.Range(0, 100) > 70) {
                            // Generates Iron/Coal Ore and Stone
                            mineMap[x, y] = Random.Range(0, 100) > 80 ? TileIds.IRON_ORE : y < 75 ? TileIds.COAL_ORE : Random.Range(0, 100) > 60 ? TileIds.IRON_ORE : TileIds.STONE;
                            continue;
                        }

                        if(y > 75) {
                            if(y > 95) {
                                // Generates Silver/Copper Ore and Stone
                                mineMap[x, y] = Random.Range(0, 100) > 50 ? Random.Range(0, 100) > 85 ? TileIds.COPPER_ORE : TileIds.STONE : Random.Range(0, 100) > 93 ? TileIds.SILVER_ORE : TileIds.STONE;
                                continue;
                            }
                            
                            // Generates Copper Ore and Stone
                            mineMap[x, y] = Random.Range(0, 100) > 85 ? TileIds.COPPER_ORE : TileIds.STONE;
                            continue;
                        }
                    }

                    if(y > 116 && y < 175) {
                        // Generates Silver/Gold/Iron/Copper Ore and Stone
                        mineMap[x, y] = Random.Range(0, 100) > 93 ? TileIds.SILVER_ORE : y > 150 ? Random.Range(0, 100) > 97 ? TileIds.GOLD_ORE :
                            TileIds.STONE : Random.Range(0, 100) > 95 ? Random.Range(0, 100) > 50 ? TileIds.IRON_ORE : TileIds.COPPER_ORE : TileIds.STONE;
                        continue;
                    }

                    if(y > 181 && y < 280) {
                        if(Random.Range(0, 100) > 75) {
                            // Generates Aluminium/Gold/Silver Ore and Stone
                            mineMap[x, y] = y < 240 ? Random.Range(0, 100) > 83 ? TileIds.GOLD_ORE : Random.Range(0, 100) > 81 ? TileIds.SILVER_ORE : TileIds.STONE :
                                Random.Range(0, 100) > 90 ? TileIds.GOLD_ORE : Random.Range(0, 100) > 95 ? TileIds.ALUMINIUM_ORE : TileIds.STONE;
                            continue;
                        }
                        
                        // Generates Aluminium Ore and Stone
                        mineMap[x, y] = y > 240 ? Random.Range(0, 100) > 94 ? 8 : TileIds.STONE : TileIds.STONE;
                        continue;
                    }

                    mineMap[x, y] = TileIds.STONE;
                }
            }

            return mineMap;
        }

        // Convert a 2D Vector3Int array into a single 1D Vector3Int array for using SetTiles
        private static Vector3Int[] Vector3IntToArray(Vector3Int[,] input) {
            int size = input.Length;
            Vector3Int[] result = new Vector3Int[size];

            int write = 0;
            for(int i = 0; i <= input.GetUpperBound(0); i++) {
                for(int z = 0; z <= input.GetUpperBound(1); z++) {
                    result[write++] = input[i, z];
                }
            }
        
            return result;
        }

        // Convert a 2D Tile array into a single 1D Tile array for using SetTiles
        private static TileBase[] TileToArray(Tile[,] input) {
            int size = input.Length;
            TileBase[] result = new TileBase[size];

            int write = 0;
            for(int i = 0; i <= input.GetUpperBound(0); i++) {
                for(int z = 0; z <= input.GetUpperBound(1); z++) {
                    result[write++] = input[i, z];
                }
            }
        
            return result;
        }
    }
}
