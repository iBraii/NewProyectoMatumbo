using UnityEngine;
using UnityEngine.InputSystem;

public class CheatsCode : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction noClip, inmortality, infiniteDC, speed;

    [HideInInspector]
    public bool nc, inmo, infi, spd;
    public Collider[] noClipCols;

    public GameObject cheatCodeTxt;
    float timer;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        noClip = playerInput.actions["NoClip"];
        inmortality = playerInput.actions["Inmortality"];
        infiniteDC = playerInput.actions["InfiniteDC"];
        speed = playerInput.actions["Speed"];

        noClip.performed += PerformNoClip;
        inmortality.performed += PerformInmortality;
        infiniteDC.performed += PerformDC;
        speed.performed += PerformSPD;
    }
    private void OnDisable()
    {
        noClip.performed -= PerformNoClip;
        inmortality.performed -= PerformInmortality;
        infiniteDC.performed -= PerformDC;
        speed.performed -= PerformSPD;
    }
    private void Update()
    {
        if (inmo)
            PlayerSingleton.Instance.stress = 0f;
        if (infi)
            PlayerSingleton.Instance.dreamEnergy = 3f;

        #region nulls
        if (cheatCodeTxt == null)
        {
            Debug.LogWarning("no hay texto cheatcode");
            return;
        }
        #endregion
        if (cheatCodeTxt.activeInHierarchy)
            timer += Time.deltaTime;
        if (timer >= 5f)
        {
            cheatCodeTxt.SetActive(false);
            timer = 0;
        }      
    }
    void PerformNoClip(InputAction.CallbackContext ctx)
    {
        #region nulls
        if (cheatCodeTxt == null)
        {
            Debug.LogWarning("no hay texto cheatcode");
            return;
        }
        #endregion
        cheatCodeTxt.SetActive(true);
        nc = !nc;
        foreach(Collider col in noClipCols)
        {
            if(col.GetComponent<Collider>() == null)
            {
                Debug.Log("Algun objeto no tiene collider");
                return;
            }
            col.GetComponent<Collider>().enabled = !col.GetComponent<Collider>().enabled;
        }
    }
    void PerformInmortality(InputAction.CallbackContext ctx)
    {
        #region nulls
        if (cheatCodeTxt == null)
        {
            Debug.LogWarning("no hay texto cheatcode");
            return;
        }
        #endregion
        cheatCodeTxt.SetActive(true);
        inmo = !inmo;
    }
    void PerformDC(InputAction.CallbackContext ctx)
    {
        #region nulls
        if (cheatCodeTxt == null)
        {
            Debug.LogWarning("no hay texto cheatcode");
            return;
        }
        #endregion
        cheatCodeTxt.SetActive(true);
        infi = !infi;
    }
    void PerformSPD(InputAction.CallbackContext ctx)
    {
        #region nulls
        if (cheatCodeTxt == null)
        {
            Debug.LogWarning("no hay texto cheatcode");
            return;
        }
        #endregion
        cheatCodeTxt.SetActive(true);
        spd = !spd;
        if (spd)
            gameObject.GetComponent<PlayerMovement>().movementSpeed *= 3;
        else
            gameObject.GetComponent<PlayerMovement>().movementSpeed = 0.75f;     
    }
}
