using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lolopupka;

public class AnimationAdjuster : MonoBehaviour
{
    [SerializeField] proceduralAnimation playerAnim;
    private PlayerLeveling levelup;

    public float originalSpeed;
    public float speed;

    // Start is called before the first frame update
    void Awake()
    {
        levelup = GetComponent<PlayerLeveling>();

        originalSpeed = PlayerStats.playerMovementSpeed;
        speed = PlayerStats.playerMovementSpeed;
    }

    public void AdjustAnimation()
    {
        if  (speed != PlayerStats.playerMovementSpeed)
        {
            speed= PlayerStats.playerMovementSpeed;

            float modifier = (speed - 1) / 4;

            playerAnim.cycleLimit = playerAnim.cycleLimit - modifier;
            playerAnim.stepSpeed = playerAnim.stepSpeed + modifier;
        }
    }
}
