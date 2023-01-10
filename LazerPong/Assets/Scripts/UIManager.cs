using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region Public
    public static UIManager instance;
    public delegate void Debug();
    #endregion
    public Debug debug;
    #region Serialize
    [SerializeField] private GameObject StartGamePanel;
    [SerializeField] private GameObject MainGamePanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TMP_Text currentScoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private GameObject EffectForScore;
    [SerializeField] private int currentScore ;
    [SerializeField] private int highScore;
    [SerializeField] private int scoreValue ;
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
        LoadHighScore();
        UpdateHighScoreText();
    }

    public void TapToStartButton()
    {
        
        SetPanelActiveOrDeactive(StartGamePanel, false);
        SetPanelActiveOrDeactive(MainGamePanel, true);
        //acces to GameManager and activate game.
        GameManager.instance.isGameActive = true;
       
       
      
    }

    public void AddScore()
    {
        //add score to current score,
        currentScore += scoreValue;
        //if current score biger than high score , then current score is our new high score.
        if (currentScore > highScore)
        {
            highScore = currentScore;
        }

        

        SaveHighScore();
        UpdateScoreText();
        AnimateScore();
    }

    public void AnimateScore()
    {

        //if we had score , activate effect (it seems like ball in fire)
        SetPanelActiveOrDeactive(EffectForScore, true);
        //wait 0.5 seconds and deactivate effect(particle effect in ball)
        StartCoroutine(WaitAndDeactivateEffect(0.5f));


    }

    private IEnumerator WaitAndDeactivateEffect(float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
        SetPanelActiveOrDeactive(EffectForScore, false);
    }


    public void UpdateScoreText()
    {
        //update score text.
        currentScoreText.text = currentScore.ToString();
    }
    
    public void LoseGame()
    {
        //if we lost the game ,reach game Manager and deactivate game, make the gameOverPanel appear.
        GameManager.instance.isGameActive = false;
        SetPanelActiveOrDeactive(GameOverPanel, true);
    }

    public void RestartGame()
    {
        //load everything again.
        SceneManager.LoadScene("MainScene");
      

    }

    void SetPanelActiveOrDeactive(GameObject panel, bool activeOrDeactive )
    {
        //method for activate or deactivate panels.
        panel.SetActive(activeOrDeactive);
    }

   

    void LoadHighScore()
    {
        //if we don't have high score , set high score values 0.
        if (!PlayerPrefs.HasKey("highScore"))
        {
            PlayerPrefs.SetInt("highScore", 0);
        }
        else
        {
            //if we have high score values then , take it and make sure it's equals to our high score variable.
            highScore = PlayerPrefs.GetInt("highScore");
        }
       
    }
    
    void SaveHighScore()
    {
        //save high score value to keep in memory.
        PlayerPrefs.SetInt("highScore", highScore);
        UpdateHighScoreText();
    }

    void UpdateHighScoreText()
    {
        //update high score text.
        highScoreText.text = highScore.ToString();
    }
}
