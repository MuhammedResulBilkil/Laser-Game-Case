using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameActive = false;
    //public GameState state;

    //public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }

    private void Start()
    {
       // UpdateGameState(GameState.GameActive);
    }

   /* public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch(newState)
        {
            case GameState.GameActive:
                HandleGameActive();
                break;
            case GameState.NextLevel:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleGameActive()
    {
        throw new NotImplementedException();
    }

    public enum GameState
    {
        GameActive,
        NextLevel,
        Lose
    }*/

    private void Update()
    {
        if (isGameActive)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
