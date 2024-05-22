using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public WeaponManager weaponManager;
    public TMP_Text pickText;
    bool firstWeaponChosen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void choseMachinegun(){
        if (firstWeaponChosen == false)
        {
            weaponManager.SetWeapon(0, 0);
            pickText.text = "Choose your second weapon!";
            firstWeaponChosen = true;
            
        }
        else
        {
            weaponManager.SetWeapon(0, 1);
            choseOption();
        }
    }

    public void choseShotgun(){
        if (firstWeaponChosen == false)
        {
            weaponManager.SetWeapon(1, 0);
            pickText.text = "Choose your second weapon!";
            firstWeaponChosen = true;
        }
        else
        {
            weaponManager.SetWeapon(1, 1);
            choseOption();
        }
    }

    public void choseCannon()
    {
        if (firstWeaponChosen == false)
        {
            weaponManager.SetWeapon(2, 0);
            pickText.text = "Choose your second weapon!";
            firstWeaponChosen = true;
        }
        else
        {
            weaponManager.SetWeapon(2, 1);
            choseOption();
        }
    }

    public void choseMissilelauncher(){
        if (firstWeaponChosen == false)
        {
            weaponManager.SetWeapon(3, 0);
            pickText.text = "Choose your second weapon!";
            firstWeaponChosen = true;
        }
        else
        {
            weaponManager.SetWeapon(3, 1);
            choseOption();
        }
    }

    

    void choseOption() {   
        starterOptions.SetActive(false);
        startButton.SetActive(true);
        gameInfo.SetActive(true);
    }
}
