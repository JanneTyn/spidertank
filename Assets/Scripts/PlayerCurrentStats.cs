using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCurrentStats : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text stat1;
    public TMP_Text stat2;
    public TMP_Text stat3;
    public TMP_Text stat4;
    public TMP_Text stat5;
    public TMP_Text stat6;
    public TMP_Text stat7;
    public string[] colors = new string[7];

    private void Start()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = "<color=white>";
        }
    }

    public void UpdateColor(int id)
    {
        colors[id] = "<color=green>";
    }

    public void SetCurrentStatsText()
    {
        stat1.text = "Health: " + colors[0] + PlayerStats.playerHealth;
        stat2.text = "Damage: " + colors[1] + (PlayerStats.playerDamage + 100) + "%";
        stat3.text = "Fire Rate: " + colors[2] + (PlayerStats.playerFireRate * 100) + "%";
        stat4.text = "Movement Speed: " + colors[3] + (PlayerStats.playerMovementSpeed * 100) + "%";
        stat5.text = "Experience Boost: " + colors[4] + (PlayerStats.playerXPrate * 100) + "%";
        stat6.text = "Health Regeneration: +" + colors[5] + (PlayerStats.playerHealthRegen / PlayerStats.playerHealthRegenTimeInterval) + " hp/s";
        stat7.text = "Critical Chance: " + colors[6] + (PlayerStats.playerCriticalChance) + "%";
    }
}
