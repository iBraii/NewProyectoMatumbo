using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionChange : MonoBehaviour
{
    public Material mat1;
    public Color color;
    public float intensity;
    // Start is called before the first frame update
    void Start()
    {
        mat1 = GetComponent<Material>();
        color = mat1.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        intensity = Mathf.PingPong(Time.time / 4, .3f);
        mat1.SetColor("_EmissionColor", color * intensity);
    }
}
