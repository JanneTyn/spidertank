using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonEffect : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    [SerializeField] float fadeSpeed;
    [SerializeField] float widthStart;
    [SerializeField] float widthEnd;
    [SerializeField] Material originalMat;
    Material mat;
    Color fade = new Color(1, 1, 1, 0);
    public void CreateLine(Vector3 endPos)
    {
        mat = new Material(originalMat);

        line.material = mat;

        line.startWidth = widthStart;
        line.endWidth = widthEnd;
        line.SetPosition(0, transform.position);
        line.enabled = true;
        line.SetPosition(1, endPos);
    }

    private void Update()
    {
        if (mat.color.a > 0 && line.enabled)
        {
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a - fadeSpeed * Time.deltaTime);
            //mat.color = Color.Lerp(Color.white, fade, fadeSpeed);
            mat.SetColor("_EmissionColor", Color.Lerp(Color.white, Color.black, fadeSpeed));
        }
        else if (mat.color.a <= 0 && line.enabled)
        {
            line.enabled = false;
        }
    }
}
