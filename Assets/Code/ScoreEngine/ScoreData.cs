using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Database.Score
{
    [CreateAssetMenu(fileName = "ScoreData", menuName = "Score data", order = 1)]
    public class ScoreData : ScriptableObject
    {
        [SerializeField]
        private LevelData[] levelDatas;

        public LevelData GetLevelData(int index)
        {
            if(index >= 0 && index < levelDatas.Length)
            {
                return levelDatas[index];
            }
            else
            {
                throw new System.Exception($"You are trying to GetLevelData with index {index}, but the whole amount of datas is {levelDatas.Length}");
            }
        }
    }

    [System.Serializable]
    public class LevelData
    {
        [SerializeField]
        private int redTeamScore = 0;
        public int RedTeamScore
        {
            get { return redTeamScore; }
            set
            {
                if (value >= 0)
                    redTeamScore = value;
            }
        }
        [SerializeField]
        private int greenTeamScore = 0;
        public int GreenTeamScore
        {
            get { return greenTeamScore; }
            set
            {
                if (value >= 0)
                    greenTeamScore = value;
            }
        }

        private bool isplayed;

        public bool isPlayed
        {
            get { return isplayed; }
            set
            {
                if (!isplayed)
                    isplayed = value;
            }
        }

    }
}
