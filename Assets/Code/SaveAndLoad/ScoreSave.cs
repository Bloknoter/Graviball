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
        public delegate void ScoreSaveListener(bool succeed);
        public event ScoreSaveListener OnScoreSaved;

        public delegate void OnNewHightScoreDelegate();
        public event OnNewHightScoreDelegate OnNewHighScore;

        public const string NEW_HIGHSCORE = "new_highscore";

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

        private bool m_scoreSaved;

        public bool ScoreSaved => m_scoreSaved;

        private void Start()
        {
            timer.OnTimeRunOut += OnTimeRunOut;
        }

        private void OnTimeRunOut()
        {
            Database.Score.LevelData levelData = scoreData.LevelDataAt(levelsData.ChoosedLevel);
            if (levelData.isPlayed == false || 
                levelData.GreenTeamScore - levelData.RedTeamScore < scoreCounter.GreenTeamScore - scoreCounter.RedTeamScore)
            {
                levelData.RedTeamScore = scoreCounter.RedTeamScore;
                levelData.GreenTeamScore = scoreCounter.GreenTeamScore;
                levelData.isPlayed = true;
                scoreData.CallScoreDataChangedEvent();

                SendNewHighscoreToWebPage(levelData);

                OnNewHighScore?.Invoke();
            }
        }

        private void SendNewHighscoreToWebPage(Database.Score.LevelData levelData)
        {
            var data = new JSONScoreData(levelsData.ChoosedLevel, levelData.GreenTeamScore, levelData.RedTeamScore);
            var convertedData = JsonUtility.ToJson(data);

            WebCommunication.WebBridge.Instance.Send(NEW_HIGHSCORE, convertedData);

            SetScoreAsSaved(true);
        }

        private void SetScoreAsSaved(bool succeed)
        {
            m_scoreSaved = true;
            OnScoreSaved?.Invoke(succeed);
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
