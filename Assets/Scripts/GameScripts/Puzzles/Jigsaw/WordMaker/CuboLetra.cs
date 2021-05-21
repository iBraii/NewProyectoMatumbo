using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboLetra : MonoBehaviour
{
    public float tiltAngle;
    public float[] randomTiltAngle = new float[4];
    public int x;
    public Quaternion target;
    public float smooth;
    public Quaternion pos_correctRot;
    public GameObject obj_wordMaker;
    private WordMakerM sc_wordMakerM;
    public bool isCorrect;
    // Start is called before the first frame update
    void Start()
    {
        sc_wordMakerM = obj_wordMaker.GetComponent<WordMakerM>();
        pos_correctRot = transform.localRotation;
        //pos_correctRot.rotation = transform.localRotation;
        isCorrect = false;
        randomTiltAngle[0] = 0;
        randomTiltAngle[1] = 90;
        randomTiltAngle[2] = 180;
        randomTiltAngle[3] = 270;
        x = Random.Range(0, 3);
        tiltAngle = randomTiltAngle[x];
        transform.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
    }

    private void OnMouseDown()
    {
        transform.localRotation *= Quaternion.Euler(-90,0,0);
    }
    private void Update()
    {
        if (transform.localRotation == pos_correctRot)
        {
            if(!isCorrect)
            {
                sc_wordMakerM.correctPiecesCounter++;
                isCorrect = true;
            }   
        }
    }
}
