using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class shotgun : MonoBehaviour
{

    public int baseDamage = 5;
    private int trueDamage = 0;
    public float perShotDelay = 1f;
    public float bulletSpread = 1f;
    public int bullets = 6;
    float shotsfired = 0;
    public float range = 10;
    public int side = 0;
    public ThirdPersonCam cam;
    public CrosshairRaycast crosshair;
    private GameObject shotEnemy;
    private List<GameObject> enemiesHit;
    public TMP_Text shotDelayText1;
    public TMP_Text shotDelayText2;
    private float timestamp = 0.0f;
    public float amplitudeGain = 0.1f;
    public float frequencyGain = 0.1f;
    public float shakeDuration = 0.1f;

    public ParticleSystem muzzle;

    public SoundController sound;

    [SerializeField] BulletEffect bulletEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(side))
        {
            //StartCoroutine(cam.Shake(amplitudeGain, frequencyGain, shakeDuration));
            if (Time.time > timestamp)
            {
                timestamp = Time.time + (perShotDelay * (1 / PlayerStats.playerFireRate));
                shotsfired++;
                //Debug.Log("Pam " + shotsfired);

                muzzle.Play();
                sound.ShotgunSound();
                bulletEffect.muzzle = muzzle.transform.position;

                for (int i = 0; i < bullets; i++) {
                    //enemiesHit = new List<GameObject>();
                    //shotEnemy = crosshair.checkEnemyRaycast(out hit, bulletSpread);
                    if (crosshair.checkEnemyRaycast(out RaycastHit hit, bulletSpread))
                    {
                        shotEnemy = hit.collider.gameObject;
                        trueDamage = Mathf.RoundToInt(baseDamage * (1 + (PlayerStats.playerDamage / 100)));
                        trueDamage = PlayerStats.RollCriticalChance(trueDamage);
                        //vihuun osuttu, vähennetään healthia
                        Debug.Log("Enemy hit");

                        if (shotEnemy.TryGetComponent(out Enemy enemyScript))
                        {
                            enemyScript = GetEnemyParentScript();
                            if (enemyScript != null)
                            {
                                //CalculateDamageByDistance(baseDamage, range, shotEnemy);
                                enemyScript.TakeDamage(trueDamage);
                                crosshair.createDamageMarker(trueDamage, hit.point);
                            }
                            else
                            {
                                Debug.Log("ENEMY NULL!!!");
                            }
                        }
                        else if (shotEnemy.TryGetComponent(out EnemyRange enemyScriptRange))
                        {
                            enemyScriptRange = GetRangeEnemyParentScript();
                            if (enemyScriptRange != null)
                            {
                                //CalculateDamageByDistance(baseDamage, range, shotEnemy);
                                enemyScriptRange.TakeDamage(trueDamage);
                                crosshair.createDamageMarker(trueDamage, hit.point);
                            }
                            else
                            {
                                Debug.Log("ENEMY NULL!!!");
                            }
                        }
                        else if (shotEnemy.TryGetComponent(out EnemyExploder enemyScriptExploder))
                        {
                            enemyScriptExploder = GetExploderEnemyParentScript();
                            if (enemyScriptExploder != null)
                            {
                                enemyScriptExploder.TakeDamage(trueDamage);
                                crosshair.createDamageMarker(trueDamage, hit.point);
                            }
                            else
                            {
                                Debug.Log("ENEMY NULL!!!");
                            }
                        }
                        else
                        {
                            Debug.Log("Unknown enemy");
                        }
                    }
                }
            }
        }       
        else { cam.endShake(); }

        DisplayRemainingShotDelay();
    }

    Enemy GetEnemyParentScript()
    {
        for (int i = 0; i < 6; i++)
        {
            Enemy enemyscript = shotEnemy.GetComponent<Enemy>();
            if (enemyscript != null)
            {
                return enemyscript;
            }
            else
            {
                if (shotEnemy.transform.parent != null)
                {
                    shotEnemy = shotEnemy.transform.parent.gameObject;
                }
            }
        }
        return null;
    }

    EnemyRange GetRangeEnemyParentScript()
    {
        for (int i = 0; i < 6; i++)
        {
            EnemyRange enemyscript = shotEnemy.GetComponent<EnemyRange>();
            if (enemyscript != null)
            {
                return enemyscript;
            }
            else
            {
                if (shotEnemy.transform.parent != null)
                {
                    shotEnemy = shotEnemy.transform.parent.gameObject;
                }
            }
        }
        return null;
    }

    EnemyExploder GetExploderEnemyParentScript()
    {
        for (int i = 0; i < 6; i++)
        {
            EnemyExploder enemyscript = shotEnemy.GetComponent<EnemyExploder>();
            if (enemyscript != null)
            {
                return enemyscript;
            }
            else
            {
                if (shotEnemy.transform.parent != null)
                {
                    shotEnemy = shotEnemy.transform.parent.gameObject;
                }
            }
        }
        return null;
    }
    public void DisplayRemainingShotDelay()
    {
        float timeLeft = timestamp - Time.time;
        if (side == 0)
        {
            if (timeLeft < 0)
            {
                shotDelayText1.gameObject.SetActive(false);
            }
            else
            {
                shotDelayText1.gameObject.SetActive(true);
                shotDelayText1.text = timeLeft.ToString("F1");
            }
        }
        else
        {
            if (timeLeft < 0)
            {
                shotDelayText2.gameObject.SetActive(false);
            }
            else
            {
                shotDelayText2.gameObject.SetActive(true);
                shotDelayText2.text = timeLeft.ToString("F1");
            }
        }
    }
}
