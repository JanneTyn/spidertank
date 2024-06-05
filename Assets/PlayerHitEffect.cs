using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHitEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public Image dmgEffect;
    public float dmgEffectFadeTime = 1.0f;
    public float dmgEffectStayTime = 0.5f;
    
    public void PlayerDamagedEffect()
    {
        StopAllCoroutines();
        dmgEffect.gameObject.SetActive(true);
        StartCoroutine(Fade());
    }

    public IEnumerator Fade()
    {       
        float elapsedTime = 0;
        dmgEffect.color = new Color(1f, 1f, 1f, 1f);
        while (elapsedTime < dmgEffectStayTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0;
        while (elapsedTime < dmgEffectFadeTime)
        {         
            float transparency = dmgEffectFadeTime - elapsedTime;
            dmgEffect.color = new Color(1f, 1f, 1f, transparency);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        dmgEffect.gameObject.SetActive(false);
    }
}
