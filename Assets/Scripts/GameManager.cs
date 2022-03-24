using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Gameplay Settings
    [SerializeField] int timeLeft = 100;
    private bool gamePaused;
    private int diamonds;

    #region Unity Callbacks
    private void Start()
    {
        InvokeRepeating(nameof(StopperTick), 3, 1);
    }
    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(gamePaused)
            {
                gamePaused = false;
                Time.timeScale = 1;
                Debug.Log("Game Unpaused");
            }
            else
            {
                gamePaused = true;
                Time.timeScale = 0;
                Debug.Log("Game Paused");
            }
        }
    }
    #endregion

    #region Private Methods
    private void StopperTick()
    {
        timeLeft--;
        if(timeLeft <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        CancelInvoke(nameof(StopperTick));
        timeLeft = 0;
    }
    #endregion

    #region Public Methods
    public void AddDiamonds(int count)
    {
        this.diamonds += count;
    }

    public void AddTime(int time)
    {
        this.timeLeft += time;
    }

    public void FreezeTime(int time)
    {
        CancelInvoke(nameof(StopperTick));
        InvokeRepeating(nameof(StopperTick), time, 1);
    }
    #endregion
}
