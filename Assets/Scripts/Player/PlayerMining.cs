using System.Collections;
using Explorer.Input;
using UnityEngine;
using UnityEngine.Tilemaps;
using Explorer.WorldGeneration;
using Unity.RemoteConfig;

// ReSharper disable RedundantTypeArgumentsOfMethod
// ReSharper disable RedundantLambdaParameterType
namespace Explorer.Player {
    public class PlayerMining : MonoBehaviour {
        private struct UserAttributes { }
        private struct AppAttributes { }
        
        public Tilemap mineMap;
        private InputHandler inputHandler;
        private PlayerData playerData;
        [SerializeField]
        private TerrainGen terrainGen;
        private float mineTime;
        private float mineTimeUpgradeModifier = 0.06F;

        private float grassMineTime, stoneMineTime, coalOreMineTime, ironOreMineTime, copperOreMineTime, silverOreMineTime, goldOreMineTime,
            aluminiumOreMineTime, cobaltOreMineTime, uraniumOreMineTime, diamondOreMineTime, strontiumOreMineTime, rubyOreMineTime, 
            quartzOreMineTime, sapphireOreMineTime, jasperJadeOreMineTime, amberOreMineTime, topazOreMineTime;

        private void Awake() {
            inputHandler = GameObject.FindGameObjectWithTag("InputHandler").GetComponent<InputHandler>();
            playerData = GetComponent<PlayerData>();
            
            ConfigManager.FetchCompleted += (ConfigResponse response) => {
                mineTimeUpgradeModifier = ConfigManager.appConfig.GetFloat("mineTimeUpgradeModifier");
                
                // Get Ore Mine Times
                grassMineTime = ConfigManager.appConfig.GetFloat("grassMineTime", 0.2F);
                stoneMineTime = ConfigManager.appConfig.GetFloat("stoneMineTime", 0.5F);
                coalOreMineTime = ConfigManager.appConfig.GetFloat("coalOreMineTime", 1.15F);
                ironOreMineTime = ConfigManager.appConfig.GetFloat("ironOreMineTime", 1.2F);
                copperOreMineTime = ConfigManager.appConfig.GetFloat("copperOreMineTime", 1.15F);
                silverOreMineTime = ConfigManager.appConfig.GetFloat("silverOreMineTime", 1.05F);
                goldOreMineTime = ConfigManager.appConfig.GetFloat("goldOreMineTime", 1.6F);
                aluminiumOreMineTime = ConfigManager.appConfig.GetFloat("aluminiumOreMineTime", 1.34F);
                cobaltOreMineTime = ConfigManager.appConfig.GetFloat("cobaltOreMineTime", 0.77F);
                uraniumOreMineTime = ConfigManager.appConfig.GetFloat("uraniumOreMineTime", 0.9F);
                diamondOreMineTime = ConfigManager.appConfig.GetFloat("diamondOreMineTime", 1.1F);
                strontiumOreMineTime = ConfigManager.appConfig.GetFloat("strontiumOreMineTime", 1.34F);
                rubyOreMineTime = ConfigManager.appConfig.GetFloat("rubyOreMineTime", 1.0F);
                quartzOreMineTime = ConfigManager.appConfig.GetFloat("quartzOreMineTime", 0.67F);
                sapphireOreMineTime = ConfigManager.appConfig.GetFloat("sapphireOreMineTime", 0.98F);
                jasperJadeOreMineTime = ConfigManager.appConfig.GetFloat("jasperJadeOreMineTime", 1.6F);
                amberOreMineTime = ConfigManager.appConfig.GetFloat("amberOreMineTime", 1.5F);
                topazOreMineTime = ConfigManager.appConfig.GetFloat("topazOreMineTime", 1.34F);
            };
            ConfigManager.FetchConfigs<UserAttributes, AppAttributes>(new UserAttributes(), new AppAttributes());
        }

        private void Update() {
            if(!UnityEngine.Input.GetKeyDown(inputHandler.mineKey) || !CanMine() || mineTime != 0.0F) return;
            StartCoroutine(nameof(Mine));
        }

        private IEnumerator Mine() {
            if(Camera.main != null) {
                Vector3 mousePos = UnityEngine.Input.mousePosition;
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                mineTime = GetTimeToMine(worldPos);
                if(mineTime != 0.0F) {
                    yield return new WaitForSeconds(mineTime);
                    bool shouldMine = MineTile(worldPos);
                    if(shouldMine) mineMap.SetTile(mineMap.WorldToCell(worldPos), null);
                    mineTime = 0.0F;
                } else {
                    yield return new WaitForSeconds(0.0F);
                }
            } else {
                yield return new WaitForSeconds(0.0F);
            }
        }

