using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAFKcheck : MonoBehaviour
{
    // Start is called before the first frame update
    bool afkcheck = false;
    bool afkbool = false;
    public GameObject spawnpoint;
    public GameObject player;
    int layerMaskEnemyPart = 1 << 8;
    void Start()
    {
        spawnpoint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (afkcheck == false)
        {
            afkcheck = true;
            StartCoroutine(AFKwait());
        }
    }

    public IEnumerator AFKwait()
    {
        Vector3 currentPos = this.gameObject.transform.position;
        yield return new WaitForSeconds(5);
        Debug.Log("beforecheck");
        afkbool = false;
        Collider[] hitColliders = (Physics.OverlapSphere(currentPos, 5, layerMaskEnemyPart));

        for (int i = 0; i < hitColliders.Length; i++)
        {
            Debug.Log("hitcollider: " + hitColliders[i].gameObject.name);
            if (hitColliders[i].gameObject == player.gameObject)
            {
                spawnpoint.SetActive(true);
                afkbool = true;
                Debug.Log("afk too long");
            }
            
        }

        if (afkbool == false) 
        {                    
                spawnpoint.SetActive(false);
                Debug.Log("out of afk");          
        }
        
        afkcheck = false;
    }
}
