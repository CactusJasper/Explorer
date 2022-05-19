using UnityEngine;

namespace Explorer.Player {
    public class PlayerData : MonoBehaviour
    {
        public decimal balance = 0;

        // Upgrades
        [SerializeField]
        private int inventoryCapacityLevel = 0;
        [SerializeField]
        private int miningSpeed = 0;
        [SerializeField]
        private int drillStrength = 0;

        // TODO: Set these at runtime possibly look at the current level as well as using unity remote config
        // Upgrades Max Level
        [SerializeField]
        private int inventoryCapacityMaxLevel = 50;
        [SerializeField]
        private int miningSpeedMaxLevel = 20;
        [SerializeField]
        private int drillStrengthMaxLevel = 10;

        // Player Inventory
        [SerializeField]
        private int startingInventorySize = 50;
        [SerializeField]
        private int currentInventorySize = 50;
        [SerializeField]
        private int inventoryUsage;
        [SerializeField]
        private int grassCount;
        [SerializeField]
        private int stoneCount;
        [SerializeField]
        private int coalOreCount;
        [SerializeField]
        private int ironOreCount;
        [SerializeField]
        private int copperOreCount;
        [SerializeField]
        private int silverOreCount;
        [SerializeField]
        private int goldOreCount;
        [SerializeField]
        private int aluminiumOreCount;
        [SerializeField]
        private int cobaltOreCount;
        [SerializeField]
        private int uraniumOreCount;
        [SerializeField]
        private int diamondOreCount;
        [SerializeField]
        private int strontiumOreCount;
        [SerializeField]
        private int rubyOreCount;
        [SerializeField]
        private int quartzOreCount;
        [SerializeField]
        private int sapphireOreCount;
        [SerializeField]
        private int jasperJadeOreCount;
        [SerializeField]
        private int amberOreCount;
        [SerializeField]
        private int topazOreCount;

        private void Update() {
            int totalUsage = 0;
            totalUsage += grassCount;
            totalUsage += stoneCount;
            totalUsage += coalOreCount;
            totalUsage += ironOreCount;
            totalUsage += copperOreCount;
            totalUsage += silverOreCount;
            totalUsage += goldOreCount;
            totalUsage += aluminiumOreCount;
            totalUsage += cobaltOreCount;
            totalUsage += uraniumOreCount;
            totalUsage += diamondOreCount;
            totalUsage += strontiumOreCount;
            totalUsage += quartzOreCount;
            totalUsage += sapphireOreCount;
            totalUsage += jasperJadeOreCount;
            totalUsage += amberOreCount;
            totalUsage += topazOreCount;
            inventoryUsage = totalUsage;
        }
        
        /// <summary>
        /// Checks if the player can afford the current thing they are trying to buy
        /// </summary>
        /// <param name="cost">The cost of the thing the player is buying</param>
        /// <returns>True if the player can afford the thing they are trying to buy else returns false</returns>
        public bool CanAfford(decimal cost) {
            return balance - cost >= 0;
        }

        /// <summary>
        /// Checks if the player can do an upgrade based on current level and the level they will gain
        /// <para>Valid Id's:</para>
        /// <list type="table">
        ///     <item>
        ///         <term>ID 1</term>
        ///         <description> inventoryUpgradeLevel (Inventory Space)</description>
        ///     </item>
        ///     <item>
        ///         <term>ID 2</term>
        ///         <description> miningSpeed (The Mining Speed of the players drill)</description>
        ///     </item>
        ///     <item>
        ///         <term>ID 3</term>
        ///         <description> drillStrength (The Strength of the players drill)</description>
        ///     </item>
        /// </list>
        /// <para><see cref="CanAfford(decimal)">See CanAfford() for validating the player has enough money to do the upgrade</see></para>
        /// </summary>
        /// <param name="upgradeId">The id for the upgrade you are running the check for</param>
        /// <returns>
        ///     true if the upgrade is within the max level else will be false
        ///     <para>Will always return false if the upgradeId provided is non existent</para>
        /// </returns>
        public bool CanUpgrade(int upgradeId) {
            switch(upgradeId) {
                case 0: return inventoryCapacityLevel < inventoryCapacityMaxLevel;
                case 1: return miningSpeed < miningSpeedMaxLevel;
                case 2: return drillStrength < drillStrengthMaxLevel;
                default: return false;
            }
        }

        /// <summary>
        /// Gets the players current balance
        /// </summary>
        /// <returns>The current players balance as a decimal</returns>
        public decimal GetBalance() {
            return balance;
        }

        /// <summary>
        /// Gets the current level of the players inventory capacity
        /// </summary>
        /// <returns>The players inventory capacity level</returns>
        public int GetInventoryCapacityLevel() {
            return inventoryCapacityLevel;
        }

        /// <summary>
        /// Gets the current mining speed level of the players drill
        /// </summary>
        /// <returns>The players mining speed level</returns>
        public int GetMiningSpeedLevel() {
            return miningSpeed;
        }

        /// <summary>
        /// Gets the current drill strength level of the players drill
        /// </summary>
        /// <returns>The players drill strength which determines what the player can mine</returns>
        public int GetDrillStrength() {
            return drillStrength;
        }

        /// <summary>
        /// Gets the players current inventory size
        /// </summary>
        /// <returns></returns>
        public int GetCurrentInventorySize() {
            return currentInventorySize;
        }