        private float GetTimeToMine(Vector3 worldPos) {
            float mineSpeed;
            switch(terrainGen.GetTileId(mineMap.WorldToCell(worldPos))) {
                case 1:
                    mineSpeed = grassMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? grassMineTime : grassMineTime * mineSpeed > 0.15F ? mineSpeed : 0.15F;
                case 2:
                    mineSpeed = stoneMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? stoneMineTime : stoneMineTime * mineSpeed > 0.35F ? mineSpeed : 0.35F;
                case 3:
                    mineSpeed = coalOreMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? coalOreMineTime : coalOreMineTime * mineSpeed > 0.5F ? mineSpeed : 0.5F;
                case 4:
                    mineSpeed = ironOreMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? ironOreMineTime : ironOreMineTime * mineSpeed > 0.6F ? mineSpeed : 0.6F;
                case 5:
                    mineSpeed = copperOreMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? copperOreMineTime : copperOreMineTime * mineSpeed > 0.4F ? mineSpeed : 0.4F;
                case 6:
                    mineSpeed = silverOreMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? silverOreMineTime : silverOreMineTime * mineSpeed > 0.4F ? mineSpeed : 0.4F;
                case 7:
                    mineSpeed = goldOreMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? goldOreMineTime : goldOreMineTime * mineSpeed > 0.67F ? mineSpeed : 0.67F;
                case 8:
                    mineSpeed = aluminiumOreMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? aluminiumOreMineTime : aluminiumOreMineTime * mineSpeed > 0.58F ? mineSpeed : 0.58F;
                case 9:
                    mineSpeed = cobaltOreMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? cobaltOreMineTime : cobaltOreMineTime * mineSpeed > 0.32F ? mineSpeed : 0.32F;
                case 10:
                    mineSpeed = uraniumOreMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? uraniumOreMineTime : uraniumOreMineTime * mineSpeed > 0.43F ? mineSpeed : 0.43F;
                case 11:
                    mineSpeed = diamondOreMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? diamondOreMineTime : diamondOreMineTime * mineSpeed > 0.44F ? mineSpeed : 0.44F;
                case 12:
                    mineSpeed = strontiumOreMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? strontiumOreMineTime : strontiumOreMineTime * mineSpeed > 0.67F ? mineSpeed : 0.67F;
                case 13:
                    mineSpeed = rubyOreMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? rubyOreMineTime : rubyOreMineTime * mineSpeed > 0.49F ? mineSpeed : 0.49F;
                case 14:
                    mineSpeed = quartzOreMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? quartzOreMineTime : quartzOreMineTime * mineSpeed > 0.32F ? mineSpeed : 0.32F;
                case 15:
                    mineSpeed = sapphireOreMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? sapphireOreMineTime : sapphireOreMineTime * mineSpeed > 0.412F ? mineSpeed : 0.412F;
                case 16:
                    mineSpeed = jasperJadeOreMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? jasperJadeOreMineTime : jasperJadeOreMineTime * mineSpeed > 0.56F ? mineSpeed : 0.56F;
                case 17:
                    mineSpeed = amberOreMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? amberOreMineTime : amberOreMineTime * mineSpeed > 0.476F ? mineSpeed : 0.467F;
                case 18:
                    mineSpeed = topazOreMineTime - (mineTimeUpgradeModifier * playerData.GetMiningSpeedLevel());
                    return playerData.GetMiningSpeedLevel() != 0 ? topazOreMineTime : topazOreMineTime * mineSpeed > 0.589F ? mineSpeed : 0.589F;
                default: return 0;
            }
        }
        
        private bool MineTile(Vector3 worldPos) {
            switch(terrainGen.GetTileId(mineMap.WorldToCell(worldPos))) {
                case 1:
                    playerData.AddOreToInventory(1, 1);
                    return true;
                case 2:
                    playerData.AddOreToInventory(2, 1);
                    return true;
                case 3:
                    playerData.AddOreToInventory(3, 1);
                    return true;
                case 4:
                    playerData.AddOreToInventory(4, 1);
                    return true;
                case 5:
                    playerData.AddOreToInventory(5, 1);
                    return true;
                case 6:
                    playerData.AddOreToInventory(6, 1);
                    return true;
                case 7:
                    playerData.AddOreToInventory(7, 1);
                    return true;
                case 8:
                    playerData.AddOreToInventory(8, 1);
                    return true;
                case 9:
                    playerData.AddOreToInventory(9, 1);
                    return true;
                case 10:
                    playerData.AddOreToInventory(10, 1);
                    return true;
                case 11:
                    playerData.AddOreToInventory(11, 1);
                    return true;
                case 12:
                    playerData.AddOreToInventory(12, 1);
                    return true;
                case 13:
                    playerData.AddOreToInventory(13, 1);
                    return true;
                case 14:
                    playerData.AddOreToInventory(14, 1);
                    return true;
                case 15:
                    playerData.AddOreToInventory(15, 1);
                    return true;
                case 16:
                    playerData.AddOreToInventory(16, 1);
                    return true;
                case 17:
                    playerData.AddOreToInventory(17, 1);
                    return true;
                case 18:
                    playerData.AddOreToInventory(18, 1);
                    return true;
                default: return false;
            }
        }

        private bool CanMine() {
            return playerData.GetInventoryUsage() < playerData.GetCurrentInventorySize();
        }

        public float GetMineTime() {
            return mineTime;
        }
    }
}
