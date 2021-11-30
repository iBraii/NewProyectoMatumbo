using UnityEngine;
using GameJolt.API;

public class GameJoltTrophies : MonoBehaviour
{
    private void Awake() => DontDestroyOnLoad(gameObject);

    public void TryUnlockTrophy(string trophyID)
	{
		Debug.Log("Trying Unlock Trophy");

		var trophyId = trophyID != string.Empty ? int.Parse(trophyID) : 0;
		Trophies.TryUnlock(trophyId, success => {
			Debug.LogFormat("Try Unlock Trophy {0}.", success);
		});
	}

	public void CompareTrophies()
    {
		if (!GameJoltAPI.Instance.HasUser) return;

		//BRONZE======================================================================
		if (SaveSystem.data.levelCompleted[0]) TryUnlockTrophy("152608");
		if (SaveSystem.data.levelCompleted[1]) TryUnlockTrophy("152609");
		if (SaveSystem.data.levelCompleted[2]) TryUnlockTrophy("152610");
		if (SaveSystem.data.levelCompleted[3]) TryUnlockTrophy("152611");
		if (SaveSystem.data.levelCompleted[4]) TryUnlockTrophy("152612");
		if (SaveSystem.data.levelCompleted[5]) TryUnlockTrophy("152613");

		//SILVER======================================================================
		int count0 = 0;
		for (int i = 0; i < SaveSystem.data.levelCompleted.Length; i++)
			if (SaveSystem.data.levelCompleted[i]) count0++;
		if (count0 == SaveSystem.data.levelCompleted.Length) 
			TryUnlockTrophy("152614");

		if (SaveSystem.data.achievementCompleted[0]) TryUnlockTrophy("152615");
		if (SaveSystem.data.achievementCompleted[1]) TryUnlockTrophy("152616");
		if (SaveSystem.data.achievementCompleted[2]) TryUnlockTrophy("152618");
		if (SaveSystem.data.achievementCompleted[3]) TryUnlockTrophy("152617");
		if (SaveSystem.data.achievementCompleted[4]) TryUnlockTrophy("152619");
		if (SaveSystem.data.achievementCompleted[5]) TryUnlockTrophy("152620");

		//GOLD========================================================================
		int count1 = 0;
		for (int i = 0; i < SaveSystem.data.achievementCompleted.Length; i++)
			if (SaveSystem.data.achievementCompleted[i]) count1++;
		if (count1 == SaveSystem.data.achievementCompleted.Length) 
			TryUnlockTrophy("152621");


		int count2 = 0;
		for (int i = 0; i < SaveSystem.data.levelCompletedNoDmg.Length; i++)
			if (SaveSystem.data.levelCompletedNoDmg[i]) count2++;
		if (count2 == SaveSystem.data.levelCompletedNoDmg.Length) 
			TryUnlockTrophy("152622");

		//PLATINUM===================================================================
		if (count0 == SaveSystem.data.levelCompleted.Length && count1 == SaveSystem.data.achievementCompleted.Length &&
			count2 == SaveSystem.data.levelCompletedNoDmg.Length)
			TryUnlockTrophy("152623");
	}
}