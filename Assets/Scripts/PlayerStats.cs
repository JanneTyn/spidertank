using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static int playerHealth = 100;
    public static float playerDamage = 0; //kaikki prosenttim‰‰rin‰
    public static float playerFireRate = 1.00f; 
    public static float playerMovementSpeed = 1.00f;
    public static float playerXPrate = 0;
    public static float playerHealthRegen = 0;
    public static float playerCriticalChance = 0;

    // Start is called before the first frame update
    public static void ResetDefaultValues()
    {
        playerHealth = 100;
        playerDamage = 0;
        playerFireRate = 1.00f;                    
        playerMovementSpeed = 0;
        playerXPrate = 0;
        playerHealthRegen = 0;
        playerCriticalChance = 0;
    }
}
