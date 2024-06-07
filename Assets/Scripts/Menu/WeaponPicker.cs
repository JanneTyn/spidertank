using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPicker : MonoBehaviour
{
    public GameObject machinegun;
    public GameObject shotgun;
    public GameObject cannon;
    public GameObject missileLauncher;
    public GameObject starterOptions;
    public GameObject startButton;
    public GameObject backtoMenuButton;
    public GameObject gameInfo;
    public Image weaponImage1;
    public Image weaponImage2;
    public Sprite machinegunImage;
    public Sprite shotgunImage;
    public Sprite cannonImage;
    public Sprite missileLauncherImage;
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
            weaponImage1.gameObject.SetActive(true);
            weaponImage1.sprite = machinegunImage;
            
        }
        else
        {
            weaponManager.SetWeapon(0, 1);
            weaponImage2.gameObject.SetActive(true);
            weaponImage2.sprite = machinegunImage;
            choseOption();
        }
    }

    public void choseShotgun(){
        if (firstWeaponChosen == false)
        {
            weaponManager.SetWeapon(1, 0);
            weaponImage1.gameObject.SetActive(true);
            weaponImage1.sprite = shotgunImage;
            pickText.text = "Choose your second weapon!";
            firstWeaponChosen = true;
        }
        else
        {
            weaponManager.SetWeapon(1, 1);
            weaponImage2.gameObject.SetActive(true);
            weaponImage2.sprite = shotgunImage;
            choseOption();
        }
    }

    public void choseCannon()
    {
        if (firstWeaponChosen == false)
        {
            weaponManager.SetWeapon(2, 0);
            weaponImage1.gameObject.SetActive(true);
            weaponImage1.sprite = cannonImage;
            pickText.text = "Choose your second weapon!";
            firstWeaponChosen = true;
        }
        else
        {
            weaponManager.SetWeapon(2, 1);
            weaponImage2.gameObject.SetActive(true);
            weaponImage2.sprite = cannonImage;
            choseOption();
        }
    }

    public void choseMissilelauncher(){
        if (firstWeaponChosen == false)
        {
            weaponManager.SetWeapon(3, 0);
            weaponImage1.gameObject.SetActive(true);
            weaponImage1.sprite = missileLauncherImage;
            pickText.text = "Choose your second weapon!";
            firstWeaponChosen = true;
        }
        else
        {
            weaponManager.SetWeapon(3, 1);
            weaponImage2.gameObject.SetActive(true);
            weaponImage2.sprite = missileLauncherImage;
            choseOption();
        }
    }

    

    void choseOption() {   
        starterOptions.SetActive(false);
        startButton.SetActive(true);
        backtoMenuButton.SetActive(true);
        gameInfo.SetActive(true);
    }
}
