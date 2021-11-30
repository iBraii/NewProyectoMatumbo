using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mision5Block : MonoBehaviour
{
    private Mision5 parent;
    private bool active = true;
    private bool playerClose = false;
    void Start()
    {
        parent = GetComponentInParent<Mision5>();
        transform.DOMoveY(transform.position.y + .1f, 10, false).SetEase(Ease.InOutBack).SetLoops(-1,LoopType.Yoyo);
        transform.DORotate(new Vector3(0, 360, 0), 10, RotateMode.WorldAxisAdd).SetLoops(-1,LoopType.Yoyo);
    }
    private void Update()
    {
        PlayerInteraction();
    }
    private void PlayerInteraction()
    {
        if (SaveSystem.data.achievementCompleted[parent.interact.collectableIndex]) return;
        if (parent.input.actions["Interact"].triggered && playerClose && active)
        {
            active = false;
            parent.interact.text.DOFade(0, 1);
            SoundManager.instance.Play("Confirmation");
            GetComponent<Collider>().enabled = false;
            gameObject.SetActive(false);        
            parent.currentInteractions++;
            Disable();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerClose = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerClose = false;
        }
    }
    private void Disable() => gameObject.SetActive(false);
}
