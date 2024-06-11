using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem dust;
    public ParticleSystem blood1;
    public ParticleSystem blood2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreateDust()
    {
        if (!dust.isPlaying) 
        {
            dust.Play();
        }
    }

    public void PauseDust()
    {
        dust.Stop();
    }

    public void Bleed()
    {
        blood1.Play();
        blood2.Play();
    }
    public void BleedSingleSpot()
    {
        blood1.Play();
    }

}
