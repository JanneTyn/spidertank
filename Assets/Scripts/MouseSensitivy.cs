using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MouseSensitivy : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider sensSlider;
    public CinemachineFreeLook Ccamera;
    public TMP_Text sensText;
    float maxYvalue = 0.1f;
    float maxXvalue = 5;

    float sensValue;
    void Start()
    {
        sensSlider.value = 75f;
    }

    // Update is called once per frame
    void Update()
    {
        sensValue = sensSlider.value;
        sensText.text = "Mouse sensitivity: " + sensValue.ToString("F0") + "%";
        Ccamera.m_XAxis.m_MaxSpeed = maxXvalue * (sensValue / 100);
        Ccamera.m_YAxis.m_MaxSpeed = maxYvalue * (sensValue / 100);
    }
}
