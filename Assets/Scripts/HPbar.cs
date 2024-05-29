using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPbar : MonoBehaviour
{
    public static int health;
    public static int Maxhealth;
    TMPro.TMP_Text text;
    public PlayerHP hpScript;

    // Start is called before the first frame update
    void Awake()
    {
        
        text = GetComponent<TMPro.TMP_Text>();

        hpScript = GameObject.FindWithTag("Player").GetComponent<PlayerHP>();

    }

    void Start() => UpdateHP();
    // Update is called once per frame
    void Update()
    {
        health = hpScript.curHP;
        Maxhealth = hpScript.maxHealth;
        text.text = $"Health: {health}" + "/" + Maxhealth;
    }

    void UpdateHP()
    {
        text.text = $"Health: {health}" + "/" + Maxhealth;
    }
}
