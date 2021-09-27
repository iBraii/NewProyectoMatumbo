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

    private void Update()
    {
        
    }
    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.OnMenu:
                break;
            case GameState.OnLvls:
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
    }
}
public enum GameState
{
    OnMenu,
    OnLvls,
    OnAnimatic,
    IsPlaying,
    Victory,
    Defeat,
    Paused
}
