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
    
    public void SetCurrentStatsText()
    {
        stat1.text = "Health: " + PlayerStats.playerHealth;
        stat2.text = "Damage: " + (PlayerStats.playerDamage + 100) + "%";
        stat3.text = "Fire Rate: " + (PlayerStats.playerFireRate * 100) + "%";
        stat4.text = "Movement Speed: " + (PlayerStats.playerMovementSpeed * 100) + "%";
        stat5.text = "Experience Boost: " + (PlayerStats.playerXPrate * 100) + "%";
        stat6.text = "Health Regeneration: +" + (PlayerStats.playerHealthRegen / PlayerStats.playerHealthRegenTimeInterval) + " hp/s";
        stat7.text = "Critical Chance: " + (PlayerStats.playerCriticalChance) + "%";
    }
}
