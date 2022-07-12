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
        public delegate void OnNewHightScoreDelegate();

        public event OnNewHightScoreDelegate OnNewHighScore;

        [SerializeField]
        private SaveFileInfo fileInfo;

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


        void Start()
        {
            timer.OnTimeRunOut += OnTimeRanOut;
        }

        void Update()
        {
            
        }

        private void OnTimeRanOut()
        {
            Database.Score.LevelData levelData = scoreData.GetLevelData(levelsData.ChoosedLevel);
            if (levelData.isPlayed == false || 
                levelData.GreenTeamScore - levelData.RedTeamScore < scoreCounter.GreenTeamScore - scoreCounter.RedTeamScore)
            {
                levelData.RedTeamScore = scoreCounter.RedTeamScore;
                levelData.GreenTeamScore = scoreCounter.GreenTeamScore;
                levelData.isPlayed = true;
                FileStream stream = new FileStream(fileInfo.FilePath, FileMode.OpenOrCreate);
                BinaryFormatter formatter = new BinaryFormatter();
                Database.Score.LevelData[] data = new Database.Score.LevelData[] { scoreData.GetLevelData(0), scoreData.GetLevelData(1), scoreData.GetLevelData(2) };
                formatter.Serialize(stream, data);
                stream.Close();
                OnNewHighScore?.Invoke();
            }
        }
    }
}
