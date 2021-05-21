using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordMakerV : MonoBehaviour
{
    private WordMakerC sc_wordMakerC;
    // Start is called before the first frame update
    void Start()
    {
        sc_wordMakerC = GetComponent<WordMakerC>();
    }

    // Update is called once per frame
    void Update()
    {
        sc_wordMakerC.PlayerInput();
        sc_wordMakerC.PlayerClose();
        sc_wordMakerC.WordCheckController();
    }
}
