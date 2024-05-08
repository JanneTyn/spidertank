using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UIElements;

public class ThirdPersonCam : MonoBehaviour
{
    public GameObject playerScript;
    public CinemachineFreeLook Ccamera;
    public float amplitudeGain = 0.1f;
    public float frequencyGain = 0.1f;
    public float shakeDuration = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //var transposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>();
        //float XRotation = transposer.m_XAxis.Value;
        //float XRotation = GetComponent<Camera>().m_XAxis.Value;
        //Debug.Log("rotation: " + XRotation);

    }

    public IEnumerator Shake()
    {
        Debug.Log("shake");
        Noise(amplitudeGain, frequencyGain);
        yield return new WaitForSeconds(shakeDuration);     
    }

    public void endShake()
    {
        Noise(0, 0);
    }

    void Noise(float amplitude, float frequency)
    {
        Debug.Log("noise");
        Ccamera.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        Ccamera.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        Ccamera.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;

        Ccamera.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
        Ccamera.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
        Ccamera.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;

    }
}
