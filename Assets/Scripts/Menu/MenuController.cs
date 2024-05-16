using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuController : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject UpgradeMenu;
    PlayerLeveling Levelingscript;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }  
    }
    void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pause the game
        //pausemenu.SetActive(true); // Show the pause menu UI
    }

    void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // Unpause the game
        pauseMenu.SetActive(false); // Hide the pause menu UI
    }

        public void Upgrade()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pause the game
        UpgradeMenu.SetActive(true); // Show the pause menu UI
    }
}
