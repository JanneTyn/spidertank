using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public string[] upgradesListCommon = { "Health", "Damage", "Fire Rate", "Movement Speed", "Experience Boost" };
    public string[] upgradesListRare = { "Health Regeneration", "Critical" };
    public float rareUpgradeChance = 20;
    public float healthUpgrade = 15;
    public float damageUpgrade = 5;
    public float fireRateUpgrade = 5;
    public float movementSpeedUpgrade = 10;
    public float xpboostUpgrade = 20;
    public float healthRegenUpgrade = 1;
    public float criticalUpgrade = 3;
    public TMP_Text upgradetext1;
    public TMP_Text upgradetext2;
    public TMP_Text upgradetext3;
    public TMP_Text upgradedescription1;
    public TMP_Text upgradedescription2;
    public TMP_Text upgradedescription3;
    public TMP_Text upgradetankorweapon1;
    public TMP_Text upgradetankorweapon2;
    public TMP_Text upgradetankorweapon3;
    public List<TMP_Text> upgradeDescriptions;
    public List<TMP_Text> upgradestankorweapon;
    public Button upgradeButton1;
    public Button upgradeButton2;
    public Button upgradeButton3;
    public List<Button> upgradeButtons;
    private List<string> randomUpgrades = new List<string>();
    private List<string> upgradeList = new List<string>();
    private int rolledUpgrades = 0;
    private int rollAttempts = 0;
    private int upgradeNumber= 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GetRandomLevelUpgrades()
    {
        while (randomUpgrades.Count < 3 && rollAttempts < 1000)
        {
            int upgradeRarityRoll = Random.Range(1, 100);

            if (upgradeRarityRoll < rareUpgradeChance)
            {
                upgradeNumber = Random.Range(0, upgradesListRare.Length);

                if (!randomUpgrades.Contains(upgradesListRare[upgradeNumber]))
                {
                    randomUpgrades.Add(upgradesListRare[upgradeNumber]);
                }
            }
            else 
            {
                upgradeNumber = Random.Range(0, upgradesListCommon.Length);

                if (!randomUpgrades.Contains(upgradesListCommon[upgradeNumber]))
                {
                    randomUpgrades.Add(upgradesListCommon[upgradeNumber]);
                }
            }

            Debug.Log("roll: " + rollAttempts);
            rollAttempts++;
            yield return null;
        }
        rollAttempts = 0;     
        Debug.Log(randomUpgrades[0] + " " + randomUpgrades[1] + " " + randomUpgrades[2]);
        upgradeList = randomUpgrades;

        SetUpgradeText();

        randomUpgrades.Clear();

    }
    public void SetUpgradeText()
    {
        upgradetext1.text = upgradeList[0];
        upgradetext2.text = upgradeList[1];
        upgradetext3.text = upgradeList[2];
        upgradeDescriptions.Add(upgradedescription1);
        upgradeDescriptions.Add(upgradedescription2);
        upgradeDescriptions.Add(upgradedescription3);
        upgradestankorweapon.Add(upgradetankorweapon1);
        upgradestankorweapon.Add(upgradetankorweapon2);
        upgradestankorweapon.Add(upgradetankorweapon3);
        upgradeButtons.Add(upgradeButton1);
        upgradeButtons.Add(upgradeButton2);
        upgradeButtons.Add(upgradeButton3);

        for (int i = 0; i < 3; i++)
        {
            switch (upgradeList[i])
            {
                case "Health":
                    upgradeDescriptions[i].text = "+" + healthUpgrade +  " Health";
                    upgradestankorweapon[i].text = "Tank";
                    upgradestankorweapon[i].color = Color.green;
                    upgradeButtons[i].onClick.AddListener(delegate { GiveHealthUpgrade(healthUpgrade); });
                    break;
                case "Damage":
                    upgradeDescriptions[i].text = "+" + damageUpgrade + "% Damage";
                    upgradestankorweapon[i].text = "Weapons";
                    upgradestankorweapon[i].color = Color.blue;
                    upgradeButtons[i].onClick.AddListener(delegate { GiveDamageUpgrade(damageUpgrade); });
                    break;
                case "Fire Rate":
                    upgradeDescriptions[i].text = "+" + fireRateUpgrade + "% Fire Rate";
                    upgradestankorweapon[i].text = "Weapons";
                    upgradestankorweapon[i].color = Color.blue;
                    upgradeButtons[i].onClick.AddListener(delegate { GiveFireRateUpgrade(fireRateUpgrade); });
                    break;
                case "Movement Speed":
                    upgradeDescriptions[i].text = "+" + movementSpeedUpgrade + "% Movement Speed";
                    upgradestankorweapon[i].text = "Tank";
                    upgradestankorweapon[i].color = Color.green;
                    upgradeButtons[i].onClick.AddListener(delegate { GiveMovementSpeedUpgrade(movementSpeedUpgrade); });
                    break;
                case "Experience Boost":
                    upgradeDescriptions[i].text = "+" + xpboostUpgrade + "% Experience Boost";
                    upgradestankorweapon[i].text = "Tank";
                    upgradestankorweapon[i].color = Color.green;
                    upgradeButtons[i].onClick.AddListener(delegate { GiveXPBoostUpgrade(xpboostUpgrade); });
                    break;
                case "Health Regeneration":
                    upgradeDescriptions[i].text = "Heal +" + healthRegenUpgrade + " every 2 seconds";
                    upgradestankorweapon[i].text = "Tank";
                    upgradestankorweapon[i].color = Color.green;
                    upgradeButtons[i].onClick.AddListener(delegate { GiveHealthRegenUpgrade(healthRegenUpgrade); });
                    break;
                case "Critical":
                    upgradeDescriptions[i].text = "+" + criticalUpgrade + "% chance for Critical (3x damage)";
                    upgradestankorweapon[i].text = "Weapons";
                    upgradestankorweapon[i].color = Color.blue;
                    upgradeButtons[i].onClick.AddListener(delegate { GiveCriticalUpgrade(criticalUpgrade); });
                    break;
                default:
                    upgradeDescriptions[i].text = "Unknown";
                    upgradestankorweapon[i].text = "Unknown";
                    upgradestankorweapon[i].color = Color.red;
                    break;
            }
        }

        upgradeDescriptions.Clear();
        upgradestankorweapon.Clear();
    }

    public void GiveHealthUpgrade(float hpUpgrade)
    {
        Debug.Log("health chosen");
        PlayerStats.playerHealth += (int)hpUpgrade;
        RemoveButtonListeners();
    }
    public void GiveDamageUpgrade(float dmgUpgrade)
    {
        Debug.Log("dmg chosen");
        PlayerStats.playerDamage += dmgUpgrade;
        RemoveButtonListeners();
    }
    public void GiveFireRateUpgrade(float firerateUpg)
    {
        Debug.Log("firerate chosen");
        PlayerStats.playerFireRate += firerateUpg;
        RemoveButtonListeners();
    }
    public void GiveMovementSpeedUpgrade(float speedUpgrade)
    {
        Debug.Log("movementspeed chosen");
        PlayerStats.playerMovementSpeed += speedUpgrade;
        RemoveButtonListeners();
    }
    public void GiveXPBoostUpgrade(float xpboost)
    {
        Debug.Log("xpboost chosen");
        PlayerStats.playerXPrate += xpboost;
        RemoveButtonListeners();
    }
    public void GiveHealthRegenUpgrade(float hpregenUpgrade)
    {
        Debug.Log("healthregen chosen");
        PlayerStats.playerHealthRegen += hpregenUpgrade;
        RemoveButtonListeners();
    }
    public void GiveCriticalUpgrade(float critical)
    {
        Debug.Log("critical chosen");
        PlayerStats.playerCriticalChance += critical;
        RemoveButtonListeners();
    }




    public void RemoveButtonListeners()
    {
        upgradeButton1.onClick.RemoveAllListeners();
        upgradeButton2.onClick.RemoveAllListeners();
        upgradeButton3.onClick.RemoveAllListeners();
    }
}
