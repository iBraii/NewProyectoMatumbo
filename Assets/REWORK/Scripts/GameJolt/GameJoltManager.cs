using UnityEngine;
using GameJolt.API;
using GameJolt.UI;
using System.Collections;
using DG.Tweening;
using TMPro;

public class GameJoltManager : MonoBehaviour
{
	[SerializeField] private GameObject warningPanel;
	[SerializeField] private GameObject warningPopUp;
	[SerializeField] private TextMeshPro text;

    private void Start() => StartCoroutine(SignIn());

	public void ChangeText(float t) => StartCoroutine(textChange(t));

	private IEnumerator textChange(float t)
    {
		yield return new WaitForSeconds(t);
		if (GameJoltAPI.Instance.HasUser) text.text = "GAMEJOLT LOG OUT";
		else text.text = "GAMEJOLT SIGN IN";
	}

    private IEnumerator SignIn()
	{
		yield return new WaitForSeconds(1.5f);
		if (GameJoltAPI.Instance.HasUser) yield break;
		SignInButtonClicked();
	}

	public void Cancelled()
	{
		warningPopUp.transform.localScale = Vector3.zero;
		warningPanel.SetActive(true);
		warningPopUp.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutBack);
	} 

	public void DeactivatePanel()
    {
		warningPopUp.transform.DOScale(Vector3.zero, 1).SetEase(Ease.InBack);
		StartCoroutine(deactivate());
    }

	private IEnumerator deactivate()
    {
		yield return new WaitUntil(() => warningPopUp.transform.localScale == Vector3.zero);
		warningPanel.SetActive(false);
    }

    public void SignInButtonClicked()
	{
		GameJoltUI.Instance.ShowSignIn(success => {
			GameJoltUI.Instance.QueueNotification(success ? "Welcome, " + GameJoltAPI.Instance.CurrentUser.Name + "." : "Sign in cancelled");
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

	public void GameJoltSignInOutButtonClicked()
	{
		if (!GameJoltAPI.Instance.HasUser)
        {
			SignInButtonClicked();
			ChangeText(0);
		}
		else
		{
			GameJoltAPI.Instance.CurrentUser.SignOut();
			GameJoltUI.Instance.QueueNotification("Logged Out");
			ChangeText(0);
		}
	}
}