using System;
using System.Collections.Generic;
using UnityEngine;
using static HighScoresScriptableObjectScript;
using static UnityEngine.GraphicsBuffer;

public class HighScoreManager : MonoBehaviour
{
    

    public HighScoresScriptableObjectScript highScoresScriptableObject;


    [SerializeField]
    public List<PlayerData> topPlayersData = new List<PlayerData>();

    [SerializeField]
    public List<PlayerData> allPlayersData = new List<PlayerData>();

    PlayerData tempPlayerData = new PlayerData();

    private void OnEnable()
    {
        GameManager.currentPlayerData += AddCurrentPlayerData;
    }

    private void OnDisable()
    {
        GameManager.currentPlayerData -= AddCurrentPlayerData;
    }

    private void Start()
    {
        FetchPlayerScores();
    }

    //Fetch Recent Highscores & Update top 5 players scores
    void FetchPlayerScores()
    {

        allPlayersData = CopyScriptableObjectData(highScoresScriptableObject.playerData);

        for (int i = 0; i < allPlayersData.Count; i++)
        {
            for (int j = i + 1; j < allPlayersData.Count; j++)
            {
                int iScore = allPlayersData[i].playerScore;
                int jScore = allPlayersData[j].playerScore;

                if (iScore <= jScore)
                {
                    tempPlayerData = allPlayersData[i];
                    allPlayersData[i] = allPlayersData[j];
                    allPlayersData[j] = tempPlayerData;
                }
            }
        }

        if (allPlayersData.Count > 0)
        {
            bool ifGreater = allPlayersData.Count > 5;

            int loopLenght = ifGreater ? 5 : allPlayersData.Count;

            topPlayersData.Clear();

            for (int k = 0; k < loopLenght; k++)
            {
                topPlayersData.Add(allPlayersData[k]);
            }
        }

        GameManager.Instance.UpdatePlayerHighScore(topPlayersData, allPlayersData.Count);
    }

    List<PlayerData> CopyScriptableObjectData(List<PlayerData> playerDatas)
    {
        var newList = new List<PlayerData>(playerDatas);
        return newList;
    }

    PlayerData UpdateCurrentPlayerData(PlayerData playerDatas)
    {
        PlayerData playerData = playerDatas;
        return playerData;
    }

    void AddCurrentPlayerData(int playerNo, int playerScore)
    {
        //Debug.Log(playerNo);
        //Debug.Log(playerScore);
        PlayerData currentPlayerData = new PlayerData();
        currentPlayerData.playerName = playerNo;
        currentPlayerData.playerScore = playerScore;
        highScoresScriptableObject.playerData.Add(UpdateCurrentPlayerData(currentPlayerData));
        FetchPlayerScores();

    }





}
