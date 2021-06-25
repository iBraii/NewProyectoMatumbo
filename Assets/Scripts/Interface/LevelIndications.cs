using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIndications : MonoBehaviour
{
    public GameObject thirdPersonCamera;
    public PlayerM player;
    public Camera mainC;
    public float timeToReturnCamera;
    public Transform[] lerpPosition;
    public bool isLerping = false;
  
    public Vector3 cameraPosition;
    void Start()
    {
        thirdPersonCamera = GameObject.Find("ThirdPersonCamera");
        player = GameObject.Find("Player").GetComponent<PlayerM>();
        mainC = Camera.main;
        isLerping = true;
        cameraPosition = mainC.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        LerpToPosition(cameraPosition,lerpPosition[0], 0, 3, 4,100);
    }
    public void LerpToPosition(Vector3 cameraInitialPosition, Transform positionToLerp,float startTime,
        float journeyLenght,float totalTime, float rotationSpeed)
    {
        if (isLerping)
        {
            player.canMove = false;
            thirdPersonCamera.SetActive(false);
            timeToReturnCamera += Time.deltaTime;
            mainC.transform.rotation = Quaternion.RotateTowards(mainC.transform.rotation, rotTarget, rotationSpeed * Time.deltaTime);
            float distCovered = (timeToReturnCamera - startTime) * 1f;
            mainC.transform.position = Vector3.Lerp(cameraInitialPosition, positionToLerp.position, distCovered / journeyLenght);
            if (timeToReturnCamera >= totalTime)
            {
                timeToReturnCamera = 0;
                isLerping = false;
                thirdPersonCamera.SetActive(true);
                player.canMove = true;
            }
        }
        
    }
}
