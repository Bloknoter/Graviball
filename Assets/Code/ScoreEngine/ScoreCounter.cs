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

        public int RedTeamScore { get; private set; }

        public int GreenTeamScore { get; private set; }

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
