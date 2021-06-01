using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainTeaserV : MonoBehaviour
{
    private BrainTeaserC sc_brainTeaserC;
    private BrainTeaserM sc_brainTeaserM;

  
    // Start is called before the first frame update
    void Start()
    {
        sc_brainTeaserC = GetComponent<BrainTeaserC>();
        sc_brainTeaserM = GetComponent<BrainTeaserM>();
    }

    // Update is called once per frame
    void Update()
    {
        sc_brainTeaserC.PlayerInput();
        sc_brainTeaserC.PlayerClose();
        sc_brainTeaserC.MovePieces();
        sc_brainTeaserC.PuzzlecompletedCheck();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sc_brainTeaserM.playerOnAura = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sc_brainTeaserM.playerOnAura = false;
        }
    }
}