        /// <summary>
        /// Gets the players current inventory usage
        /// </summary>
        /// <returns>Integer of the current inventory usage</returns>
        public int GetInventoryUsage() {
            return inventoryUsage;
        }

        /// <summary>
        /// Gets the players current inventory usage
        /// </summary>
        /// <returns>Integer of the current inventory usage</returns>
        public int GetCurrentOreInInventory(int oreId) {
            switch(oreId) {
                case 1: return grassCount;
                case 2: return stoneCount;
                case 3: return coalOreCount;
                case 4: return ironOreCount;
                case 5: return copperOreCount;
                case 6: return silverOreCount;
                case 7: return goldOreCount;
                case 8: return aluminiumOreCount;
                case 9: return cobaltOreCount;
                case 10: return uraniumOreCount;
                case 11: return diamondOreCount;
                case 12: return strontiumOreCount;
                case 13: return rubyOreCount;
                case 14: return quartzOreCount;
                case 15: return sapphireOreCount;
                case 16: return jasperJadeOreCount;
                case 17: return amberOreCount;
                case 18: return topazOreCount;
                default: return 0;
            }
        }
        
        public void AddOreToInventory(int oreId, int count) {
            if(inventoryUsage + count > currentInventorySize) return;
            switch(oreId) {
                case 1:
                    grassCount += count;
                    inventoryUsage += count;
                    break;
                case 2:
                    stoneCount += count;
                    inventoryUsage += count;
                    break;
                case 3:
                    coalOreCount += count;
                    inventoryUsage += count;
                    break;
                case 4:
                    ironOreCount += count;
                    inventoryUsage += count;
                    break;
                case 5:
                    copperOreCount += count;
                    inventoryUsage += count;
                    break;
                case 6:
                    silverOreCount += count;
                    inventoryUsage += count;
                    break;
                case 7:
                    goldOreCount += count;
                    inventoryUsage += count;
                    break;
                case 8:
                    aluminiumOreCount += count;
                    inventoryUsage += count;
                    break;
                case 9:
                    cobaltOreCount += count;
                    inventoryUsage += count;
                    break;
                case 10:
                    uraniumOreCount += count;
                    inventoryUsage += count;
                    break;
                case 11:
                    diamondOreCount += count;
                    inventoryUsage += count;
                    break;
                case 12:
                    strontiumOreCount += count;
                    inventoryUsage += count;
                    break;
                case 13:
                    rubyOreCount += count;
                    inventoryUsage += count;
                    break;
                case 14:
                    quartzOreCount += count;
                    inventoryUsage += count;
                    break;
                case 15:
                    sapphireOreCount += count;
                    inventoryUsage += count;
                    break;
                case 16:
                    jasperJadeOreCount += count;
                    inventoryUsage += count;
                    break;
                case 17:
                    amberOreCount += count;
                    inventoryUsage += count;
                    break;
                case 18:
                    topazOreCount += count;
                    inventoryUsage += count;
                    break;
            }
        }

        ///  <summary>
        /// Increases the players upgrade by 1 level based on the passed in upgradeId
        /// <para>Valid Id's:</para>
        /// <list type="table">
        ///     <item>
        ///         <term>ID 1</term>
        ///         <description> inventoryUpgradeLevel (Inventory Space)</description>
        ///     </item>
        ///     <item>
        ///         <term>ID 2</term>
        ///         <description> miningSpeed (The Mining Speed of the players drill)</description>
        ///     </item>
        ///     <item>
        ///         <term>ID 3</term>
        ///         <description> drillStrength (The Strength of the players drill)</description>
        ///     </item>
        /// </list>
        /// <b>(doesn't validate if the upgrade will exceeded the upgrades max level <see cref="CanUpgrade(int)">see here for validating if they can</see>)</b>
        /// </summary>
        /// <param name="upgradeId">The id for the upgrade you are leveling up</param>
        public void DoUpgrade(int upgradeId) {
            switch(upgradeId) {
                case 0:
                    inventoryCapacityLevel++;
                    currentInventorySize = startingInventorySize * inventoryCapacityLevel;
                    return;
                case 1:
                    miningSpeed++;
                    return;
                case 2:
                    drillStrength++;
                    return;
                default:
                    return;
            }
        }

        /// <summary>
        /// Increases the players upgrade by the passed in amount based on the passed in upgradeId 
        /// <para>Valid Id's:</para>
        /// <list type="table">
        ///     <item>
        ///         <term>ID 1</term>
        ///         <description> inventoryUpgradeLevel (Inventory Space)</description>
        ///     </item>
        ///     <item>
        ///         <term>ID 2</term>
        ///         <description> miningSpeed (The Mining Speed of the players drill)</description>
        ///     </item>
        ///     <item>
        ///         <term>ID 3</term>
        ///         <description> drillStrength (The Strength of the players drill)</description>
        ///     </item>
        /// </list>
        /// <b>(doesn't validate if the amount exceeds the upgrades max level <see cref="CanUpgrade(int)">see here for validating if they can</see>)</b>
        /// </summary>
        /// <param name="upgradeId">The id for the upgrade you are leveling up</param>
        /// <param name="amount">The amount of levels you want to upgrade the selected upgrade</param>
        public void DoUpgrade(int upgradeId, int amount) {
            switch (upgradeId) {
                case 0:
                    inventoryCapacityLevel += amount;
                    return;
                case 1:
                    miningSpeed++;
                    return;
                case 2:
                    drillStrength++;
                    return;
                default:
                    return;
            }
        }
    }
}
