using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{

    Collider m_Collider;
    public PlayerHitEffect hitEffect;
    public GameObject gameoverScreen;
    public Slider HPSlider;
    public int curHP = 100;


    public int healAmount = 25;

    public int damage = 5;
    public int damageProjectile = 10;
    public int damageExplosion = 20;
    public int playerDeathScreenTime = 4;

    public SoundController sound;

    [SerializeField] public Image imageReference;
    [SerializeField] private Gradient gradient;

    private void UpdateFillBarAmount(float amount)
    {
        imageReference.fillAmount = amount;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Collider = GetComponent<Collider>();
        imageReference.color = gradient.Evaluate(1f);
        StartCoroutine(HealthRegeneration());

    }

    // Update is called once per frame
    void Update()
    {

        if (curHP >= PlayerStats.playerHealth)
        {
            curHP = PlayerStats.playerHealth;
        }

        if (curHP < 0)
        {
            curHP = 0;
        }

        if (curHP == 0)
        {
            StartCoroutine(PlayerDeath());
        }

        HPSlider.value = curHP;
        HPSlider.maxValue = PlayerStats.playerHealth;
        imageReference.color = gradient.Evaluate(HPSlider.normalizedValue);

    }
    
    
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 7)
        {
            curHP -= damage;
            hitEffect.PlayerDamagedEffect();

        }
        if (collision.gameObject.tag == "Projectile")
        {
            sound.RangedDamageSound();
            curHP -= damageProjectile;
            hitEffect.PlayerDamagedEffect();


        }

        if (collision.gameObject.tag == "HealthPack")
        {
            curHP += healAmount;
            if (curHP > PlayerStats.playerHealth)
            {
                curHP = PlayerStats.playerHealth;
            }
        }

        if (collision.gameObject.tag == "Explosion")
        {
            sound.EnemyExplosionSound();
            curHP -= damageExplosion;
            hitEffect.PlayerDamagedEffect();
            // StartCoroutine("Iframe");
        }
    }
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer == 7)
        {
            StartCoroutine("DelayDmg");

        }
        if (collision.gameObject.tag == "TankEnemy")
        {
            StopCoroutine("DelayDmg");
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 7)
        {
            StopCoroutine("DelayDmg");

        }
    }

    private IEnumerator DelayDmg()
    {
        yield return new WaitForSeconds(1f);
        curHP -= damage;
        hitEffect.PlayerDamagedEffect();

        StopCoroutine("DelayDmg");
    }

    private IEnumerator Iframe()
    {
        
        m_Collider.enabled = !m_Collider.enabled;
        yield return new WaitForSeconds(0.2f);
        m_Collider.enabled = !m_Collider.enabled;
        StopCoroutine("Iframe");
    }

    public IEnumerator HealthRegeneration()
    {
        while (true)
        {
            yield return new WaitForSeconds(PlayerStats.playerHealthRegenTimeInterval);
            curHP += (int)PlayerStats.playerHealthRegen;
            if (curHP >= PlayerStats.playerHealth)
            {
                curHP = PlayerStats.playerHealth;
            }
        }
    }

    public IEnumerator PlayerDeath()
    {
        float elapsedTime = 0;
        gameoverScreen.SetActive(true);
        Time.timeScale = 0f;
        while (elapsedTime < playerDeathScreenTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1f;
        gameoverScreen.SetActive(false);
        PlayerStats.ResetDefaultValues();
        SceneManager.LoadScene("MainMenu");
    }
   
}
