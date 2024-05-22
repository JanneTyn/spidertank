using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyTag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Enemy CheckTag(GameObject shotEnemy)
    {
        if (shotEnemy.gameObject.tag == "MeleeEnemy")
        {
            Enemy enemyScript = GetEnemyParentScript(shotEnemy);
            return enemyScript;
        }
        else if  (shotEnemy.gameObject.tag == "RangeEnemy")
        {
            Enemy enemyScript2 = GetEnemyParentScript(shotEnemy);
            return enemyScript2;
        }
        return null;
    }

    Enemy GetEnemyParentScript(GameObject enemyHit)
    {
        Enemy enemyscript = enemyHit.GetComponent<Enemy>();

        if (enemyscript != null)
        {
            return enemyscript;
        }
        else
        {
            if (enemyHit.transform.parent != null)
            {
                enemyHit = enemyHit.transform.parent.gameObject;
                Enemy enemyscript2 = enemyHit.GetComponent<Enemy>();

                if (enemyscript2 != null)
                {
                    return enemyscript2;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

    }

    EnemyRange GetRangeEnemyParentScript(GameObject enemyHit)
    {
        EnemyRange enemyscript = enemyHit.GetComponent<EnemyRange>();

        if (enemyscript != null)
        {
            return enemyscript;
        }
        else
        {
            if (enemyHit.transform.parent != null)
            {
                enemyHit = enemyHit.transform.parent.gameObject;
                EnemyRange enemyscript2 = enemyHit.GetComponent<EnemyRange>();

                if (enemyscript2 != null)
                {
                    return enemyscript2;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

    }
}
