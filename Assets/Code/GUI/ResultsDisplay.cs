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

        private void OnEnable()
        {
            scoreData.OnScoreDataChanged += OnScoreDataChanged;
            UpdateResults();
        }

        private void OnScoreDataChanged()
        {
            UpdateResults();
        }

        private void UpdateResults()
        {
            for (int i = 0; i < ResultsT.Length; i++)
            {
                LevelData levelData = scoreData.LevelDataAt(i);
                if (levelData.isPlayed)
                    ResultsT[i].text = $"{levelData.GreenTeamScore}-{levelData.RedTeamScore}";
                else
                    ResultsT[i].text = $"0-0";
            }
        }

        private void OnDisable()
        {
            scoreData.OnScoreDataChanged -= OnScoreDataChanged;
        }
    }
}
