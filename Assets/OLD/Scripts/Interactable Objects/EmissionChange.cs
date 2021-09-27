using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionChange : MonoBehaviour
{
    public Material mat1;
    public Color color;
    private float intensity;
    public float vel;

    void Update()
    {
        if(mat1 == null)
        {
            Debug.LogWarning("Asignale material putito");
            return;
        }
        intensity = Mathf.PingPong(Time.time / vel, .5f);
        mat1.SetColor("_EmissionColor", color * intensity);
    }
}
