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

        public const string PLAYER_NAME_KEY_FORM = "player_name";
        public const string LEVEL_KEY_FORM = "level";
        public const string PLAYER_SCORE_KEY_FORM = "player_score";
        public const string ENEMY_SCORE_KEY_FORM = "enemy_score";

        public delegate void OnNewHightScoreDelegate();

        public event OnNewHightScoreDelegate OnNewHighScore;

        [SerializeField]
        private string m_url;

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
                    StartCoroutine(SendNewHighscoreToServer(levelData));
                else
                    OnScoreSendFinished?.Invoke(false);

                OnNewHighScore?.Invoke();
            }
        }

        private IEnumerator SendNewHighscoreToServer(Database.Score.LevelData levelData)
        {
            WWWForm form = new WWWForm();
            form.AddField(PLAYER_NAME_KEY_FORM, m_sessionData.Name);
            form.AddField(LEVEL_KEY_FORM, levelsData.ChoosedLevel);
            form.AddField(PLAYER_SCORE_KEY_FORM, levelData.GreenTeamScore);
            form.AddField(ENEMY_SCORE_KEY_FORM, levelData.RedTeamScore);

            WWW connection = new WWW(m_url, form);

            yield return connection;

            if(connection.error != null)
            {
                OnScoreSendFinished?.Invoke(false);
                yield break;
            }
            else if(connection.isDone) 
            {
                OnScoreSendFinished?.Invoke(true);
            }

            connection.Dispose();
        }
    }
}
