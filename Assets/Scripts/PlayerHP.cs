using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{

    Collider m_Collider;
    public Slider HPSlider;
    public int maxHealth = 100;
    public int curHP = 100;


    public int healAmount = 25;

    public int damage = 5;
    public int damageProjectile = 10;
    public int damageExplosion = 20;

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

    }

    // Update is called once per frame
    void Update()
    {

        if (curHP >= 100)
        {
            curHP = 100;
        }

        if (curHP < 0)
        {
            curHP = 0;
        }

        if (curHP == 0)
        {
            SceneManager.LoadScene("MainMenu");
        }

        HPSlider.value = curHP;
        imageReference.color = gradient.Evaluate(HPSlider.normalizedValue);

    }
    
    
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 7)
        {
            curHP -= damage;

        }
        if (collision.gameObject.tag == "Projectile")
        {
            curHP -= damageProjectile;
            
        }

        if (collision.gameObject.tag == "HealthPack")
        {
            curHP += healAmount;
        }

        if (collision.gameObject.tag == "TankEnemy")
        {
            curHP -= damageExplosion;
            StartCoroutine("Iframe");
        }
    }


    private IEnumerator Iframe()
    {
        
        m_Collider.enabled = !m_Collider.enabled;
        yield return new WaitForSeconds(0.2f);
        m_Collider.enabled = !m_Collider.enabled;
        StopCoroutine("Iframe");
    }
   
}
