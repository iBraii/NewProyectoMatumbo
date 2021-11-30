using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChange : MonoBehaviour
{
    [SerializeField] private Material[] mats;
    private MeshRenderer currentRender;
    [SerializeField] private Button btn;

    private void Start() => currentRender.material = mats[0];
    private void Awake()
    {
        btn = GetComponentInParent<Button>();
        currentRender = GetComponent<MeshRenderer>();
    }
    public void ButtonColor()
    {
        if(mats == null || btn == null) { Debug.LogWarning("No hay mat o btn"); return; }

        if(btn.buttonToggle) currentRender.material = mats[1];
        else currentRender.material = mats[0];
    }
}
