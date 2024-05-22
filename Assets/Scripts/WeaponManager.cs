using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject machineGun;
    public GameObject machineGunRight;
    public GameObject shotGun;
    public GameObject shotGunRight;
    public GameObject cannon;
    public GameObject cannonRight;
    public GameObject missileLauncher;
    public GameObject missileLauncherRight;
    private machineGunScript machinegunscript;
    private shotgun shotgunscript;
    private cannonScript cannonscript;
    private MissileLauncherScript missilescript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWeapon(int weaponNumber, int side) 
    {
        switch (weaponNumber)
        {
            case 0:
                
                if (side == 1)
                {
                    machineGunRight.SetActive(true);
                    //machineGunRight.transform.position = new Vector3(0.315f, machineGun.transform.position.y, machineGun.transform.position.z);
                    //machineGunRight.transform.localScale = new Vector3(-1, 1, 1);
                    machineGunRight.GetComponent<machineGunScript>().side = 1;
                }
                else
                {
                    machineGun.SetActive(true);
                    //machineGun.transform.position = new Vector3(-0.315f, machineGun.transform.position.y, machineGun.transform.position.z);
                    //machineGun.transform.localScale = new Vector3(1, 1, 1);
                    machineGun.GetComponent<machineGunScript>().side = 0;
                }
                return;
            case 1:
                
                if (side == 1)
                {
                    shotGunRight.SetActive(true);
                    //shotGunRight.transform.position = new Vector3(0.315f, machineGun.transform.position.y, machineGun.transform.position.z);
                    //shotGunRight.transform.localScale = new Vector3(-1, 1, 1);
                    shotGunRight.GetComponent<shotgun>().side = 1;
                }
                else
                {
                    shotGun.SetActive(true);
                    //shotGun.transform.position = new Vector3(-0.315f, machineGun.transform.position.y, machineGun.transform.position.z);
                    //shotGun.transform.localScale = new Vector3(1, 1, 1);
                    shotGun.GetComponent<shotgun>().side = 0;
                }
                return;
            case 2:
                
                if (side == 1)
                {
                    cannonRight.SetActive(true);
                    //cannonRight.transform.position = new Vector3(0.315f, machineGun.transform.position.y, machineGun.transform.position.z);
                    //cannonRight.transform.localScale = new Vector3(-1, 1, 1);
                    cannonRight.GetComponent<cannonScript>().side = 1;
                }
                else
                {
                    cannon.SetActive(true);
                    //cannon.transform.position = new Vector3(-0.315f, machineGun.transform.position.y, machineGun.transform.position.z);
                    //cannon.transform.localScale = new Vector3(1, 1, 1);
                    cannon.GetComponent<cannonScript>().side = 0;
                }
                return;
            case 3:
                
                if (side == 1)
                {
                    missileLauncherRight.SetActive(true);
                    //missileLauncherRight.transform.position = new Vector3(0.315f, machineGun.transform.position.y, machineGun.transform.position.z);
                    //missileLauncherRight.transform.localScale = new Vector3(-1, 1, 1);
                    missileLauncherRight.GetComponent<MissileLauncherScript>().side = 1;
                }
                else
                {
                    missileLauncher.SetActive(true);
                    //missileLauncher.transform.position = new Vector3(-0.315f, machineGun.transform.position.y, machineGun.transform.position.z);
                    //missileLauncher.transform.localScale = new Vector3(1, 1, 1);
                    missileLauncher.GetComponent<MissileLauncherScript>().side = 0;
                }
                return;
        }
    }
}
