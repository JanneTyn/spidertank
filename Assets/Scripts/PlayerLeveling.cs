using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLeveling : MonoBehaviour
{
    public float playerTotalExp = 0;
    public int playerLevel = 0;
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
        
    }


    public void GetEnemyKillExperience(int expAmount)
    {
        playerTotalExp += expAmount * PlayerStats.playerXPrate;

        if (playerLevel < playerLevelThresholds.Count)
        {
            int playerLevelTresholdInt = playerLevelThresholds[playerLevel];
            float playerLevelTresholdFloat = playerLevelTresholdInt;
            float playerTotalExpFloat = playerTotalExp;
            float expPercent = playerTotalExpFloat / playerLevelTresholdFloat * 100;

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
