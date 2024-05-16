using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPicker : MonoBehaviour
{
    public GameObject machinegun;
    public GameObject shotgun;
    public GameObject cannon;
    public GameObject missileLauncher;
    public GameObject starterOptions;
    public GameObject startButton;
    public GameObject gameInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void choseMachinegun(){
        machinegun.SetActive(true);
        choseOption();
    }

    public void choseShotgun(){
        shotgun.SetActive(true);
        choseOption();
    }

    public void choseMissilelauncher(){
        missileLauncher.SetActive(true);
        choseOption();
    }

    public void choseCannon(){
        cannon.SetActive(true);
        choseOption();
    }

    void choseOption() {   
        starterOptions.SetActive(false);
        startButton.SetActive(true);
        gameInfo.SetActive(true);
    }
}
