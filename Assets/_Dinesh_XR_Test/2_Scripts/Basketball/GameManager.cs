using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static HighScoresScriptableObjectScript;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void AddCurrentData(int playerNo, int playerScore);
    public static event AddCurrentData currentPlayerData;

    [SerializeField]
    GameObject mainMenuCanvas, startMenuUI, gameOverMenuUI, scoreCanvas, ballsParent, playerLocomotion;

    [SerializeField]
    Transform player;

    [SerializeField]
    TextMeshProUGUI playerStartMenuNameText, playerGameOverMenuNameText, playerScoreMenuNameText, timerText, GameOverScoreText, scoreText;

    [SerializeField]
    float gameTimer = 30f;

    [SerializeField]
    Transform[] balls;

    [SerializeField]
    TextMeshProUGUI[] topPlayerNamesText, topPlayerScoresText;

    int currentPlayerNo = 0;

    float timer = 0;

    Vector3 playerInitialPos;

    Vector3[] ballsInitialPos;

    int score;

    bool gameOver, startMenu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

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
        timer = gameTimer;
        startMenu = true;
        playerInitialPos = player.position;

        ballsInitialPos = new Vector3[balls.Length];
        for (int i = 0; i < balls.Length; i++)
        {
            ballsInitialPos[i] = balls[i].position;
        }
    }

    private void Update()
    {
        if (!gameOver && !startMenu)
        {
            timerText.text = "" + timer.ToString("f0");
            timer -= Time.deltaTime;
        }

        if (timer <= 0 && !gameOver)
        {
            gameOver = true;
            GameOver();

        }
    }

    void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void StartGame()
    {
        //Debug.Log("GameStarted");
        mainMenuCanvas.SetActive(false);
        scoreCanvas.SetActive(true);
        ballsParent.SetActive(true);
        playerLocomotion.SetActive(true);
        startMenu = false;
    }

    void GameOver()
    {
        //Debug.Log("GameOver");
        GameOverScoreText.text = "Score: " + score;
        startMenuUI.SetActive(false);
        scoreCanvas.SetActive(false);
        ballsParent.SetActive(false);
        gameOverMenuUI.SetActive(true);
        mainMenuCanvas.SetActive(true);
        player.position = playerInitialPos;
        playerLocomotion.SetActive(false);
    }

    public void Restart()
    {

        AddCurrentPlayerScore();

        score = 0;
        scoreText.text = score.ToString();

        timer = gameTimer;
        startMenu = true;
        gameOver = false;

        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].GetComponent<Rigidbody>().isKinematic = true;
            balls[i].position = ballsInitialPos[i];
            balls[i].GetComponent<XRGrabInteractable>().enabled = true;
            balls[i].GetComponent<Rigidbody>().isKinematic = false;
        }

        

        gameOverMenuUI.SetActive(false);
        startMenuUI.SetActive(true);

    }

    private void ResetHighScores()
    {
        for (int i = 0;i < topPlayerNamesText.Length;i++)
        {
            topPlayerNamesText[i].text = topPlayerScoresText[i].text = "-";
        }
    }

    public void UpdatePlayerHighScore(List<PlayerData> topPlayersData, int totalNoOfPlayersData)
    {
        ResetHighScores();

        currentPlayerNo = totalNoOfPlayersData;

        currentPlayerNo++;

        playerStartMenuNameText.text = playerGameOverMenuNameText.text = playerScoreMenuNameText.text = "Player No. " + currentPlayerNo;

        //Debug.Log(topPlayersData.Count);
        //Debug.Log(topPlayersData.Count);
        for (int i = 0; i < topPlayersData.Count; i++)
        {
            topPlayerNamesText[i].text = "Player No. " + topPlayersData[i].playerName;
            topPlayerScoresText[i].text = topPlayersData[i].playerScore.ToString();
        }
    }

    void AddCurrentPlayerScore()
    {
        if(currentPlayerData!=null)
        {
            currentPlayerData(currentPlayerNo, score);
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
        //Debug.Log("Exit");
    }
}
