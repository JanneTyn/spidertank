using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLeveling : MonoBehaviour
{
    public float playerTotalExp = 0;
    public int playerLevel = 0;
    int playerLevelOldThreshold;
    float playerLevelDifference;
    float playerLevelCurrentExpDifference;
    float expPercent;
    public List<int> playerLevelThresholdsTest = new List<int> { 20, 40, 80, 120, 200, 350, 500, 800, 1200};
    public List<int> playerLevelThresholds = new List<int> { 20, 40, 80, 120, 200, 350, 500, 800, 1200};
    public Upgrades upgrades;

    public menuController menu;

    public Slider expSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(upgrades.GetRandomLevelUpgrades());
            menu.Pause(true);
        } */
    }


    public void GetEnemyKillExperience(int expAmount)
    {
        playerTotalExp += expAmount * PlayerStats.playerXPrate;

        if (playerLevel < playerLevelThresholds.Count)
        {
            int playerLevelTresholdInt = playerLevelThresholds[playerLevel];
            if (playerLevel > 0)
            {
                playerLevelOldThreshold = playerLevelThresholds[playerLevel - 1];
            }
            else
            {
                playerLevelOldThreshold = 0;
            }
            playerLevelDifference = playerLevelTresholdInt - playerLevelOldThreshold;
            playerLevelCurrentExpDifference = playerTotalExp - playerLevelOldThreshold;
            if (playerLevelCurrentExpDifference > 0)
            {
                expPercent = playerLevelCurrentExpDifference / playerLevelDifference * 100;
            }
            else
            {
                expPercent = 0;
            }
            //float playerLevelTresholdFloat = playerLevelTresholdInt;
            //float playerTotalExpFloat = playerTotalExp;
            //expPercent = playerTotalExpFloat / playerLevelTresholdFloat * 100;

            if (playerLevel < playerLevelThresholds.Count)
            {
                if (playerTotalExp >= playerLevelThresholds[playerLevel])
                {
                    LevelUp();
                    expSlider.value = 0;
                }
                else
                {
                    expSlider.value = expPercent;
                }
            }
        }
        
       /* Debug.Log("playertotalexpfloat: " + playerTotalExpFloat);
        Debug.Log("playerlevelthresholdfloat: " + playerLevelTresholdFloat); 
        Debug.Log("exppercent out of 100: " + expPercent);*/
    }

    public void LevelUp()
    {
        playerLevel += 1;       
        Debug.Log("Levelled up: Level " + playerLevel);
        StartCoroutine(DelayBeforeMenu());
    }

    public IEnumerator DelayBeforeMenu()
    {
        float elapsedTime = 0;
        float time = 0.3f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(upgrades.GetRandomLevelUpgrades());
        menu.Pause(true);
    }
}
