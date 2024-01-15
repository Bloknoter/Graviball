using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Database.Score;
using WebCommunication;


namespace SaveLoad
{
    public class ScoreLoad : MonoBehaviour
    {
        public const string PLAYER_SCORE_DATA = "score_data";

        private static bool m_fetched = false;

        [SerializeField]
        private ScoreData scoreData;

        private void Start()
        {
            if (!m_fetched)
            {
                m_fetched = true;
                FetchScore();
            }
        }

        private void FetchScore()
        {
            var dataAsJson = WebBridge.Instance.Request(PLAYER_SCORE_DATA);
            FillScoreFromJson(dataAsJson);

            scoreData.CallScoreDataChangedEvent();
        }

        private void FillScoreFromJson(string json)
        {
            var fetchedData = JsonUtility.FromJson<FetchedScoreData>(json);

            for(int i = 0; i < scoreData.LevelsCount; ++i) 
            {
                var levelData = scoreData.LevelDataAt(i);
                if (i < fetchedData.levels.Count)
                {
                    levelData.isPlayed = true;
                    levelData.GreenTeamScore = fetchedData.levels[i].greenScore;
                    levelData.RedTeamScore = fetchedData.levels[i].redScore;
                }
                else
                {
                    levelData.isPlayed = false;
                }
            }
        }

        [System.Serializable]
        private class FetchedScoreData
        {
            [System.Serializable]
            public class LevelData
            {
                public int greenScore;
                public int redScore;

                public LevelData(int greenScore, int redScore)
                {
                    this.greenScore = greenScore;
                    this.redScore = redScore;
                }
            }

            public List<LevelData> levels;

            public FetchedScoreData(List<LevelData> levels)
            {
                this.levels = levels;
            }
        }
    }
}
