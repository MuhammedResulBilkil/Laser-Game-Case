using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public delegate void Debug();
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

   
    private int maxScore_=20;


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
        GameManager.instance.isGameActive = true;
        //GameManager.instance.SetState(GameManager.GameStates.STARTED);
       
      
    }

    public void AddScore()
    {
        AnimateScore();
        currentScore += scoreValue;
        if (currentScore ==  maxScore_)
        {
            NextLevel();
        }
        if (currentScore > highScore)
        {
            highScore = currentScore;
        }

        

        SaveHighScore();
        UpdateScoreText();
        
    }

    public void AnimateScore()
    {
       
       // Vector3 position = EffectForScore.transform.position;
       // Vector3 targetPos = position + Vector3.up*7;
        //EffectForScore.transform.position = Vector3.Lerp(position, targetPos , 2);



        

        StartCoroutine(chr());
        

    }

    private IEnumerator chr()
    {
        yield return new WaitForSeconds(1);
     //   EffectForScore.SetActive(false);
    }

    private void NextLevel()
    {
        
        //BallController.instance.AddForce(5);
        maxScore_ += 20;
    }

    public void UpdateScoreText()
    {
        currentScoreText.text = currentScore.ToString();
    }
    
    public void LoseGame()
    {
        GameManager.instance.isGameActive = false;
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

    public void AddMethod(Debug method)
    {
        debug += method;
    }

    void LoadHighScore()
    {
        if (!PlayerPrefs.HasKey("highScore"))
        {
            PlayerPrefs.SetInt("highScore", 0);
        }
        else
        {
            highScore = PlayerPrefs.GetInt("highScore");
        }
       
    }
    
    void SaveHighScore()
    {
        PlayerPrefs.SetInt("highScore", highScore);
        UpdateHighScoreText();
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = highScore.ToString();
    }
}
