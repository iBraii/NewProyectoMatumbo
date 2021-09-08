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
    }
    private void Start()
    {
        canRotate = true;
    }
    public bool isGrounded { get; set; }
    public bool isHiding { get; set; }
    public bool canRotate { get; set; }
    public float stress { get; set; }
}

