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
            int totalScore = scoreCounter.CalculatePlayerScore();
            if (scoreData.Highscore < totalScore)
            {
                scoreData.Highscore = totalScore;

                SendNewHighscoreToWebPage(scoreData.Highscore);

                OnNewHighScore?.Invoke();
            }
        }

        private void SendNewHighscoreToWebPage(int score)
        {
            WebCommunication.WebBridge.Instance.Send(NEW_HIGHSCORE, score.ToString());

            SetScoreAsSaved(true);
        }

        private void SetScoreAsSaved(bool succeed)
        {
            m_scoreSaved = true;
            OnScoreSaved?.Invoke(succeed);
        }
    }
}
