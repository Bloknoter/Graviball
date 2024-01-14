using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Database.Score;
using Database.AI;


namespace SaveLoad
{
    public class ScoreLoad : MonoBehaviour
    {
        public delegate void ScoreLoadListener(bool succeed);
        public event ScoreLoadListener OnScoreLoaded;

        public const string PLAYER_NAME_KEY_FORM = "player_name";

        [SerializeField]
        private string m_url;

        [SerializeField]
        private WebCommunication.GameSocket m_gameSocket;

        [SerializeField]
        private Player.PlayerSessionData m_sessionData;

        [SerializeField]
        private ScoreData scoreData;

        private void Awake()
        {
            m_sessionData.OnConnected += OnConnected;
        }

        private void OnConnected()
        {
            StartCoroutine(FetchScoreFromServer());
        }

        private IEnumerator FetchScoreFromServer()
        {
            WWWForm form = new WWWForm();
            form.AddField(PLAYER_NAME_KEY_FORM, m_sessionData.Name);

            WWW connection = new WWW(m_url, form);

            yield return connection;

            if (connection.error != null)
            {
                OnScoreLoaded?.Invoke(false);
                connection.Dispose();
                yield break;
            }
            else if (connection.isDone)
            {
                FillScoreFromJson(connection.text);
                OnScoreLoaded?.Invoke(true);
            }

            connection.Dispose();
        }

        private void FillScoreFromJson(string json)
        {
            var fetchedData = JsonUtility.FromJson<FetchedScoreData>(json);

            for(int i = 0; i < fetchedData.levels.Count; ++i) 
            {
                var levelData = scoreData.GetLevelData(fetchedData.levels[i].level);
                levelData.isPlayed = true;
                levelData.GreenTeamScore = fetchedData.levels[i].player_score;
                levelData.RedTeamScore = fetchedData.levels[i].enemy_score;
            }
        }

        private class FetchedScoreData
        {
            public class LevelData
            {
                public int level;
                public int player_score;
                public int enemy_score;
            }

            public List<LevelData> levels;
        }
    }
}
