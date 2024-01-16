using Database.Score;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace GameGUI
{
    public class HighscoreHUD : MonoBehaviour
    {
        [SerializeField]
        private ScoreData m_scoreData;

        [SerializeField]
        private TextMeshProUGUI m_scoreText;

        private void Start()
        {
            m_scoreData.OnHighscoreDataChanged += OnScoreChanged;
            m_scoreText.text = m_scoreData.Highscore.ToString();
        }

        private void OnScoreChanged()
        {
            m_scoreText.text = m_scoreData.Highscore.ToString();
        }
    }
}
