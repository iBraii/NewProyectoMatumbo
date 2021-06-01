using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Pieza : MonoBehaviour
{
    public Vector3 pos_correctPosition;
    public bool selected;
    public bool isCorrect;
    public BrainTeaserM sc_brainTeaserM;
    public float distanceToCorrect;
    void Start()
    {
        pos_correctPosition = transform.localPosition;
        transform.localPosition = new Vector3(Random.Range(-10, 10), 0.28f, Random.Range(-7, 8));
        GetComponent<SortingGroup>().sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToCorrect = Vector3.Distance(pos_correctPosition, transform.localPosition);
        CheckCorrectPosition();
        
    }
    void CheckCorrectPosition()
    {
        if (distanceToCorrect <= 0.5)
        {
            if (!selected)
            {
                if (!isCorrect)
                {
                    transform.localPosition = pos_correctPosition;
                    isCorrect = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                    sc_brainTeaserM.correctPieces++;
                }
            }
        }
    }
}
