using UnityEngine;
using System.Collections;

public class CheckPoints : MonoBehaviour
{
    //[SerializeField] private Transform[] dynamicObjs;
    [SerializeField] private Transform[] checkPoints;

    //public static Vector3[][] objsPos;
    public /*static */ Vector3[] currentCheckpoint = new Vector3[2];
    //public static int currentCPIndex = -1;

    [SerializeField] private Transform player;

    //private void Start() => StartCoroutine(LoadPositions());
    //public static void ResetStatics()
    //{
    //    objsPos = null;
    //    currentCheckpoint = null;
    //    currentCPIndex = -1;
    //}
    public void SavePositions(int id)
    {
        if (checkPoints == null) return;
        currentCheckpoint[0] = checkPoints[id].position;         // 0 - position
        currentCheckpoint[1] = checkPoints[id].localEulerAngles; // 1 - rotation

        //if (dynamicObjs.Length == 0) return;
        //objsPos = new Vector3[dynamicObjs.Length][];
        //for (int i = 0; i < dynamicObjs.Length; i++)
        //{
        //    objsPos[i] = new Vector3[2];
        //    objsPos[i][0] = dynamicObjs[i].position;         // 0 - position
        //    objsPos[i][1] = dynamicObjs[i].localEulerAngles; // 1 - rotation
        //}
    }

    public  IEnumerator LoadPositions()
    {
        if (currentCheckpoint[0] == Vector3.zero) yield break;
        player.gameObject.GetComponent<CharacterController>().enabled = false;
        player.gameObject.GetComponent<PlayerMovement>().useGravity = false;

        player.position = currentCheckpoint[0];         // 0 - position
        player.localEulerAngles = currentCheckpoint[1]; // 1 - rotation

        yield return new WaitForSeconds(.1f);

        player.gameObject.GetComponent<CharacterController>().enabled = true;
        player.gameObject.GetComponent<PlayerMovement>().useGravity = true;

        //if (dynamicObjs.Length == 0) yield break;
        //for(int i = 0; i < dynamicObjs.Length; i++)
        //{
        //    dynamicObjs[i].position = objsPos[i][0];         // 0 - position
        //    dynamicObjs[i].localEulerAngles = objsPos[i][1]; // 1 - rotation
        //}
    }

    private void OnDrawGizmos()
    {
        foreach (Transform checkpoint in checkPoints)
            Gizmos.DrawWireCube(checkpoint.position, new Vector3(.1f,.3f,.1f));
    }
}