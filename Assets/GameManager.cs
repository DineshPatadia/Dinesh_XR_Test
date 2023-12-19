using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    GameObject mainMenuCanvas, scoreCanvas, ballsParent;

    [SerializeField]
    TextMeshProUGUI timerText, scoreText;

    [SerializeField]
    float timer = 30f;

    int score;

    bool gameOver, startMenu;

    private void OnEnable()
    {
        BasketScoreScript.incrementScore += AddScore;
    }

    private void OnDisable()
    {
        BasketScoreScript.incrementScore -= AddScore;
    }

    private void Start()
    {
        score = 0;
        Time.timeScale = 1f;
        startMenu = true;
    }

    private void Update()
    {
        if (!gameOver && !startMenu)
        {
            timerText.text = "" + timer.ToString("f0");
            timer -= Time.deltaTime;
        }

        if(timer<=0)
        {
            gameOver = true;
        }
    }

    void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void StartGame()
    {
        Debug.Log("GameStarted");
        mainMenuCanvas.SetActive(false);
        scoreCanvas.SetActive(true);
        ballsParent.SetActive(true);
        startMenu = false;
    }



    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
