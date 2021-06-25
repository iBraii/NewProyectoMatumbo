using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIndications : MonoBehaviour
{
    public GameObject thirdPersonCamera;
    public PlayerM player;
    public Camera mainC;
    public float timeToReturnCamera;
    
    public bool isLerping = false;
  
    

    [Header("LerpSettings")]
    public Transform lerpPosition;
    public float startTime;
    public float journeyLenght;
    public float totalTime;
    public float rotationSpeed;
    public Vector3 cameraPosition;

    void Start()
    {
        thirdPersonCamera = GameObject.Find("ThirdPersonCamera");
        player = GameObject.Find("Player").GetComponent<PlayerM>();
        mainC = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLerping)
        {
            LerpToPosition(cameraPosition, lerpPosition, startTime, journeyLenght, totalTime, rotationSpeed);
        }
        
    }
    public void LerpToPosition(Vector3 cameraInitialPosition, Transform positionToLerp,float startTime,
        float journeyLenght,float totalTime, float rotationSpeed)
    {
        
            player.canMove = false;
            thirdPersonCamera.SetActive(false);
            timeToReturnCamera += Time.deltaTime;
            mainC.transform.rotation = Quaternion.RotateTowards(mainC.transform.rotation, positionToLerp.rotation, rotationSpeed * Time.deltaTime);
            float distCovered = (timeToReturnCamera - startTime) * 1f;
            mainC.transform.position = Vector3.Lerp(cameraInitialPosition, positionToLerp.position, distCovered / journeyLenght);
            if (timeToReturnCamera >= totalTime)
            {
                timeToReturnCamera = 0;
                isLerping = false;
                thirdPersonCamera.SetActive(true);
                player.canMove = true;
                this.gameObject.SetActive(false);
            }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cameraPosition = mainC.transform.position;
            isLerping = true;
        }
    }
}
