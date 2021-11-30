using UnityEngine;
using GameJolt.API;
using GameJolt.UI;
using System.Collections;
using DG.Tweening;
using TMPro;

public class GameJoltSignIn : MonoBehaviour
{
	[SerializeField] private GameObject warningPanel;
	[SerializeField] private GameObject warningPopUp;
	[SerializeField] private TextMeshPro text;
	private static bool isFirstTime;

	private void Start()
	{
		FindObjectOfType<GameOptions>().DeactivateOtherFunctions();
		if(!isFirstTime) StartCoroutine(SignIn());
		isFirstTime = true;
	}

    private void Update() => ChangeText(0);

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
		if (GameJoltAPI.Instance.HasUser)
		{
			FindObjectOfType<GameJoltTrophies>().CompareTrophies();
			yield break;
		}
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

	public void GameJoltSignInOutButtonClicked()
	{
		if (!GameJoltAPI.Instance.HasUser) SignInButtonClicked();
		else
		{
			GameJoltAPI.Instance.CurrentUser.SignOut();
			GameJoltUI.Instance.QueueNotification("Logged Out");
		}
	}
}