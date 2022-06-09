using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple game managers in the scene!");
        }
    }

    //UI
    public Text timeText;
    public Text diamondText;
    public Text redKeyText;
    public Text greenKeyText;
    public Text goldKeyText;

    public GameObject snowFlake;
    public GameObject endGamePanel;
    public Text endGameText;
    public Text infoText;

    // Gameplay Settings
    [SerializeField] int timeLeft = 100;
    private bool gamePaused;
    private int diamonds;
    public int redKeys = 0;
    public int greenKeys = 0;
    public int goldKeys = 0;
    private bool gameEnded;

    #region Unity Callbacks
    private void Start()
    {
        timeText.text = timeLeft.ToString();
        InvokeRepeating(nameof(StopperTick), 3, 1);
    }
    private void Update()
    {
        if(gameEnded)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
            if(Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if(Input.GetButtonDown("Cancel"))
        {
            if(gamePaused)
            {
                gamePaused = false;
                Time.timeScale = 1;
                infoText.text = "";
            }
            else
            {
                gamePaused = true;
                Time.timeScale = 0;
                infoText.text = "Game Paused\nPress Esc to Unpause";
            }
        }
    }
    #endregion

    #region Private Methods
    private void StopperTick()
    {
        snowFlake.SetActive(false);
        timeLeft--;
        timeText.text = timeLeft.ToString();
        if(timeLeft <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        CancelInvoke(nameof(StopperTick));
        timeLeft = 0;
        timeText.text = timeLeft.ToString();
        endGamePanel.SetActive(true);
        gameEnded = true;
    }
    public void GameWin()
    {
        CancelInvoke(nameof(StopperTick));

        endGamePanel.SetActive(true);
        endGameText.text = "You Win";
        gameEnded = true;
    }
    #endregion

    #region Public Methods
    public void AddDiamonds(int count)
    {
        this.diamonds += count;
        diamondText.text = diamonds.ToString();
    }

    public void AddTime(int time)
    {
        this.timeLeft += time;
        timeText.text = timeLeft.ToString();
    }

    public void FreezeTime(int time)
    {
        snowFlake.SetActive(true);
        CancelInvoke(nameof(StopperTick));
        InvokeRepeating(nameof(StopperTick), time, 1);
    }

    public void AddKey(KeyColor color)
    {
        switch(color)
        {
            case KeyColor.Gold:
                goldKeys++;
                goldKeyText.text = goldKeys.ToString();
                break;

            case KeyColor.Green:
                greenKeys++;
                greenKeyText.text = greenKeys.ToString();
                break;

            case KeyColor.Red:
                redKeys++;
                redKeyText.text = redKeys.ToString();
                break;
        }
    }
    #endregion
}
