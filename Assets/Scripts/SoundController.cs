using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    bool MG_active;
    AudioSource audioSource;

    public AudioClip MG_active_sound;
    public AudioClip MG_finished_sound;

    SoundController sound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MG_Sound_On()
    {
        sound.MG_active = true;
        Debug.Log(MG_active + "soundon");
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(MG_active_sound);

            Debug.Log(MG_active + "soundon loop");
        }
    }
    public void MG_Sound_Off()
    {
        Debug.Log(MG_active + "soundoff");
        audioSource.Stop();
        if (MG_active == true)
        {
            Debug.Log(MG_active + "soundoff loop");
            audioSource.PlayOneShot(MG_finished_sound);
            MG_active = false;
        }
    }
}
