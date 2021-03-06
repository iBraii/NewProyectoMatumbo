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
        #region Default Values
        stress = 0;

        maxLightEnergy = 10;
        lightEnergy = maxLightEnergy;

        maxDreamEnergy = 3;
        dreamEnergy = maxDreamEnergy;

        canUseDreamCatcher = false;
        canJump = true;
        isHiding = false;
        canRotate = true;
        #endregion
    }
    public bool isGrounded { get; set; }
    public bool isHiding { get; set; }
    public bool beingAttacked { get; set; }
    public bool canRotate { get; set; }
    public float stress { get; set; }
    public float lightEnergy { get; set; }
    public float maxLightEnergy { get; set; }
    public float dreamEnergy { get; set; }
    public float maxDreamEnergy { get; set; }
    public float maxSpeed { get; set; }
    public bool usingWeap { get; set; }
    public bool canMove { get; set; }
    public bool canJump { get; set; }
    public bool canUseDreamCatcher { get; set; }
    public bool isMoving { get; set; }
    public bool grabingBox { get; set; }
    public bool onAnimation { get; set; }
}