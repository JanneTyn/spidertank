using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuController : MonoBehaviour
{
    private bool isPaused = false;
    public bool upgrade = false;
    public GameObject pauseMenu;
    public GameObject cam;
    public GameObject upgradeMenu;
    public CrosshairRaycast crosshairCanvas;

    [SerializeField] Lolopupka.proceduralAnimation pa;

    // Start is called before the first frame update
    void Start()
    {
        Pause();
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
    public void Pause()
    {
        isPaused = true;
        pa.enabled = false;
        Time.timeScale = 0f; // Pause the game
        cam.gameObject.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (upgrade == false) {
            pauseMenu.SetActive(true); // Show the pause menu UI
        }
        else { 
            upgradeMenu.SetActive(true);
            upgrade = false;
        }
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // Unpause the game
        pa.enabled = true;
        pauseMenu.SetActive(false); // Hide the pause menu UI
        upgradeMenu.SetActive(false);
        cam.gameObject.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        crosshairCanvas.ClearDmgMarkers();
    }
}
