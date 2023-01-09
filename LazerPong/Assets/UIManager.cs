using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    #region Serialize
    [SerializeField] private GameObject StartGamePanel;
    [SerializeField] private GameObject MainGamePanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TMP_Text currentScoreText;
    
    [SerializeField] private float currentScore ;
    [SerializeField] private float scoreValue ;
    #endregion

    GameObject panel;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }

    /*  private void Awake()
      {
          GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
      }

      private void OnDestroy()
      {
          GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
      }

      private void GameManagerOnGameStateChanged(GameManager.GameState state)
      {
          panel.SetActive(state == GameManager.GameState.GameActive);
      }*/

    public void TapToStartButton()
    {
        // GameManagerOnGameStateChanged(GameManager.GameState.GameActive);
        SetPanelActiveOrDeactive(StartGamePanel, false);
        SetPanelActiveOrDeactive(MainGamePanel, true);
        GameManager.instance.isGameActive = true;
       
       // currentScoreText.text = score.ToString();
    }

    public void AddScore()
    {
        currentScore += scoreValue;
        currentScoreText.text = currentScore.ToString();
    }

    public void LoseGame()
    {
        SetPanelActiveOrDeactive(GameOverPanel, true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
      

    }

    void SetPanelActiveOrDeactive(GameObject panel, bool activeOrDeactive )
    {
        panel.SetActive(activeOrDeactive);
    }

    
}
