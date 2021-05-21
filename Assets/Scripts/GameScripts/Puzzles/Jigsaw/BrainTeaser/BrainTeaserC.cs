using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BrainTeaserC : MonoBehaviour
{
    private BrainTeaserM sc_brainTeaserM;

    private void Start()
    {
        sc_brainTeaserM = GetComponent<BrainTeaserM>();
    }
    public void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (sc_brainTeaserM.isActive)
            {
                DeactivatePuzzle();
                
            }
            else if (!sc_brainTeaserM.isActive)
            {
                ActivePuzzle();
                
            }
        }
    }
    void ActivePuzzle()
    {
        if (PlayerClose() && Input.GetKeyDown(KeyCode.E) && !sc_brainTeaserM.isActive)
        {
            sc_brainTeaserM.player.GetComponent<PlayerM>().solvingPuzzle = true;
            sc_brainTeaserM.isActive = true;
            sc_brainTeaserM.thirdPersonCamera.SetActive(false);
            sc_brainTeaserM.mainCamera.transform.position = sc_brainTeaserM.puzzleCameraPosition.transform.position;
            sc_brainTeaserM.mainCamera.transform.LookAt(transform);
            sc_brainTeaserM.player.GetComponent<PlayerM>().canMove = false;
        }
    }

    void DeactivatePuzzle()
    {
        if (sc_brainTeaserM.isActive && Input.GetKeyDown(KeyCode.E))
        {
            sc_brainTeaserM.player.GetComponent<PlayerM>().solvingPuzzle = false;
            sc_brainTeaserM.isActive = false;
            sc_brainTeaserM.thirdPersonCamera.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
            sc_brainTeaserM.player.GetComponent<PlayerM>().canMove = true;
        }
    }

    public bool PlayerClose()
    {
        sc_brainTeaserM.distanceToPlayer = Vector3.Distance(transform.position, sc_brainTeaserM.player.transform.position);
        if (sc_brainTeaserM.distanceToPlayer < sc_brainTeaserM.minDistance)
        {
            sc_brainTeaserM.playerClose = true;
            return true;
        }
        else
        {
            sc_brainTeaserM.playerClose = false;
            return false;
        }
    }

    public void MovePieces()
    {
        if (Input.GetMouseButtonDown(0))
        {
            sc_brainTeaserM.isMakingPuzzle = true;
            Ray ray = sc_brainTeaserM.mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if (hit.transform.CompareTag("Piece"))
                {
                    Debug.Log(hit.collider.name);
                    if (!hit.transform.GetComponent<Pieza>().isCorrect)
                    {
                        sc_brainTeaserM.selectedPiece = hit.transform.gameObject;
                        sc_brainTeaserM.selectedPiece.GetComponent<Pieza>().selected = true;
                        sc_brainTeaserM.selectedPiece.GetComponent<SortingGroup>().sortingOrder = sc_brainTeaserM.layer;
                        sc_brainTeaserM.layer++;
                    }
                }
            }
         
        }
        if (Input.GetMouseButtonUp(0))
        {
            sc_brainTeaserM.isMakingPuzzle = false;
            Cursor.visible = true;
            if (sc_brainTeaserM.selectedPiece != null)
            {
                sc_brainTeaserM.selectedPiece.GetComponent<Pieza>().selected = false;
                sc_brainTeaserM.selectedPiece = null;
            }
        }

        if (sc_brainTeaserM.selectedPiece != null)
        {
            float x = Input.GetAxisRaw("Mouse X") * Time.deltaTime;
            float z = Input.GetAxisRaw("Mouse Y") * Time.deltaTime;
           
            Vector3 pos_pieceTranslation = new Vector3(x, 0f, z).normalized;
            sc_brainTeaserM.selectedPiece.transform.localPosition += pos_pieceTranslation/45;

            if (sc_brainTeaserM.selectedPiece.transform.localPosition.x > 15 || sc_brainTeaserM.selectedPiece.transform.localPosition.x < -15||
                sc_brainTeaserM.selectedPiece.transform.localPosition.z > 7 || sc_brainTeaserM.selectedPiece.transform.localPosition.z < -7)
            {
               sc_brainTeaserM.selectedPiece.transform.localPosition = new Vector3(Random.Range(-15, 15), 0f, Random.Range(-7, 8));
            }
           

           
        }
    }

    public void PuzzlecompletedCheck()
    {
        if (sc_brainTeaserM.correctPieces == 9)
        {
            sc_brainTeaserM.obj_aura.SetActive(false);
        }
    }

    public void DamagePlayer()
    {
        //if (sc_brainTeaserM.playerOnAura)
        //{
        //    sc_brainTeaserM.player.GetComponent<PlayerM>().life += (sc_brainTeaserM.damage * Time.deltaTime)/4;
        //}
    }
}
