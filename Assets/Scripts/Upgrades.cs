using StarterAssets;
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
    public float[] upgradeRarities = { 350, 100, 10 }; //välillä 1-1000, rare/epic/legendary
    public string[] upgradeTitles = { "Common", "Rare", "Epic", "Legendary" };
    public Color[] upgradeColors = { Color.white, Color.blue, new Color(0.6f, 0, 1.0f, 1f), new Color(1f, 0.7f, 0, 1f) }; 
    public float[] healthUpgrade = { 15, 30, 45, 60 };
    public float[] damageUpgrade = {10, 20, 30, 40 };
    public float[] fireRateUpgrade = { 0.1f, 0.15f, 0.25f, 0.4f };
    public float[] movementSpeedUpgrade = { 0.1f, 0.2f, 0.3f, 0.4f };
    public float[] xpboostUpgrade = { 0.25f, 0.5f, 0.75f, 1.0f };
    public float[] healthRegenUpgrade = { 1, 2, 3, 4 };
    public float[] criticalUpgrade = { 3, 6, 9, 12 };
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
    public Image upgradeImage1;
    public Image upgradeImage2;
    public Image upgradeImage3;
    public List<Image> upgradeImages;
    public Sprite healthUpgradeIcon;
    public Sprite damageUpgradeIcon;
    public Sprite fireRateUpgradeIcon;
    public Sprite movementSpeedUpgradeIcon;
    public Sprite xpboostUpgradeIcon;
    public Sprite healthRegenUpgradeIcon;
    public Sprite criticalUpgradeIcon;
    public PlayerCurrentStats currentStats;
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
        upgradeImages.Add(upgradeImage1);
        upgradeImages.Add(upgradeImage2);
        upgradeImages.Add(upgradeImage3);
        currentStats.SetCurrentStatsText();


        for (int i = 0; i < 3; i++)
        {
            int rarity = RollUpgradeRarity();
            switch (upgradeList[i])
            {
                case "Health":
                    upgradeDescriptions[i].text = "+" + healthUpgrade[rarity] + " Health";
                    upgradestankorweapon[i].text = "Tank/" + upgradeTitles[rarity];
                    upgradestankorweapon[i].color = upgradeColors[rarity];
                    upgradeImages[i].sprite = healthUpgradeIcon;
                    upgradeButtons[i].onClick.AddListener(delegate { GiveHealthUpgrade(healthUpgrade[rarity]); });
                    break;
                case "Damage":
                    upgradeDescriptions[i].text = "+" + damageUpgrade[rarity] + "% Damage";
                    upgradestankorweapon[i].text = "Weapons/" + upgradeTitles[rarity];
                    upgradestankorweapon[i].color = upgradeColors[rarity];
                    upgradeImages[i].sprite = damageUpgradeIcon;
                    upgradeButtons[i].onClick.AddListener(delegate { GiveDamageUpgrade(damageUpgrade[rarity]); });
                    break;
                case "Fire Rate":
                    upgradeDescriptions[i].text = "+" + fireRateUpgrade[rarity] * 100 + "% Fire Rate";
                    upgradestankorweapon[i].text = "Weapons/" + upgradeTitles[rarity];
                    upgradestankorweapon[i].color = upgradeColors[rarity];
                    upgradeImages[i].sprite = fireRateUpgradeIcon;
                    upgradeButtons[i].onClick.AddListener(delegate { GiveFireRateUpgrade(fireRateUpgrade[rarity]); });
                    break;
                case "Movement Speed":
                    upgradeDescriptions[i].text = "+" + movementSpeedUpgrade[rarity] * 100 + "% Movement Speed";
                    upgradestankorweapon[i].text = "Tank/" + upgradeTitles[rarity];
                    upgradestankorweapon[i].color = upgradeColors[rarity];
                    upgradeImages[i].sprite = movementSpeedUpgradeIcon;
                    upgradeButtons[i].onClick.AddListener(delegate { GiveMovementSpeedUpgrade(movementSpeedUpgrade[rarity]); });
                    break;
                case "Experience Boost":
                    upgradeDescriptions[i].text = "+" + xpboostUpgrade[rarity] * 100 + "% Experience Boost";
                    upgradestankorweapon[i].text = "Tank/" + upgradeTitles[rarity];
                    upgradestankorweapon[i].color = upgradeColors[rarity];
                    upgradeImages[i].sprite = xpboostUpgradeIcon;
                    upgradeButtons[i].onClick.AddListener(delegate { GiveXPBoostUpgrade(xpboostUpgrade[rarity]); });
                    break;
                case "Health Regeneration":
                    upgradeDescriptions[i].text = "Heal for +" + healthRegenUpgrade[rarity] + " every " + PlayerStats.playerHealthRegenTimeInterval + " seconds";
                    upgradestankorweapon[i].text = "Tank/" + upgradeTitles[rarity];
                    upgradestankorweapon[i].color = upgradeColors[rarity];
                    upgradeImages[i].sprite = healthRegenUpgradeIcon;
                    upgradeButtons[i].onClick.AddListener(delegate { GiveHealthRegenUpgrade(healthRegenUpgrade[rarity]); });
                    break;
                case "Critical":
                    upgradeDescriptions[i].text = "+" + criticalUpgrade[rarity] + "% chance for Critical (3x damage)";
                    upgradestankorweapon[i].text = "Weapons/" + upgradeTitles[rarity];
                    upgradestankorweapon[i].color = upgradeColors[rarity];
                    upgradeImages[i].sprite = criticalUpgradeIcon;
                    upgradeButtons[i].onClick.AddListener(delegate { GiveCriticalUpgrade(criticalUpgrade[rarity]); });
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
        upgradeImages.Clear();
    }

    public void GiveHealthUpgrade(float hpUpgrade)
    {
        Debug.Log("health chosen");
        PlayerStats.playerHealth += (int)hpUpgrade;
        currentStats.UpdateColor(0);
        RemoveButtonListeners();
    }
    public void GiveDamageUpgrade(float dmgUpgrade)
    {
        Debug.Log("dmg chosen");
        PlayerStats.playerDamage += dmgUpgrade;
        currentStats.UpdateColor(1);
        RemoveButtonListeners();
    }
    public void GiveFireRateUpgrade(float firerateUpg)
    {
        Debug.Log("firerate chosen");
        PlayerStats.playerFireRate += firerateUpg;
        currentStats.UpdateColor(2);
        RemoveButtonListeners();
    }
    public void GiveMovementSpeedUpgrade(float speedUpgrade)
    {
        Debug.Log("movementspeed chosen");
        PlayerStats.playerMovementSpeed += speedUpgrade;
        currentStats.UpdateColor(3);
        ChangeMovementSpeed();
        RemoveButtonListeners();
    }
    public void GiveXPBoostUpgrade(float xpboost)
    {
        Debug.Log("xpboost chosen");
        PlayerStats.playerXPrate += xpboost;
        currentStats.UpdateColor(4);
        RemoveButtonListeners();
    }
    public void GiveHealthRegenUpgrade(float hpregenUpgrade)
    {
        Debug.Log("healthregen chosen");
        PlayerStats.playerHealthRegen += hpregenUpgrade;
        currentStats.UpdateColor(5);
        RemoveButtonListeners();
    }
    public void GiveCriticalUpgrade(float critical)
    {
        Debug.Log("critical chosen");
        PlayerStats.playerCriticalChance += critical;
        currentStats.UpdateColor(6);
        RemoveButtonListeners();
    }

    public void ChangeMovementSpeed()
    {
        GetComponent<ThirdPersonController>().UpdateMovementSpeed();
    }

    public int RollUpgradeRarity()
    {
        int rarity = Random.Range(0, 1000);
        if (rarity > upgradeRarities[0] ) { return 0; }
        else if (rarity > upgradeRarities[1] ) { return 1; }
        else if (rarity > upgradeRarities[2] ) {  return 2; }
        else {  return 3; }
    }




    public void RemoveButtonListeners()
    {
        upgradeButton1.onClick.RemoveAllListeners();
        upgradeButton2.onClick.RemoveAllListeners();
        upgradeButton3.onClick.RemoveAllListeners();
    }
}
