using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameActive = false;
    public GameStates state;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }


    }

    private void Start()
    {
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
           
            case GameStates.STARTED:
                Time.timeScale = 1;
                break;
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
            SetState(GameStates.STARTED);
        }
        else
        {
            SetState(GameStates.OVER);
        }

        UpdateGameState();
    }

   
}
