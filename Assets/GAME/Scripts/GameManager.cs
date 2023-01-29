using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Public
    public static GameManager instance;
    public bool isGameActive = false;
    public GameStates state;
    #endregion

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }


    }

    private void Start()
    {
        //set frame rate to run this game correctly on every device.
        Application.targetFrameRate = 60;
    }

   

    public enum GameStates
    {
       
        STARTED,
        OVER
    }



    public void SetState(GameStates newState) => state = newState;
   

    public GameStates GetState() 
    {
        return state;
    }

  

    public void UpdateGameState()
    {
        switch (GetState())
        {
           //if game state is started, then start the game.
            case GameStates.STARTED:
                Time.timeScale = 1;
                break;
            //if game state is over , then stop the game. (nothing can move)
            case GameStates.OVER:
                Time.timeScale = 0;
                break;
            default:
                //bla bla
                break;
        }
    }

    private void Update()
    {
        if (isGameActive)
        {
            //if game is active so our game state will be started.
            SetState(GameStates.STARTED);
        }
        else
        {
            //if game is not active so our game state will be over.
            SetState(GameStates.OVER);
        }

        UpdateGameState();
    }

   
}
