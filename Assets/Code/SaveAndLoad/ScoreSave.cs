using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Database;
using Database.Score;
using Database.AI;
using GameEngine;

namespace SaveLoad
{
    public class ScoreSave : MonoBehaviour
    {
        public delegate void ScoreSendListener(bool succeed);
        public event ScoreSendListener OnScoreSendFinished;

        public const string NEW_HIGHSCORE = "new_highscore";

        public delegate void OnNewHightScoreDelegate();

        public event OnNewHightScoreDelegate OnNewHighScore;

        [SerializeField]
        private Player.PlayerSessionData m_sessionData;

        [SerializeField]
        private BotLevelsData levelsData;

        [SerializeField]
        private ScoreData scoreData;

        [SerializeField]
        private ScoreCounter scoreCounter;

        [SerializeField]
        private Timer timer;

        [SerializeField]
        private MenuEngine.MenuController menuController;


        private void Start()
        {
            timer.OnTimeRunOut += OnTimeRunOut;
        }

        private void OnTimeRunOut()
        {
            Database.Score.LevelData levelData = scoreData.GetLevelData(levelsData.ChoosedLevel);
            if (levelData.isPlayed == false || 
                levelData.GreenTeamScore - levelData.RedTeamScore < scoreCounter.GreenTeamScore - scoreCounter.RedTeamScore)
            {
                levelData.RedTeamScore = scoreCounter.RedTeamScore;
                levelData.GreenTeamScore = scoreCounter.GreenTeamScore;
                levelData.isPlayed = true;

                if (m_sessionData != null && m_sessionData.Connected)
                    SendNewHighscoreToWebPage(levelData);
                else
                    OnScoreSendFinished?.Invoke(false);

                OnNewHighScore?.Invoke();
            }
        }

        private void SendNewHighscoreToWebPage(Database.Score.LevelData levelData)
        {
            var data = new JSONScoreData(levelsData.ChoosedLevel, levelData.GreenTeamScore, levelData.RedTeamScore);

            var convertedData = JsonUtility.ToJson(data);

            WebCommunication.WebBridge.SendValue(NEW_HIGHSCORE, convertedData);
        }

        private class JSONScoreData
        {
            public int level;
            public int greenScore;
            public int redScore;

            public JSONScoreData(int level, int greensScore, int redScore)
            {
                this.level = level;
                this.greenScore = greensScore;
                this.redScore = redScore;
            }
        }
    }
}
