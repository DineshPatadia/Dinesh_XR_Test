using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "High Scores", menuName = "Basketball")]
public class HighScoresScriptableObjectScript : ScriptableObject
{
    [Serializable]
    public class PlayerData
    {
        public int playerName;
        public int playerScore;
    }

    [SerializeField]
    public List<PlayerData> playerData = new List<PlayerData>();
}
