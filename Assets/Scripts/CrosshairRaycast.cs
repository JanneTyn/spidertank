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
    RaycastHit hit;
    int layerMask = 1 << 7;
    Vector3 pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);


    void Start()
    {
        //cam = GetComponent<Camera>();
        dmgUI = new Vector3(500, 300, 0);
    }

    void Update()
    {
        ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        Color col = Color.red;
        
        if (Physics.Raycast(ray, out hit, 100, layerMask)) col = Color.green;
        Debug.DrawRay(ray.origin, ray.direction * 100, col);
    }

    public GameObject checkEnemyRaycast()
    {
        if (Physics.Raycast(ray, out hit, 100, layerMask)) 
        {
            return hit.collider.gameObject;
        } 
        else return null;
    }

    public void createDamageMarker(float damage)
    {
        GameObject dmgMarker = Instantiate(dmgMarkerPrefab, dmgUI, Quaternion.identity);
        
        dmgMarker.transform.SetParent(crosshairCanvas.transform);
        var rect = dmgMarker.GetComponent<RectTransform>();
        rect.localPosition = new Vector3(40, 20, 0);       
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
