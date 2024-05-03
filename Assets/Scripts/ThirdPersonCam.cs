using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UIElements;

public class ThirdPersonCam : MonoBehaviour
{
    public GameObject playerScript;
    public CinemachineFreeLook camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //var transposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>();
        //float XRotation = transposer.m_XAxis.Value;
        float XRotation = camera.m_XAxis.Value;
        //Debug.Log("rotation: " + XRotation);

    }
}
