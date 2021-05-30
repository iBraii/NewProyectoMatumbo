using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Text4 : MonoBehaviour
{
    public Text hide;
    private PlayerM player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerM>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isHiding)
        {
            hide.text = ("Hide: Yes");

        }
        else
        {
            hide.text = ("Hide: No");
        }
    }
}
