using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrosshairRaycast : MonoBehaviour
{
    public Camera cam;
    public GameObject dmgMarkerPrefab;
    public Canvas crosshairCanvas;
    private Vector3 dmgUI;
    public TMP_Text textmsh;
    Ray ray;
    Ray machineGunRay;
    Ray shotGunRay;
    RaycastHit hit;
    int layerMask = 1 << 7;
    Vector3 pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    float randomDir = 0;
    List<GameObject> enemiesHit = new List<GameObject>();
    public bool machinegunShooting = false;


    void Start()
    {
        //cam = GetComponent<Camera>();
        dmgUI = new Vector3(500, 300, 0);
    }

    void Update()
    {
        ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        //randomDir = Random.Range(-2f , 2f);
        //ray.direction = new Vector3(ray.direction.x + randomDir, ray.direction.y + randomDir, ray.direction.z + randomDir);
        Color col = Color.red;
        
        if (Physics.Raycast(ray, out hit, 100, layerMask)) col = Color.green;
        Debug.DrawRay(ray.origin, ray.direction * 100, col);
    }

    public GameObject checkEnemyRaycast(float bulletSpread = 0f)
    {
        machineGunRay = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        randomDir = Random.Range(-bulletSpread, bulletSpread);
        machineGunRay.direction = new Vector3(machineGunRay.direction.x + randomDir, machineGunRay.direction.y + randomDir, machineGunRay.direction.z + randomDir);
        Debug.DrawRay(machineGunRay.origin, machineGunRay.direction * 100, Color.magenta);
        if (Physics.Raycast(machineGunRay, out hit, 100, layerMask)) 
        {
            return hit.collider.gameObject;
        } 
        else return null;
    }

    public List<GameObject> checkEnemyRaycastShotGun(float bulletSpread = 0f, int bullets = 6)
    {
        enemiesHit = new List<GameObject>();
        for (int i = 0; i < bullets; i++)
        {
            shotGunRay = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));  
            randomDir = Random.Range(-bulletSpread, bulletSpread);
            shotGunRay.direction = new Vector3(shotGunRay.direction.x + randomDir, shotGunRay.direction.y + randomDir, shotGunRay.direction.z + randomDir);
            Debug.DrawRay(shotGunRay.origin, shotGunRay.direction * 100, Color.magenta);
            if (Physics.Raycast(shotGunRay, out hit, 100, layerMask))
            {
                enemiesHit.Add(hit.collider.gameObject);
            }
        }
        return enemiesHit;
    }

    public void createDamageMarker(float damage, GameObject shotenemy)
    {
        GameObject dmgMarker = Instantiate(dmgMarkerPrefab, dmgUI, Quaternion.identity);
        
        dmgMarker.transform.SetParent(crosshairCanvas.transform);
        var rect = dmgMarker.GetComponent<RectTransform>();
        Vector3 enemypos = cam.transform.InverseTransformPoint(shotenemy.transform.position);
        rect.localPosition = enemypos;
        
        textmsh = dmgMarker.GetComponent<TMP_Text>();
        textmsh.text = damage.ToString();
        dmgMarker.SetActive(true);
        StartCoroutine(SmoothLerp(dmgMarker, 0.5f));
        
    }

    private IEnumerator SmoothLerp(GameObject dmgMark, float time)
    {
        Vector3 startingPos = dmgMark.transform.position;
        Vector3 finalPos = dmgMark.transform.position + (dmgMark.transform.up * 30);

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            dmgMark.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(dmgMark);
    }
}
