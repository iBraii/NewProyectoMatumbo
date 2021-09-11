using System;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    private static PlayerSingleton _instance;
    public static PlayerSingleton Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject("PlayerSingleton");
                go.AddComponent<PlayerSingleton>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        stress = 0;
        maxEnergy = 3;
        energy = maxEnergy;
        isHiding = false;
    }
    private void Start()
    {
        canRotate = true;
    }
    public bool isGrounded { get; set; }
    public bool isHiding { get; set; }
    public bool canRotate { get; set; }
    public float stress { get; set; }
    public float energy { get; set; }
    public float maxEnergy { get; set; }
    public bool usingWeap { get; set; }
}

