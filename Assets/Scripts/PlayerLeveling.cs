using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeveling : MonoBehaviour
{
    public int playerTotalExp = 0;
    public int playerLevel = 0;
    public List<int> playerLevelThresholds = new List<int> { 20, 40, 80, 120, 200};
    public Upgrades upgrades;

    public menuController menu;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetEnemyKillExperience(int expAmount)
    {
        playerTotalExp += expAmount;
        if (playerTotalExp >= playerLevelThresholds[playerLevel] && playerLevel < playerLevelThresholds.Count)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        playerLevel += 1;       
        Debug.Log("Levelled up: Level " + playerLevel);
        StartCoroutine(upgrades.GetRandomLevelUpgrades());
        menu.upgrade = true;
        menu.Pause();
    }
}
