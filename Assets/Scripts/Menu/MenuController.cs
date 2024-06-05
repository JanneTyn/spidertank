using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject cam;
    public GameObject upgradeMenu;
    public GameObject dmgEffect;
    public Upgrades upgrades;
    public CrosshairRaycast crosshairCanvas;

    [SerializeField] Lolopupka.proceduralAnimation pa;

    // Start is called before the first frame update
    void Start()
    {
        Pause(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause(false);
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                if (!upgradeMenu.activeSelf)
                {
                    Resume();
                }
            }
        }  
    }
    public void Pause(bool upgrade)
    {
        isPaused = true;
        pa.enabled = false;

        cam.gameObject.SetActive(false);
        dmgEffect.gameObject.SetActive(false);
        crosshairCanvas.ClearDmgMarkers();

        Time.timeScale = 0f; // Pause the game     
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (upgrade == false) {
            pauseMenu.SetActive(true); // Show the pause menu UI
        }
        else { 
            upgradeMenu.SetActive(true);
            //upgrades.SetUpgradeText();
        }
    }

    public void Resume()
    {
        isPaused = false;
        cam.gameObject.SetActive(true);
        Time.timeScale = 1f; // Unpause the game
        pa.enabled = true;
        pauseMenu.SetActive(false); // Hide the pause menu UI
        upgradeMenu.SetActive(false);      
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;        
    }

    public void QuitToMenu()
    {
        PlayerStats.ResetDefaultValues();
        SceneManager.LoadScene("MainMenu");
    }
}
