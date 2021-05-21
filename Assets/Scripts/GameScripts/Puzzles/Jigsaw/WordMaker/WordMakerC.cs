using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordMakerC : MonoBehaviour
{
    private WordMakerM sc_wordMakerM;
    private CuboLetra sc_cuboLetra;
    // Start is called before the first frame update
    void Start()
    {
        sc_wordMakerM = GetComponent<WordMakerM>();
        sc_cuboLetra = GameObject.Find("CuboLetra").GetComponent<CuboLetra>();
    }

    public void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (sc_wordMakerM.isActive)
            {
                DeactivatePuzzle();
               
            }
            else if (!sc_wordMakerM.isActive)
            {
                ActivePuzzle();
                
            }
        }
    }
    void ActivePuzzle()
    {
        if (PlayerClose()  && !sc_wordMakerM.isActive)
        {
            sc_wordMakerM.player.GetComponent<PlayerM>().solvingPuzzle = true;
            sc_wordMakerM.solvingPuzzle = true;
            sc_wordMakerM.isActive = true;
            sc_wordMakerM.thirdPersonCamera.SetActive(false);
            sc_wordMakerM.mainCamera.transform.position = sc_wordMakerM.puzzleCameraPosition.transform.position;
            sc_wordMakerM.mainCamera.transform.LookAt(transform);
            sc_wordMakerM.player.GetComponent<PlayerM>().canMove = false;

            sc_wordMakerM.playerBody.GetComponent<MeshRenderer>().enabled = false;
            sc_wordMakerM.playerEyes.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void DeactivatePuzzle()
    {
        if (sc_wordMakerM.isActive )
        {
            sc_wordMakerM.player.GetComponent<PlayerM>().solvingPuzzle = false;
            sc_wordMakerM.solvingPuzzle = false;
            sc_wordMakerM.isActive = false;
            sc_wordMakerM.thirdPersonCamera.SetActive(true);
            sc_wordMakerM.player.GetComponent<PlayerM>().canMove = true;
            sc_wordMakerM.playerBody.GetComponent<MeshRenderer>().enabled = true;
            sc_wordMakerM.playerEyes.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    public void CursorController()
    {
        if (sc_wordMakerM.isActive)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public bool PlayerClose()
    {
        sc_wordMakerM.distanceToPlayer = Vector3.Distance(transform.position, sc_wordMakerM.player.transform.position);
        if (sc_wordMakerM.distanceToPlayer < sc_wordMakerM.minDistance)
        {
            sc_wordMakerM.playerClose = true;
            return true;
        }
        else
        {
            sc_wordMakerM.playerClose = true;
            return false;
        }
    }
    public void WordCheckController()
    {
        if (sc_wordMakerM.correctPiecesCounter >= sc_wordMakerM.maxPieces)
        {
            sc_wordMakerM.isCompleted = true; 
        }
    }
}
