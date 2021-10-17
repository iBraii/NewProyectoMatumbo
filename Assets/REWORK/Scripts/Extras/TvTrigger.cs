using UnityEngine;

public class TvTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject tv;
    [SerializeField] private Material onMat;

    private Material[] mats;
    private bool tvStatus=false;

    void Start()
    {
        mats = tv.GetComponent<Renderer>().materials;
        mats[4] = onMat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (tvStatus == false)
            {
                source.Play();
                tv.GetComponent<Renderer>().materials = mats;
                tvStatus = true;
            }
        }
    }

}
