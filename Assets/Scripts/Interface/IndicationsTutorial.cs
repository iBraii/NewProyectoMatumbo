using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class IndicationsTutorial : MonoBehaviour
{
    public GameObject obj_indicationSign;
    public GameObject player;
    public GameObject thirdPersonCamera;
    public GameObject DreamCatcherPresentation;
    public GameObject DarkAreaPresentation;
    public GameObject DarkArea;
    public GameObject panelsInd;
    public Transform box;
    public Transform blanket;
    private PlayerM sc_playerM;
    private PlayerC sc_playerC;
    public Text indicationText;
    public Animator obj_indicationAm;
    public GameObject enemy;
    public bool learningMovement, learningPush, learningDreamCatcher, cameraFollowenemy,cameraFollowDC, learningHide,hideLearned,dreamCatcherHint,hasLearnedDreamCatcher,learningButton,buttonLearned,
        learningDark;
    private bool hasLearnedPush;
    private bool cameraDarkArea;
    public GameObject newCameraPosition;
    public Camera mainC;
    public float journeyLenght;
    public float startTime;
    public float timeToReturnCamera,buttonIndicationTimer;
    private Vector3 cameraInitialPosition;
    private GameObject obj_dreamCatcher;
    [SerializeField]
    private float moveTimerSign, enemiesDCHint;
    void Start()
    {
        player = GameObject.Find("Player");
        thirdPersonCamera = GameObject.Find("ThirdPersonCamera");
        sc_playerM = player.GetComponent<PlayerM>();
        sc_playerC = player.GetComponent<PlayerC>();
        //obj_indicationSign = GameObject.Find("IndicationOBJ");
        obj_indicationAm = obj_indicationSign.GetComponent<Animator>();
        Invoke("LearnMovement", 2);
        newCameraPosition = GameObject.Find("EnemyFollowOBJ");
        obj_dreamCatcher = GameObject.Find("DreamCatcher");
        sc_playerM.MovementLearned = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Time.time > 1)
        {
            moveTimerSign += Time.deltaTime;
            LearnMovement();
        }

        LearnPush();

        if (Vector3.Distance(player.transform.position, box.position) < 3 && hasLearnedPush == false)
        {
            learningPush = true;
        }
        
        LearnDreamCatcher();
        if (player.GetComponent<PlayerM>().hasWeapon&&!hasLearnedDreamCatcher)
        {
            learningDreamCatcher=true;
        }
        CameraFollowEnemy();

        if (Vector3.Distance(player.transform.position, blanket.position) < 5 && !hideLearned)
        {
            learningHide = true;
        }
        LearnToHide();
        LearnDreamCatcherHint();
        LearnButton();
        LearnDark();
        CameraFollowDC();
        CameraShowDarkArea();
    }

    public void LearnMovement()
    {
        if (learningMovement)
        {
            obj_indicationSign.SetActive(true);
            indicationText.text = "USE THE MOUSE TO CONTROL THE CAMERA AND PRESS 'W', 'A', 'S', 'D' KEYS TO MOVE AROUND";
            if((Input.GetKeyDown(sc_playerC.keyUp) || Input.GetKeyDown(sc_playerC.keyRight) ||
                Input.GetKeyDown(sc_playerC.keyLeft) || Input.GetKeyDown(sc_playerC.keyDown)) && moveTimerSign >= 2.3f)
            {
                
                obj_indicationAm.SetBool("Deactivate", true);
                Invoke("DeactivateSign", 1);
                learningMovement = false;
            }
        }
        
    }

    public void DeactivateSign()
    {
        indicationText.text = "";
        obj_indicationAm.SetBool("Deactivate", false);
        obj_indicationSign.SetActive(false);
        
    }
    public void LearnPush()
    {
        if (learningPush)
        {
            obj_indicationSign.SetActive(true);
            indicationText.text = "HOLD 'E' NEAR TO AN INTERACTABLE BOX AND MOVE FORWARD/BACKWARD TO PUSH/PULL";
            if (Vector3.Distance(player.transform.position,box.position) >= 3)
            {
                obj_indicationAm.SetBool("Deactivate", true);
                Invoke("DeactivateSign", 1);
            }
            if(sc_playerM.isMovingBox)
            {
                
                obj_indicationAm.SetBool("Deactivate", true);
                Invoke("DeactivateSign", 1);
                learningPush = false;
                hasLearnedPush = true;
            }
        }   
    }

    public void LearnDreamCatcher()
    {
        if (learningDreamCatcher)
        {
            obj_indicationSign.SetActive(true);
            indicationText.text = "HOLD 'Q' TO TEMPORARILY STUN ENEMIES. THE LONGER YOU HOLD, THE LONGER THEY WILL BE STUNNED";
            if (sc_playerM.isUsingWeapon)
            {  
                obj_indicationAm.SetBool("Deactivate", true);
                Invoke("DeactivateSign", 1);
                hasLearnedDreamCatcher = true;
                learningDreamCatcher = false;
            }
        }
    }

    public void ActivateEnemy()
    {
        Debug.Log("SpawningEnemy");
        cameraInitialPosition = mainC.transform.position;
        cameraFollowenemy = true;
        enemy.SetActive(true);
        newCameraPosition = GameObject.Find("EnemyFollowOBJ");
        sc_playerM.isHiding=true;
    }

    public void CameraFollowEnemy()
    {
        if (cameraFollowenemy)
        {
            Quaternion rotation = Quaternion.LookRotation(enemy.transform.position - mainC.transform.position);
            mainC.transform.rotation = Quaternion.Lerp(mainC.transform.rotation, rotation, 100 * Time.deltaTime);
            timeToReturnCamera += Time.deltaTime;
            thirdPersonCamera.SetActive(false);
            float distCovered = (timeToReturnCamera - startTime) * 3;
            mainC.transform.position = Vector3.Lerp(cameraInitialPosition, newCameraPosition.transform.position, distCovered / journeyLenght);
            Debug.Log(distCovered);
            
            
            if (timeToReturnCamera >= 5f)
            {
                timeToReturnCamera = 0;
                cameraFollowenemy = false;
                thirdPersonCamera.SetActive(true);
                sc_playerM.isHiding = false;
            }
        }
        
    }
    public void ActivateDCP()
    {
        cameraInitialPosition = mainC.transform.position;
        cameraFollowDC = true;    
        sc_playerM.isHiding = true;
        newCameraPosition = DreamCatcherPresentation;
    }
    public void CameraFollowDC()
    {
        if (cameraFollowDC)
        {
            mainC.transform.LookAt(obj_dreamCatcher.transform);
            timeToReturnCamera += Time.deltaTime;
            thirdPersonCamera.SetActive(false);
            float distCovered = (timeToReturnCamera - startTime) * 1;
            mainC.transform.position = Vector3.Lerp(cameraInitialPosition, DreamCatcherPresentation.transform.position, distCovered / journeyLenght);
            Debug.Log(distCovered);

            if (timeToReturnCamera >= 5f)
            {
                timeToReturnCamera = 0;
                cameraFollowDC = false;
                thirdPersonCamera.SetActive(true);
                sc_playerM.isHiding = false;
            }
        }
        
    }
    public void ActivateDarkArea()
    {
        cameraInitialPosition = mainC.transform.position;
        cameraDarkArea = true;
        sc_playerM.isHiding = true;
        newCameraPosition = DarkAreaPresentation;
    }
    public void CameraShowDarkArea()
    {
        if (cameraDarkArea)
        {
            mainC.transform.LookAt(DarkArea.transform);
            timeToReturnCamera += Time.deltaTime;
            thirdPersonCamera.SetActive(false);
            float distCovered = (timeToReturnCamera - startTime) * 1;
            mainC.transform.position = Vector3.Lerp(cameraInitialPosition, DarkAreaPresentation.transform.position, distCovered / journeyLenght);
            Debug.Log(distCovered);

            if (timeToReturnCamera >= 5f)
            {
                timeToReturnCamera = 0;
                cameraDarkArea = false;
                thirdPersonCamera.SetActive(true);
                sc_playerM.isHiding = false;
            }
        }
    }
    public void LearnToHide()
    {
        if (learningHide)
        {
            obj_indicationSign.SetActive(true);
            indicationText.text = "PRESS 'E' NEAR TO A BLANKET TO HIDE IN IT";
            if (Vector3.Distance(player.transform.position, blanket.position) >= 5)
            {
                obj_indicationAm.SetBool("Deactivate", true);
                Invoke("DeactivateSign", 1);
            }
            if(sc_playerM.isHiding)
            {
                obj_indicationAm.SetBool("Deactivate", true);
                Invoke("DeactivateSign", 1);
                learningHide = false;
                hideLearned = true;
            }
        }
    }

    public void LearnDreamCatcherHint()
    {
        if (dreamCatcherHint)
        {
            obj_indicationSign.SetActive(true);
            //panelsInd.SetActive(true);
            enemiesDCHint += Time.deltaTime;
            indicationText.text = "NIGHMARES WILL FILL UP YOUR STRESS BAR. DO NOT LET YOUR STRESS BUILD";
            if (enemiesDCHint >= 3)
            {
                
                obj_indicationAm.SetBool("Deactivate", true);
                Invoke("DeactivateSign", 1);
                //panelsInd.SetActive(false);
                dreamCatcherHint = false;
            }
        }
    }

    public void LearnButton()
    {
        if (learningButton)
        {
            buttonIndicationTimer += Time.deltaTime;
            obj_indicationSign.SetActive(true);
            indicationText.text = "USE AN INTERACTABLE BOX TO ACTIVATE BUTTONS";
            if (buttonIndicationTimer>=3f)
            {
                buttonIndicationTimer = 0;
                learningButton = false;
                buttonLearned = true;
                obj_indicationAm.SetBool("Deactivate", true);
                Invoke("DeactivateSign", 1);
            }
        }
    }

    public void LearnDark()
    {
        if (learningDark)
        {
            buttonIndicationTimer += Time.deltaTime;
            obj_indicationSign.SetActive(true);
            indicationText.text = "DARK AREAS WILL GREATLY INCREASE YOUR STRESS BAR";
            if (buttonIndicationTimer >= 3f)
            {
                learningDark = false;
                buttonIndicationTimer = 0;
                obj_indicationAm.SetBool("Deactivate", true);
                Invoke("DeactivateSign", 1);
            }
        }
    }



}
