using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPbar : MonoBehaviour
{
    public static int health;
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
        text.text = $"Health: {health}";
    }

    void UpdateHP()
    {
        text.text = $"Health: {health}";
    }
}
