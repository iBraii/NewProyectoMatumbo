using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class ActualComic : MonoBehaviour
{
    //ComicVars
    public Sprite[] page;    
    public Image comic;
    public int numeroDecomic;
    int comicIndex = 0;
    //Animator
    private Animator am;
    private AudioSource audioSource;
    void Start()
    {
        //audioSource.Play();
        am = GetComponent<Animator>();
        am.SetInteger("numeroDeComic", numeroDecomic);
        comic.sprite = page[comicIndex];
    }


    public void ChangePage()
    {
        comicIndex++;
        comic.sprite = page[comicIndex];
    }
    
}
