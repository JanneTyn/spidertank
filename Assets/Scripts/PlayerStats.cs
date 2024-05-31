using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static int playerHealth = 100;
    public static float playerDamage = 0; //kaikki prosenttimäärinä
    public static float playerFireRate = 1.00f; 
    public static float playerMovementSpeed = 1.00f;
    public static float playerXPrate = 1.00f;
    public static float playerHealthRegen = 0;
    public static float playerHealthRegenTimeInterval = 2;
    public static float playerCriticalChance = 0;
    public static float playerCriticalMultiplier = 3;
    public ThirdPersonController thirdPersonController;

    // Start is called before the first frame update
    public static void ResetDefaultValues()
    {
        playerHealth = 100;
        playerDamage = 0;
        playerFireRate = 1.00f;                    
        playerMovementSpeed = 1.00f;
        playerXPrate = 1.00f;
        playerHealthRegen = 0;
        playerHealthRegenTimeInterval = 2;
        playerCriticalChance = 0;
        playerCriticalMultiplier = 3;     
    }

    public void ResetMoveSpeed()
    {
        thirdPersonController.ResetMovementSpeed();
    }

    public static int RollCriticalChance(int dmg)
    {
        float rng = Random.Range(1, 101);
        if (rng < playerCriticalChance ) 
        {
            dmg = dmg * (int)playerCriticalMultiplier;
        }
        return dmg;
    }
}
