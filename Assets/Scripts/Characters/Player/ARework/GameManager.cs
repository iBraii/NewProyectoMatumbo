using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    public GameState state;
    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.OnMenu:
                break;
            case GameState.OnLvlSelection:
                break;
            case GameState.OnOptions:
                break;
            case GameState.OnRanking:
                break;
            case GameState.OnAnimatic:
                break;
            case GameState.IsPlaying:
                break;
            case GameState.Victory:
                break;
            case GameState.Defeat:
                break;
            case GameState.Paused:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }
}
public enum GameState
{
    OnMenu,
    OnLvlSelection,
    OnOptions,
    OnRanking,
    OnAnimatic,
    IsPlaying,
    Victory,
    Defeat,
    Paused
}
