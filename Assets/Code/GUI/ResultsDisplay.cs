using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Database.Score;

namespace GameGUI
{
    public class ResultsDisplay : MonoBehaviour
    {
        [SerializeField]
        private Text[] ResultsT;

        [SerializeField]
        private ScoreData scoreData;

        void Start()
        {
            for(int i = 0; i < ResultsT.Length;i++)
            {
                LevelData levelData = scoreData.GetLevelData(i);
                if (levelData.isPlayed)
                    ResultsT[i].text = $"{levelData.GreenTeamScore}-{levelData.RedTeamScore}";
                else
                    ResultsT[i].text = $"0-0";
            }
        }

        void Update()
        {

        }
    }
}
