using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameEngine
{
    public class ScoreCounter : MonoBehaviour
    {
        public delegate void OnScoreChangedDelegate(int newscore);

        public event OnScoreChangedDelegate OnScoreofRedTeamChangedEvent;
        public event OnScoreChangedDelegate OnScoreofGreenTeamChangedEvent;
        public event OnScoreChangedDelegate OnPlayerPointsChanged;

        private int m_playerScorePoints;

        public int RedTeamScore { get; private set; }

        public int GreenTeamScore { get; private set; }

        public void AddPlayerPoints(int points)
        {
            m_playerScorePoints += points;
            OnPlayerPointsChanged?.Invoke(CalculatePlayerScore()); ;
        }

        public int CalculatePlayerScore()
        {
            return Mathf.Max(0, m_playerScorePoints);
        }

        public void AddScoreToRedTeam()
        {
            RedTeamScore++;
            OnScoreofRedTeamChangedEvent?.Invoke(RedTeamScore);
        }

        public void AddScoreToGreenTeam()
        {
            GreenTeamScore++;
            OnScoreofGreenTeamChangedEvent?.Invoke(GreenTeamScore);
        }
    }
}
