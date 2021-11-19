using UnityEngine;
using GameJolt.API;
using GameJolt.UI;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameJoltManager : MonoBehaviour
{
	public void SignInButtonClicked()
	{
		GameJoltUI.Instance.ShowSignIn(success => {
			GameJoltUI.Instance.QueueNotification(success ? "Welcome, dreamer." : "Window closed");
		});
	}
	public void UnlockTrophy(string trophyID)
	{
		Debug.Log("Unlocked Trophy.");

		var trophyId = trophyID != string.Empty ? int.Parse(trophyID) : 0;
		Trophies.Unlock(trophyId, success => {
			Debug.LogFormat("Unlock Trophy {0}.", success ? "Successful" : "Failed");
		});
	}

	public void TryUnlockTrophy(string trophyID)
	{
		Debug.Log("Trying Unlock Trophy");

		var trophyId = trophyID != string.Empty ? int.Parse(trophyID) : 0;
		Trophies.TryUnlock(trophyId, success => {
			Debug.LogFormat("Try Unlock Trophy {0}.", success);
		});
	}
}