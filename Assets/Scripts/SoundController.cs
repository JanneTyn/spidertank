using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    bool MG_active;
    
    public AudioSource audioSource;
    public AudioSource MG_active_sound;
    public AudioClip MG_finished_sound;
    public AudioClip Shotgun;
    public AudioClip Cannon;


    // Start is called before the first frame update
    void Start()
    {
      //  audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MG_Sound_On()
    {
        MG_active = true;
    //    Debug.Log(MG_active + "soundon");
        if (!MG_active_sound.isPlaying)
        {
            MG_active_sound.Play();

       //     Debug.Log(MG_active + "soundon loop");
        }
    }

    public void MG_Sound_Off()
    {
      //  Debug.Log(MG_active + "soundoff");
        MG_active_sound.Stop();
        if (MG_active == true)
        {
           // Debug.Log(MG_active + "soundoff loop");
            audioSource.PlayOneShot(MG_finished_sound);
            MG_active = false;
        }
    }

    public void ShotgunSound()
    {
        audioSource.PlayOneShot(Shotgun);
        Debug.Log( "shotgun fired");
    }

    public void CannonSound()
    {
        audioSource.PlayOneShot(Cannon);
    }
}
